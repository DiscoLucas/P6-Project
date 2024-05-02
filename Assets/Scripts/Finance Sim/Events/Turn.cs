using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Turn
{
    public List<GameObject> actions;
    public actionsDisplay currentActionDisplay;
    public int actionsIndex = 0;
    public void startDay()
    {
        actionsIndex = 0;
        startDisplay();
    }

    public void startDisplay() {
        currentActionDisplay = Event_manager.instance.instiate_interface(actions[actionsIndex]);
        if (currentActionDisplay.clientIndex <0)
        {
            currentActionDisplay.setClient(0);
        }
        ClientData c = Event_manager.instance.clientManager.getClient(currentActionDisplay.clientIndex);
        currentActionDisplay.fillOutDisplay();
        if (!c.haveBeenPresented) {
            currentActionDisplay.gameObject.SetActive(false);
            Event_manager.instance.clientManager.startClientIntro(c);
        }


    }
    public void updateActions()
    {
        if (currentActionDisplay.isFinnished())
        {
            int oldclientIndex = currentActionDisplay.clientIndex;
            currentActionDisplay.end();
            actionsIndex++;
            if (actionsIndex >= actions.Count)
            {
                Event_manager.instance.changeDay();
                Debug.Log("Change day");
            }
            else
            {
                currentActionDisplay = Event_manager.instance.instiate_interface(actions[actionsIndex]);
                //IF client have not been set then set it to the first client
                //TODO: change the methode
                Debug.Log("Client index " + currentActionDisplay.clientIndex);
                if (currentActionDisplay.clientIndex == -1)
                {
                    currentActionDisplay.setClient(0);
                }

                if (currentActionDisplay.clientIndex == oldclientIndex)
                {
                    
                }
                else 
                {
                    ClientData c = Event_manager.instance.clientManager.getClient(currentActionDisplay.clientIndex);
                    Event_manager.instance.clientManager.startClientIntro(c);
                }
                currentActionDisplay.fillOutDisplay();
                
            }

        }
        currentActionDisplay.displayUpdate();
    }
}
