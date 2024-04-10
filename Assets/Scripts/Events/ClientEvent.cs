using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;
[Serializable]
public class ClientEvent : Event
{
    public TMP_Text Header;
    public ClientEvent realtedeEvent;
    public override void startEvent()
    {
        Header.text = eventName;
        
    }

}
