using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class MarketManager : MonoBehaviour
{
    public int eventID;
    public TextMeshProUGUI eventDescription;
    public TextMeshProUGUI eventEffect;
    public GameObject Popup;

    public markedEvents[] markedEvents;
    public void showMarkedEvent() {
        Popup.SetActive(true);
        eventID = UnityEngine.Random.Range(0, markedEvents.Length);
        eventDescription.text = markedEvents[eventID].eventsDescription;
        eventEffect.text = markedEvents[eventID].eventsEffect;
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

    public markedEvents(string eventsDescription, string eventsEffect) { 
        this.eventsDescription = eventsDescription;
        this.eventsEffect = eventsEffect;
    }
}
