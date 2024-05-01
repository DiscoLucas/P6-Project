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

    [Header("Client Meeting")]
    [Header("Managers (Karen Moment)")]
    public static GameManager instance;
    [SerializeField] public ClientManager clm;
    [SerializeField] 
    MarketManager mm;
    [SerializeField]
    private LoanManager loanManager;

    CaseManager csm;

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

    [Header("Menu Stuff")]
    public GameObject action_Menu;
    public GameObject talkClient_BTN, checkComputer_Btn, AskforHelp_btn;

    private void Awake()
    {
        //Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            clientMeetingDone = new UnityEvent();
            loanManager = new LoanManager();
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        counterString = mountCounter.text;
        //loanManager = FindObjectOfType<LoanManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space")) {
            //createLoan();
        }
    }

    private void Start()
    {
        nextMounth();
        //createLoan();
    }

    /// <summary>
    /// This functions update the current turn;
    /// </summary>
    public void updateTurn() 
    {

        //change to the next event
        nextMounth();
    }

    /// <summary>
    /// This function change the mounth and clear out the old 
    /// #TODO: SKRIVES IND I UPDATE TURN
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
            action_Menu.SetActive(true);
            talkClient_BTN.SetActive(true);
        }
    }

    /// <summary>
    /// This function is called from the action menu and start the clietn talked 
    /// It is controlled by the turnT if it should be a client introduction or client metting
    /// </summary>
    public void startConviencation() {
        action_Menu.SetActive(false);
        talkClient_BTN.SetActive(false);
        if (turnT == TurnType.Change_forCustomer)
        {
            clientMeeting();
        } else if (turnT == TurnType.New_customer) {
            newCustomer();
        }
    }

    
    /// <summary>
    /// This functions adds a new customoer
    /// </summary>
    void newCustomer() {
        if (clm.getClientsTempCount() <= 1)
        {
            clm.canGenerateMoreClients = false;
        }
        Debug.Log("New customer" + " Mounth: " + monthNumber);
        //Decide which client meeting the new customer should start with
        if(clm != null)
        {
            ClientData client = clm.getNewClient();
            csm.clientMeetIndex = client.firstCaseIndex;
            clm.startClientIntro(client);
            clm.currentClient= client;
            if(!csm.clientMeetingsTemplates[csm.clientMeetIndex].canBeUsedMoreThanOnes)
                csm.clientMeetingsTemplates[csm.clientMeetIndex].clientThatHaveUsed.Add(client.clientName);
        }
        //createClientMeeting(clientMeetingPrefabs[0]);
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
        csm.clientMeetIndex = UnityEngine.Random.Range(0, csm.clientMeetingsTemplates.Length);
        getAndSetnewMeetIndex(client, 10);
        clm.startClientIntro(client);
        clm.currentClient = client;
        if (!csm.clientMeetingsTemplates[csm.clientMeetIndex].canBeUsedMoreThanOnes)
            csm.clientMeetingsTemplates[csm.clientMeetIndex].clientThatHaveUsed.Add(client.clientName);

        //Client walks in, like in newCustomer function
        //Client choses speech that revolves around getting update to bonds "Hey jeg har f�et bedre arbejde lol"
        //Player gets to fill out the correct paper work - Dette slutter af med en ja/nej
        //Client g�r som player siger
    }

    public void getAndSetnewMeetIndex(ClientData client, int tryes) {
        if (tryes < 0)
        {
            for (int i = 0; i < csm.clientMeetingsTemplates.Length; i++)
            {
                csm.clientMeetIndex = i;
                if (seeIfClientHaveTriedThisMeetingBefore(client)) {
                    return;
                }
            }
        }
        else {
            csm.clientMeetIndex = UnityEngine.Random.Range(0, csm.clientMeetingsTemplates.Length);
            if (csm.clientMeetingsTemplates[csm.clientMeetIndex].canBeUsedMoreThanOnes)
            {
                return;
            }else {
                if (!seeIfClientHaveTriedThisMeetingBefore(client)) {
                    getAndSetnewMeetIndex(client, tryes - 1);
                }
            }
        }
        
    }

    bool seeIfClientHaveTriedThisMeetingBefore(ClientData client)
    {
        bool found = true;
        for (int i = 0; i < csm.clientMeetingsTemplates[csm.clientMeetIndex].clientThatHaveUsed.Count; i++)
        {
            if (csm.clientMeetingsTemplates[csm.clientMeetIndex].clientThatHaveUsed[i] == client.clientName)
            {
                found = false; break;
            }
        }

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
            if (loan == null) {
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
    public void Method()
    {
        throw new System.NotImplementedException(); }
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
        createClientMeeting(csm.clientMeetingsTemplates[csm.clientMeetIndex].meetingPrefab, clm.getrRandomClient());

    }


    void destoryCurrentClientMeeting() {
        //Log what is need to be logged

        //Destory the current object
        if (csm.currentClientMeeting != null)
        {
            Destroy(csm.currentClientMeeting.gameObject);
        }

        if (clm.currentClient != null) {
            clm.currentClient = null;
        }
        csm.clientMeetIndex = -1;

        if (!clm.ClientObject.active) {
            updateTurn();        
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


