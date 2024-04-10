using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Event
{
    public String eventName;

    public virtual void loadEvent() { 
    
    }
    public virtual void startEvent()
    {

    }

    public virtual void updateVisual()
    {

    }

    public virtual void eventUpdate()
    {
    }

    public virtual bool eventDone()
    {
        return false;
    }

}
