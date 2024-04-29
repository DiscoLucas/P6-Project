using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static MarketManager;
public class MarketManager : MonoBehaviour
{
    public int eventID;
    public TextMeshProUGUI eventDescription;
    public TextMeshProUGUI eventEffect;
    public GameObject Popup;

    public MarketEvents[] marketEvents;

    private void Awake()
    {
        
    }

    public enum MarketEventType
    {
        None,
        InterestRateChange,
        VolatilityChange,
        HousingPriceChange
        /*FrygtForInflation,
        PresPÂDenDanskeKrone,
        UroPÂFinansMarkedet,
        BekymringForRecession,
        ForbrugerTillid,
        PolitiskeBeslutninger,
        DÂrligÿkonomi,
        LavKonjunktur,
        HumanitÊrKrise,
        FolkShorterGMEAktierIgen,
        MarkedsRegulationer,
        MarkedetErIStabilVÊkst,
        MarkedetErIStabilRecession,
        MarkedetErIStabilStagnation,
        OptimismePÂMarkedet,*/
    }
    /*
    public double GetMarketModifier(int eventID)
    {
        switch (eventID)
        {
            case
        }
    }*/

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
