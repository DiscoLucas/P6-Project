using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DailyAction
{
    public ClientEvent[] events;
    public int eventIndex = 0;
    public void updateEvent() {
        Event cEvent = events[eventIndex];
        cEvent.startEvent();

    }
}
