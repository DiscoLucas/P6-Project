using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Event
{
    public string name;
    public string describtion;
    public bool is_done = false;
    public Event realted_Event;
    public EventInterface e_interface;
    public GameObject e_interface_prefab;

    public virtual void startEvent() {
        e_interface = Event_manager.instance.instiate_interface(e_interface_prefab);
        e_interface.ye_event = this;
    }
    public virtual void endEvent()
    {
        e_interface.endInterface();
    }
    public virtual bool isEventFinnished() {
        return is_done;
    }
    public virtual void update() { 
        
    }

    public virtual void VisualUpdate() { 
    
    }


}
