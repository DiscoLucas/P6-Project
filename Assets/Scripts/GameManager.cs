using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent clientMeetingDone;
    public static GameManager instance;
    [SerializeField] ClientManager cm;

    //forslag:
    //[SerializeField]
    //Incidents currentIncident

    [SerializeField]
    GameObject[] clientMeetingPrefabs;
    [SerializeField]
    public ClientMeeting currentClientMeeting;
    [SerializeField] Transform clientMeetingTransform;
    //[SerializeField] List<Incident> Incidents incidents;
    public int startType = 0;
    public TurnEvent[] turnType;
    public int monthNumber = 0;

    private void Awake()
    {
        //Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            clientMeetingDone = new UnityEvent();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("space")) {
            nextMounth();
        }
    }

    private void Start()
    {
        nextMounth();
    }

    /// <summary>
    /// This functions update the current turn;
    /// </summary>
    public void updateTurn() {
        //change to the next event
        nextMounth();
    }

    /// <summary>
    /// This function change the mounth and clear out the old
    /// </summary>
    public void nextMounth() {
        //Log what needs to be logged


        //Change the mounth
        newMounth();
    }

    /// <summary>
    /// Decides what should happend this mounth
    /// </summary>
    void newMounth() {
        monthNumber++;
        Debug.Log("#######################");

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
            newMounth();
        }
        //Make the somthing that shows what mounth the user are in
        Debug.Log("Mounth: " + monthNumber);
    }

    /// <summary>
    /// This functions adds a new customoer
    /// </summary>
    void newCustomer() {
        Debug.Log("New customer");
        if(cm != null)
        {
            ClientData client = cm.getNewClient();
            cm.startClientIntro(client);

        }
        //createClientMeeting(clientMeetingPrefabs[0]);
    }

    /// <summary>
    /// This functions apply and incident
    /// </summary>
    void Incident()
    {
        Debug.Log("A racing incident");
        //træk incident fra incident list
        //kør incident
    }

    /// <summary>
    /// This function starts a meeting with the clients
    /// </summary>
    void clientMeeting()
    {
        Debug.Log("Client meeting have startede");
        createClientMeeting(clientMeetingPrefabs[UnityEngine.Random.Range(0,clientMeetingPrefabs.Length)]);
        //Client walks in, like in newCustomer function
        //Client choses speech that revolves around getting update to bonds "Hey jeg har fået bedre arbejde lol"
        //Player gets to fill out the correct paper work - Dette slutter af med en ja/nej
        //Client gør som player siger
    }

    /// <summary>
    /// This function decide what should happend this mount
    /// TODO: Make it smarter so it does not chooese random. fx if a loan have expired it should start a meeting
    /// </summary>
    /// <returns></returns>
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
    public void Method()
    {
        throw new System.NotImplementedException(); }
    /// <summary>
    /// This function is called when a clientMeeting have been createde and if there is one already the old on is destoryed
    /// </summary>
    public void setCurrentClientMeeting(ClientMeeting clientMeeting) {
        
        currentClientMeeting = clientMeeting;
    }
    /// <summary>
    /// Takes the client meeting prefab and create a client meeting from it
    /// </summary>
    /// <param name="prefab"></param>
    public void createClientMeeting(GameObject prefab) {
        destoryCurrentClientMeeting();
        GameObject obj = Instantiate(prefab,Vector3.zero,quaternion.identity);
        obj.transform.parent = clientMeetingTransform;

    }

    public void closeMeeting() {
        destoryCurrentClientMeeting();
        clientMeetingDone.Invoke();
    }

    public void createClientMeeting()
    {

        createClientMeeting(clientMeetingPrefabs[0]);

    }


    void destoryCurrentClientMeeting() {
        //Log what is need to be logged

        //Destory the current object
        if (currentClientMeeting != null)
        {
            Destroy(currentClientMeeting.gameObject);
        }
    }
}

/// <summary>
/// This enum is the different types of event that can happend each mouth
/// </summary>
[Serializable]
public enum TurnType {
    New_customer,
    Change_forCustomer,
    Evnet,
    None
}

/// <summary>
/// This is the calls that have all the infomation about the different event of each mounth
/// </summary>
[Serializable]
public class TurnEvent {
    public string name;
    public float chance;
    public bool disable = false;
    public TurnType type;
}


