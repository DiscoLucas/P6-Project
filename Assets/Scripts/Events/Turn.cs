using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Turn
{
    public List<GameObject> actions;
    actionsDisplay currentActionDisplay;
    public int actionsIndex = 0;
    public void startDay()
    {
        actionsIndex = 0;
        currentActionDisplay = Event_manager.instance.instiate_interface(actions[actionsIndex]);
    }

    public void updateActions()
    {
        if (currentActionDisplay.isFinnished())
        {
            currentActionDisplay.end();
            actionsIndex++;
            if (actionsIndex >= actions.Count)
                Event_manager.instance.changeDay();
            else
            {
                currentActionDisplay = Event_manager.instance.instiate_interface(actions[actionsIndex]);

            }

        }
        currentActionDisplay.displayUpdate();
    }
}
