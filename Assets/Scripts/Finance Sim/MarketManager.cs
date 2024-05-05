using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static MarketManager;
/// <summary>
/// The manager that controlls the market and loans
/// </summary>
public class MarketManager : MonoBehaviour
{
    /// <summary>
    /// The index of the current event
    /// </summary>
    public int eventID = -1;
    [Header("The popup")]
    /// <summary>
    /// The header of the event
    /// </summary>
    public TextMeshProUGUI eventDescription;
    /// <summary>
    /// The text on the popup that describe the effect of what have happend
    /// </summary>
    public TextMeshProUGUI eventEffect;
    /// <summary>
    /// The game object that pop up
    /// </summary>
    public GameObject Popup;
    /// <summary>
    /// The market events that can happen
    /// </summary>
    [Header("List of stuff")]
    public MarketEvents[] marketEvents;
    /// <summary>
    /// The list that conatin the different loans types;
    /// </summary>
    public LoanTypes[] loanTypes;
    /// <summary>
    /// List of loans
    /// </summary>
    public List<Loan> loans= new List<Loan>();
    [Header("Simulation variables")]
    [Tooltip("The amount of time that passes on every turn. each unit is 1 month")]
    public float timeHorizon = 1f;
    [Tooltip("Time increments calculated in each time horizon.")]
    public float dt;
    /// <summary>
    /// The step that are simulatede 
    /// </summary>
    public float step;

    public float priceInterestRateMultiplyer = 10;
    public float installWithoutmentMultiplyer = 0.03f;
    /// <summary>
    /// The container that have all the buttons
    /// </summary>
    [Header("UI elements")]
    public Transform btnContainer;
    /// <summary>
    /// The prefab that creates the button that update the graphs
    /// </summary>
    public GameObject graphPrefab;
    /// <summary>
    /// The scrit that controll the visual aspect of the graphs
    /// </summary>
    public IRVisualizer visualizerController;
    private void Awake()
    {
        dt = timeHorizon / 4f;
        step = (int)(timeHorizon / dt);
    }

    private void Start()
    {
    }
    /// <summary>
    /// The different types of market events
    /// </summary>
    public enum MarketEventType
    {
        None,
        InterestRateChange,
        VolatilityChange,
        HousingPriceChange
    }

    /// <summary>
    /// This function check if any loans is done or need converting 
    /// And it return the loan
    /// </summary>
    /// <returns></returns>
    public Loan checkIfTimeIsUpForLoan() {
        int cM = GameManager.instance.monthNumber;
        foreach (Loan loan in loans) {
            if (cM >= loan.initialMonth + loan.loanAmount) { 
                return loan;
            }
        }

        return null;
    }

    /// <summary>
    /// Simulate the interest rates on all loans and also add the event on the current market events
    /// </summary>
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

            foreach (LoanTypes loanTypes in loanTypes) {
                loanTypes.interssetRate *= (float)interestRateChange;
                loanTypes.volatility *= volatility;

            }
        }
        //Update All loan
        foreach (Loan loan in loans) {
            //hvis lån er med afdrag: Betal lån tilbage med monthly payment
            if (loan.installment)
                loan.payLoan((int)loan.MonthlyPayment);
            IRModifierUpdater(loan,volatility,interestRateChange,housingMarked);
        }

    }

    /// <summary>
    /// Take in a loan and calulate the new interest rates
    /// </summary>
    /// <param name="loan"></param>
    /// <param name="volatility"></param>
    /// <param name="interestRateChange"></param>
    /// <param name="housingMarked"></param>
    void IRModifierUpdater( Loan loan, double volatility, double interestRateChange, double housingMarked)
    {
        if (!loan.fixedIR)
        {
            if (loan.IRForTime.Count != 0)
            {
                IRModel_HullWhite model = new IRModel_HullWhite(loan.IRForTime.Last() * interestRateChange, loan.volatility * volatility, loan.longTermRate);
                double[] predic = model.PredictIRforTimeInterval(dt, timeHorizon);
                updateIR(loan, predic);
            }
            else // if the interest history is empty, use the initial interest rate
            {
                IRModel_HullWhite model = new IRModel_HullWhite(loan.interestRate * interestRateChange, loan.volatility * volatility, loan.longTermRate); // TODO: add market modifier to the parameters
                updateIR(loan, model.PredictIRforTimeInterval(dt, timeHorizon));
            }
        }
        else {
            int steps = (int)(timeHorizon / dt);
            if (loan.IRForTime.Count != 0)
            {
                double[] predic= new double[steps];
                Array.Fill(predic, loan.IRForTime.Last());
                updateIR(loan, predic);
            }
            else 
            {
                double[] predic = new double[steps];
                Array.Fill(predic, loan.interestRate);
                updateIR(loan, predic);
            }
        }
    }

    /// <summary>
    /// Create a loan base on the given loan amount, loantype and client
    /// </summary>
    /// <param name="client"></param>
    /// <param name="loanAmount"></param>
    /// <param name="loanType"></param>
    public Loan createLoan(ClientData client, double loanAmount, LoanTypes loanType) {
        Loan nLoan = new Loan(
                                client,
                                loanType.loanTime,
                                loanAmount, 
                                loanType.interssetRate, 
                                loanType.volatility,
                                loanType.longTermRate,
                                GameManager.instance.monthNumber,
                                loanType.installment,loanType
        );
        nLoan.fixedIR = (loanType.loanTime == 360);
        var graphObj = Instantiate(graphPrefab,btnContainer);
        LoanSelcetor loanS = graphObj.GetComponent<LoanSelcetor>();
        loanS.loan = nLoan;
        loanS.iRVisualizer = visualizerController;
        loanS.fillout();
        loans.Add(nLoan);
        return nLoan;
    }

    /// <summary>
    /// Takes in a loan and the new interestrates and added it to the interestrate list
    /// </summary>
    /// <param name="loan"></param>
    /// <param name="newIr"></param>
    public void updateIR(Loan loan, double[] newIr) {
        for (int i = 0; i < newIr.Length; i++) {
            if (loan.IRForTime.Count == 0)
            {
                double currentPrice = loan.IRPForTime[loan.IRPForTime.Count - 1];
                currentPrice -= (newIr[i]-loan.getFirstInterestRate()) * priceInterestRateMultiplyer;
                if (!loan.installment)
                    currentPrice *= 1-installWithoutmentMultiplyer;
                loan.IRPForTime.Add(currentPrice);
            }
            else {
                double currentPrice = loan.IRPForTime[loan.IRPForTime.Count - 1];
                currentPrice -= (newIr[i] - loan.IRForTime[loan.IRForTime.Count-1])* priceInterestRateMultiplyer;
                if (!loan.installment)
                    currentPrice *= 1 - installWithoutmentMultiplyer;
                loan.IRPForTime.Add(currentPrice);

            }
            loan.IRForTime.Add(newIr[i]);
        }
    }

    /// <summary>
    /// Take in a perecntes and return a double. if it get 3 it will return 1.03 and if it gets -3 it will return 0.93
    /// </summary>
    /// <param name="percentechange"></param>
    /// <returns></returns>
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
    /// <summary>
    /// This function is used to choose a market event and open the popup
    /// </summary>
    public void showMarkedEvent() 
    {
        Popup.SetActive(true);
        eventID = UnityEngine.Random.Range(0, marketEvents.Length);
        eventDescription.text = marketEvents[eventID].eventsDescription;
        marketEvents[eventID].OverideText(eventEffect);
    }
    /// <summary>
    /// Gets the current market event and if there are none there is current it return null. Every time this is used it set the current event to null
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// This function is called when the event popup is close and it closes it and update turn 
    /// </summary>
    public void endMarkedEvent() {
        Popup.SetActive(false);
        GameManager.instance.updateTurn();
    }

    internal Loan convertLoan(ClientData client, float neededLoan, Loan loan, LoanTypes l)
    {
        int remaingTime = GameManager.instance.monthNumber - loan.periodStartMonth;
        int loanTime = l.loanTime;
        loan.fixedIR = (l.loanTime == 360);
        if (remaingTime + l.loanTime >= 360) {
            Debug.Log("LastPeriod");
            loanTime = 360 - remaingTime;
            loan.lastPeriod = true;
        }
        
        loan.convertLoan(GameManager.instance.monthNumber, loanTime, l.interssetRate, l.volatility,l);
        return loan;
    }
}
/// <summary>
/// The market event that change the state of the market
/// </summary>
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

    /// <summary>
    /// This function takes in <see cref="TextMeshProUGUI"/> and write the events effect down
    /// </summary>
    /// <param name="eventDescription"></param>
    public void OverideText(TextMeshProUGUI eventDescription)
    {
        eventDescription.text = eventsEffect + " " + Math.Abs(rateModifier) + "%";
    }


}
/// <summary>
/// This class controlls and traks the of the different loans
/// </summary>
[Serializable]
public class LoanTypes {
    /// <summary>
    /// Name of the loan that is used in <see cref="Qustion"/>'s
    /// </summary>
    public string name;
    /// <summary>
    /// The time of the loan
    /// </summary>
    public int loanTime;
    /// <summary>
    /// is this type of loan isntallment
    /// </summary>
    public bool installment = false;
    /// <summary>
    /// The intersset Rate of this of <see cref="Loan"/>
    /// </summary>
    public float interssetRate;
    /// <summary>
    /// The volatility of this of <see cref="Loan"/>
    /// </summary>
    public double volatility;
    /// <summary>
    /// ???
    /// </summary>
    public double longTermRate = 0.5;
}
