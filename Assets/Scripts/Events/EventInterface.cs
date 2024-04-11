using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInterface : MonoBehaviour
{
    public Action ye_event;

    public void Start()
    {
        fillOutInterface();
    }
    public virtual void fillOutInterface() { 
       
    }

    public void endInterface() {
        Destroy(gameObject);
    }
    public virtual void interfaceUpdate() {
        return;
    }

    public void updateEvent() {
        Event_manager.instance.updateDay();
    }
}
