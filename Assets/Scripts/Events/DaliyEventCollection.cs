using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DaliyEventCollection
{
    public List<Event> events;
    public int eventIndex = 0;
    public void startDay() {
        eventIndex = 0;

        events[eventIndex].startEvent();
    }

    public void updateEvent() {
        if (events[eventIndex].isEventFinnished()) {
            events[eventIndex].endEvent();
            eventIndex++;
            if (eventIndex >= events.Count)
                Event_manager.instance.changeDay();
            else {
                events[eventIndex].startEvent();
                        
            }

        }
        events[eventIndex].update();
        events[eventIndex].VisualUpdate();
    }
}
