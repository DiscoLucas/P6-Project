using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;
using TMPro;
using XCharts;
using XCharts.Runtime;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent clientMeetingDone;
    [Header("Managers")]
    public static GameManager instance;
    public ClientManager clm;
    public MarketManager mm;
    public CaseManager csm;
    public GUIManager guim;
    public DialogueManager dlm;

    //DET HER SKAL FIKSES - Case manager skal integreres bedre.
    /*[Header("Client Meeting")]
    [SerializeField]
    public ClientMeetingInfomation[] clientMeetingsTemplates;
    public int clientMeetIndex = -1;
    [SerializeField]
    public ClientMeeting currentClientMeeting;
    [SerializeField] Transform clientMeetingTransform;*/


    [Tooltip("The amount of time that passes on every turn. each unit is 1 month")] 
    public readonly static float timeHorizon = 1f;
    [Tooltip("Time increments calculated in each time horizon.")] 
    public readonly static float dt = timeHorizon / 4f;

    [Header("References")]
    public int startType = 0;
    public TurnEvent[] turnType;
    [SerializeField]
    public int monthNumber = 0;
    private int mn_lastIncedient = -1;
    [SerializeField]
    [Tooltip("controlls how much time where nothing can happend ")]
    private int timeSkipCacth = 12;
    private TurnType turnT;
    [SerializeField]
    GameObject turnCounterObj;
    [SerializeField]
    TMP_Text mountCounter;
    string counterString;

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
        dlm = DialogueManager.instance;
        counterString = mountCounter.text;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space")) {
            //createLoan();
        }
    }

    private void Start()
    {
        updateTurn();
    }

    /// <summary>
    /// This functions update the current turn;
    /// </summary>
    public void updateTurn() 
    {

        newMounth();
    }

    /// <summary>
    /// Decides what should happend this mounth
    /// </summary>
    void newMounth() {
        monthNumber++;
        int turnTypeIndex = decideWhatShouldHappend();
        bool needChange = (MathF.Abs(mn_lastIncedient - monthNumber) > timeSkipCacth);

        mm.simulateIR();

        if (turnType[turnTypeIndex].type == TurnType.New_customer && clm.canGenerateMoreClients && !needChange)
        {
            turnT = TurnType.New_customer;
            //newCustomer();
            mn_lastIncedient = monthNumber;
            showTurnCounter();
        }
        else if (turnType[turnTypeIndex].type == TurnType.Change_forCustomer || needChange)
        {
            turnT = TurnType.Change_forCustomer;
            //clientMeeting();
            mn_lastIncedient = monthNumber;
            showTurnCounter();

        }
        else if (turnType[turnTypeIndex].type == TurnType.Evnet)
        {
            turnT = TurnType.Evnet;
            //markedEvent();
            mn_lastIncedient = monthNumber;
            showTurnCounter();
        }
        else
        {
            //call the next round;
            newMounth();
        }
    }
    /// <summary>
    /// Show the turn counter to the user
    /// </summary>
    void showTurnCounter() {
        string ageString;
        if (monthNumber < 12)
        {
            ageString = $"{monthNumber} Måned";
        }
        else
        {
            int years = monthNumber / 12;
            int remainingMonths = monthNumber % 12;
            ageString = $"{years} år og {remainingMonths} måneder";
        }

        mountCounter.text = ageString;
        turnCounterObj.SetActive(true);
    }

    /// <summary>
    /// Shows The user the start of the turn
    /// </summary>
    public void showStartTurn() {

        turnCounterObj.SetActive(false);
        if (turnT == TurnType.Evnet)
        {
            markedEvent();
        }
        else if (turnT == TurnType.Change_forCustomer || turnT == TurnType.New_customer)
        {
            guim.showActionMenu();

        }
    }

    
    /// <summary>
    /// This functions adds a new customoer
    /// </summary>
    public void newCustomer() {
        if (clm.getClientsTempCount() <= 1)
        {
            clm.canGenerateMoreClients = false;
        }
        Debug.Log("New customer" + " Mounth: " + monthNumber);
        //Decide which client meeting the new customer should start with
        if(clm != null)
        {
            ClientData client = clm.getNewClient();
            csm.createCase(client);
            clm.startClientIntro(client);
            clm.currentClient= client;
        }
    }

    /// <summary>
    /// This functions apply and incident
    /// </summary>
    void markedEvent()
    {
        Debug.Log("MArked event " + " Mounth: " + monthNumber);
        mm.showMarkedEvent();
        //tr�k incident fra incident list
        //k�r incident
    }

    /// <summary>
    /// This function starts a meeting with the clients
    /// </summary>
    public void clientMeeting()
    {
        Debug.Log("Client meeting have startede" + " Mounth: " + monthNumber);
        ClientData client = clm.getrRandomClient();
        csm.currentCaseIndex = UnityEngine.Random.Range(0, csm.currentCases.Count);
        getAndSetnewMeetIndex(client, 10);
        clm.startClientIntro(client);
        clm.currentClient = client;
    }

    public void getAndSetnewMeetIndex(ClientData client, int tryes) {
        if (tryes < 0)
        {
            for (int i = 0; i < csm.currentCases.Count; i++)
            {
                csm.currentCaseIndex = i;
                if (seeIfClientHaveTriedThisMeetingBefore(client)) {
                    return;
                }
            }
        }
        else {
            csm.currentCaseIndex = UnityEngine.Random.Range(0, csm.currentCases.Count);
                if (!seeIfClientHaveTriedThisMeetingBefore(client)) {
                    getAndSetnewMeetIndex(client, tryes - 1);
                }
        }
        
    }

    /// <summary>
    /// TODO:FIX
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    bool seeIfClientHaveTriedThisMeetingBefore(ClientData client)
    {
        bool found = true;

        return found;
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
            Loan loan = mm.checkIfTimeIsUpForLoan();
            if (loan != null) {
                Debug.Log("Change For Loan");
                if (loan.loanAmount == 360) {
                    Debug.Log("Loan Done");
                }
                return turnType.Length - 1;
            }


            for (int i = 0; i < turnType.Length-1; i++)
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
    /// <summary>
    /// This function is called when a clientMeeting have been createde and if there is one already the old on is destoryed and return the currentClient
    /// </summary>
    public ClientData setCurrentClientMeeting(ClientMeeting clientMeeting) {

        csm.currentClientMeeting = clientMeeting;
        return clm.currentClient;
    }
    /// <summary>
    /// Takes the client meeting prefab and create a client meeting from it
    /// </summary>
    /// <param name="prefab"></param>
    public void createClientMeeting(GameObject prefab, ClientData cClient) {
        destoryCurrentClientMeeting();
        clm.currentClient = cClient;
        GameObject obj = Instantiate(prefab,Vector3.zero,quaternion.identity);
        obj.transform.parent = csm.clientMeetingTransform;

    }

    public void closeMeeting() 
    {
        //TODO: let the player create a loan if they want to, through the Loan class.
        //loanManager.CreateLoan(currentClientMeeting.currentClient.clientName, 12);
        destoryCurrentClientMeeting();
        clientMeetingDone.Invoke();
    }

    public void createClientMeeting()
    {
        createClientMeeting(csm.currentCases[csm.currentCaseIndex].getCurrentMeeting().meetingPrefab, clm.getrRandomClient());

    }


    void destoryCurrentClientMeeting() {
        //Log what is need to be logged

        //Destory the current object
        if (csm.currentClientMeeting != null)
        {
            csm.endMeeting();
        }

        if (clm.currentClient != null) {
            clm.currentClient = null;
        }

        if (!clm.ClientObject.active) {
            updateTurn();        
        }
            
    }

    internal TurnType getCurrentTurnType()
    {
        return turnT;
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


