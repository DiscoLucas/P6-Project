using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ClientManager cm;
    //[SerializeField] List<Incident> Incidents incidents;
    public int startType = 0;
    public TurnEvent[] turnType;
    public int monthNumber = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            //
            monthNumber++;
            Debug.Log("#######################" +
                "\nmounth: " + monthNumber);

            int turnTypeIndex = decideWhatShouldHappend();
            if (turnType[turnTypeIndex].type == TurnType.New_customer)
            {
                newCustomer();
            }
            else if (turnType[turnTypeIndex].type == TurnType.Change_forCustomer)
            { 
                clientMeeting();
            }
            else if (turnType[turnTypeIndex].type == TurnType.Evnet)
            {
                Incident();
            }
            else if (turnType[turnTypeIndex].type == TurnType.None)
            {
                //call the next round;
            }
        }
    }


    void newCustomer() {
        Debug.Log("New customer");
        if(cm != null)
        {
            cm.hastalked = false;
            cm.ClientObject.SetActive(true);
            cm.an.Play("WalkIn");

        }
    }

    void Incident()
    {
        Debug.Log("A racing incident");
        //træk incident fra incident list
        //kør incident
    }

    void clientMeeting()
    {
        Debug.Log("Client meeting have startede");
        //client walks in, like in newCustomer function
        //Client choses speech that revolves around getting update to bonds "Hey jeg har fået bedre arbejde lol"
        //Player gets to fill out the correct paper work - Dette slutter af med en ja/nej
        //Client gør som player siger
    }


    int decideWhatShouldHappend() {
        int turnTypeIndex = 0;
        if (monthNumber == 1)
        {
            turnTypeIndex = startType;
        }
        else
        {
            for (int i = 0; i < turnType.Length; i++)
            {
                float guessValue = UnityEngine.Random.Range(0, 100);

                if (guessValue <= turnType[i].chance)
                {
                    turnTypeIndex = i;
                    break;
                }
            }
        }
        return turnTypeIndex;
    } 
}
[Serializable]
public enum TurnType {
    New_customer,
    Change_forCustomer,
    Evnet,
    None
}
[Serializable]
public class TurnEvent {
    public string name;
    public float chance;
    public bool disable = false;
    public TurnType type;
}


