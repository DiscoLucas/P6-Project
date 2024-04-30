using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static MarketManager;
public class MarketManager : MonoBehaviour
{
    public int eventID = -1;
    public TextMeshProUGUI eventDescription;
    public TextMeshProUGUI eventEffect;
    public GameObject Popup;
    public MarketEvents[] marketEvents;
    public loanTypes[] loanTypes;
    public List<Loan> loans= new List<Loan>();
    [Tooltip("The amount of time that passes on every turn. each unit is 1 month")]
    public float timeHorizon = 1f;
    [Tooltip("Time increments calculated in each time horizon.")]
    public float dt;
    public float step;
    public Transform btnContainer;
    public GameObject graphPrefab;
    public IRVisualizer visualizerController;

    public ClientData client;
    private void Awake()
    {
        dt = timeHorizon / 4f;
        step = (int)(timeHorizon / dt);
    }
    private void Start()
    {

    }

    public enum MarketEventType
    {
        None,
        InterestRateChange,
        VolatilityChange,
        HousingPriceChange
    }


    public Loan checkIfTimeIsUpForLoan() {
        int cM = GameManager.instance.monthNumber;
        foreach (Loan loan in loans) {
            if (cM >= loan.initialMonth + loan.loanAmount) { 
                return loan;
            }
        }

        return null;
    }
    public void simulateIR() {
        MarketEvents marketEvent = GetMarketEvent();
        double volatility = 1;
        double interestRateChange = 1;
        double housingMarked = 1;
        if (marketEvent != null)
        {
            // Switch between modifiers based on the event type
            switch (marketEvent.eventType)
            {
                case MarketManager.MarketEventType.InterestRateChange:
                    interestRateChange = updateModifer(marketEvent.rateModifier);
                    break;

                case MarketManager.MarketEventType.VolatilityChange:
                    volatility = updateModifer(marketEvent.rateModifier);
                    break;

                case MarketManager.MarketEventType.HousingPriceChange:
                    housingMarked = updateModifer(marketEvent.rateModifier);
                    break;
                default: break;
            }

            foreach (loanTypes loanTypes in loanTypes) {
                loanTypes.interssetRate *= (float)interestRateChange;
                loanTypes.volatility *= volatility;

            }
        }
        //Update All loan
        foreach (Loan loan in loans) {
            IRModifierUpdater(loan,volatility,interestRateChange,housingMarked);
        }

    }

    void IRModifierUpdater( Loan loan, double volatility, double interestRateChange, double housingMarked)
    {
        if (loan.LoanTerm != 360)
        {
            if (loan.IRForTime.Count != 0)
            {
                IRModel_HullWhite model = new IRModel_HullWhite(loan.IRForTime.Last() * interestRateChange, loan.volatility * volatility, loan.longTermRate);
                updateIR(loan, model.PredictIRforTimeInterval(dt, timeHorizon));
            }
            else // if the interest history is empty, use the initial interest rate
            {
                IRModel_HullWhite model = new IRModel_HullWhite(loan.interestRate * interestRateChange, loan.volatility * volatility, loan.longTermRate); // TODO: add market modifier to the parameters
                updateIR(loan, model.PredictIRforTimeInterval(dt, timeHorizon));
            }
        }
        // if the the interest history is not empty, use the last value as the current rate
        /* if (loan.IRForTime.Count != 0)
         {
             IRModel_HullWhite model = new IRModel_HullWhite(loan.IRForTime.Last() * interestRateChange, loan.volatility * volatility, loan.longTermRate);
             loanManager.UpdateIR(clientName, model.PredictIRforTimeInterval(dt, timeHorizon));
         }
         else // if the interest history is empty, use the initial interest rate
         {
             IRModel_HullWhite model = new IRModel_HullWhite(loan.interestRate * interestRateChange, loan.volatility * volatility, loan.longTermRate); // TODO: add market modifier to the parameters
             loanManager.UpdateIR(clientName, model.PredictIRforTimeInterval(dt, timeHorizon));
         }*/
    }

    public void createLoan(ClientData client, double loanAmount, loanTypes loanType) {
        Loan nLoan = new Loan(
                                client,
                                loanType.LoanTime,
                                loanAmount, 
                                loanType.interssetRate, 
                                loanType.volatility,
                                loanType.longTermRate,
                                GameManager.instance.monthNumber,
                                loanType.installment
        );
        var graphObj = Instantiate(graphPrefab,btnContainer);
        loanSelcetor loanS = graphObj.GetComponent<loanSelcetor>();
        loanS.loan = nLoan;
        loanS.iRVisualizer = visualizerController;
        loanS.fillout();
        loans.Add(nLoan);
    }


    public void updateIR(Loan loan, double[] newIr) {
        for (int i = 0; i < newIr.Length; i++) {
            loan.IRForTime.Add(newIr[i]);
        }
    }

    double updateModifer(double percentechange)
    {
        if (percentechange > 0)
        {
            return (percentechange / 100) + 1;
        }
        else
        {
            return 1 - (percentechange / 100);
        }
    }
    public void showMarkedEvent() 
    {
        Popup.SetActive(true);
        eventID = UnityEngine.Random.Range(0, marketEvents.Length);
        eventDescription.text = marketEvents[eventID].eventsDescription;
        marketEvents[eventID].OverideText(eventEffect);
    }
    public MarketEvents GetMarketEvent()
    {
        if (eventID == -1)
        {
            return null;
        }

        MarketEvents me = marketEvents[eventID];
        eventID = -1;
        return me;
    }

    public void endMarkedEvent() {
        Popup.SetActive(false);
        GameManager.instance.updateTurn();
    }
}

[Serializable]
public class MarketEvents {
    public string eventsDescription;
    public string eventsEffect;
    public MarketEventType eventType;
    public double rateModifier;

    public MarketEvents(
        string eventsDescription,
        MarketEventType eventType,
        double rateModifier) 
    {
        this.eventsDescription = eventsDescription;
        this.eventType = eventType;
        this.rateModifier = rateModifier;
        
    }

    public void OverideText(TextMeshProUGUI eventDescription)
    {
        eventDescription.text = eventsEffect + " " + Math.Abs(rateModifier) + "%";
    }


}
[Serializable]
public class loanTypes {
    public string name;
    public int LoanTime;
    public bool installment = false;
    public float interssetRate;
    public double volatility;
    public double longTermRate = 0.5;
}
