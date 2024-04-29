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

    public markedEvents[] markedEvents;

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
        PresP�DenDanskeKrone,
        UroP�FinansMarkedet,
        BekymringForRecession,
        ForbrugerTillid,
        PolitiskeBeslutninger,
        D�rlig�konomi,
        LavKonjunktur,
        Humanit�rKrise,
        FolkShorterGMEAktierIgen,
        MarkedsRegulationer,
        MarkedetErIStabilV�kst,
        MarkedetErIStabilRecession,
        MarkedetErIStabilStagnation,
        OptimismeP�Markedet,*/
    }
    /*
    public double GetMarketModifier(int eventID)
    {
        switch (eventID)
        {
            case
        }
    }*/

    public int showMarkedEvent() {
        Popup.SetActive(true);
        eventID = UnityEngine.Random.Range(0, markedEvents.Length);
        eventDescription.text = markedEvents[eventID].eventsDescription;
        eventEffect.text = markedEvents[eventID].eventsEffect;
        return eventID;
    }


    public void endMarkedEvent() {
        Popup.SetActive(false);
        GameManager.instance.updateTurn();
    }
}

[Serializable]
public class markedEvents {
    public string eventsDescription;
    public string eventsEffect;
    public MarketEventType eventType;
    public double interestRateModifier;
    public double volatilityModifier;
    public double housingPriceModifier;

    public markedEvents(
        string eventsDescription,
        MarketEventType eventType,
        double interestRateModifier,
        double volatilityModifier,
        double housingPriceModifier) 
    {
        this.eventsDescription = eventsDescription;
        this.eventType = eventType;
        
    }
}
