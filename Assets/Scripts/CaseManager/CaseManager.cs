using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CaseManager : MonoBehaviour
{
    public Case _case;
    
    [Header("Client Meeting")]
    [SerializeField]
    public Case[] clientMeetingsTemplates;
    public int clientMeetIndex = -1;
    [SerializeField]
    public ClientMeeting currentClientMeeting;

    [SerializeField] public Transform clientMeetingTransform;


    /// <summary>
    /// This function is called from the action menu and start the clietn talked 
    /// It is controlled by the turnT if it should be a client introduction or client metting
    /// </summary>
    public void startConviencation()
    {

        GameManager.instance.guim.showActionMenu();
        TurnType turnT = GameManager.instance.getCurrentTurnType();
        if (turnT == TurnType.Change_forCustomer)
        {
            GameManager.instance.clientMeeting();
        }
        else if (turnT == TurnType.New_customer)
        {
            GameManager.instance.newCustomer();
        }
    }
}
