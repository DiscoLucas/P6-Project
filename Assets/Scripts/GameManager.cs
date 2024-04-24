using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent clientMeetingDone;
    [Header("Managers (Karen Moment)")]
    public static GameManager instance;
    [SerializeField] public ClientManager cm;
    [SerializeField] 
    MarketManager mm;
    [SerializeField]
    private LoanManager loanManager;
    [Header("Client Meeting")]
    [SerializeField]
    public ClientMeetingInfomation[] clientMeetingsTemplates;
    public int clientMeetIndex = -1;
    [SerializeField]
    public ClientMeeting currentClientMeeting;
    [SerializeField] Transform clientMeetingTransform;

    [Tooltip("The amount of time that passes on every turn. each unit is 1 month")] 
    public readonly static float timeHorizon = 1f;
    [Tooltip("Time increments calculated in each time horizon.")] 
    public readonly static float dt = timeHorizon / 4f;

    [Header("References")]
    //forslag:
    //[SerializeField]
    //Incidents currentInciden
    //[SerializeField] List<Incident> Incidents incidents;
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

        counterString = mountCounter.text;
        //loanManager = FindObjectOfType<LoanManager>();
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
    public void updateTurn() 
    {

        //change to the next event
        nextMounth();
    }

    private void SimulateIR() // Todo: handle case where loan is fixed or paid off
    {
        try
        {
            // iterate over each loan and update the interest rate
            foreach (var kvp in loanManager.loanDict)
            {
                string clientName = kvp.Key;
                Loan loan = kvp.Value;

                // if the the interest history is not empty, use the last value as the current rate
                if (loan.IRForTime.Count != 0)
                {
                    IRModel_HullWhite model = new IRModel_HullWhite(loan.IRForTime.Last(), loan.volatility, loan.longTermRate);
                    loanManager.UpdateIR(clientName, model.PredictIRforTimeInterval(dt, timeHorizon));
                }
                else // if the interest history is empty, use the initial interest rate
                {
                    IRModel_HullWhite model = new IRModel_HullWhite(loan.interestRate, loan.volatility, loan.longTermRate); // TODO: add market modifier to the parameters
                    loanManager.UpdateIR(clientName, model.PredictIRforTimeInterval(dt, timeHorizon));
                }

            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error in SimulateIR, there probably wasn't any loans in the dictionary: " + e.Message);
        }
        
    }

    /// <summary>
    /// This function change the mounth and clear out the old
    /// </summary>
    public void nextMounth() {
        //Log what needs to be logged

        //Update Ir
        try {
            SimulateIR();
        }
        catch (Exception e) {
            Debug.LogError(e.ToString());
        }
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
        if (turnType[turnTypeIndex].type == TurnType.New_customer && cm.canGenerateMoreClients && !needChange)
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
        if (turnT == TurnType.Evnet) {
            markedEvent();
        } else if (turnT == TurnType.Change_forCustomer) {
            clientMeeting();
        } else if (turnT == TurnType.New_customer) {
            newCustomer();
        }
    }

    /// <summary>
    /// This functions adds a new customoer
    /// </summary>
    void newCustomer() {
        if (cm.getClientsTempCount() <= 1)
        {
            cm.canGenerateMoreClients = false;
        }
        Debug.Log("New customer" + " Mounth: " + monthNumber);
        //Decide which client meeting the new customer should start with
        if(cm != null)
        {
            ClientData client = cm.getNewClient();
            clientMeetIndex = client.firstCaseIndex;
            cm.startClientIntro(client);
            cm.currentClient= client;
            if(!clientMeetingsTemplates[clientMeetIndex].canBeUsedMoreThanOnes)
                clientMeetingsTemplates[clientMeetIndex].clientThatHaveUsed.Add(client.clientName);
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
    void clientMeeting()
    {
        Debug.Log("Client meeting have startede" + " Mounth: " + monthNumber);
        ClientData client = cm.getrRandomClient();
        clientMeetIndex = UnityEngine.Random.Range(0, clientMeetingsTemplates.Length);
        getAndSetnewMeetIndex(client, 10);
        cm.startClientIntro(client);
        cm.currentClient = client;
        if (!clientMeetingsTemplates[clientMeetIndex].canBeUsedMoreThanOnes)
            clientMeetingsTemplates[clientMeetIndex].clientThatHaveUsed.Add(client.clientName);

        //Client walks in, like in newCustomer function
        //Client choses speech that revolves around getting update to bonds "Hey jeg har f�et bedre arbejde lol"
        //Player gets to fill out the correct paper work - Dette slutter af med en ja/nej
        //Client g�r som player siger
    }

    public void getAndSetnewMeetIndex(ClientData client, int tryes) {
        if (tryes < 0)
        {
            for (int i = 0; i < clientMeetingsTemplates.Length; i++)
            {
                clientMeetIndex = i;
                if (seeIfClientHaveTriedThisMeetingBefore(client)) {
                    return;
                }
            }
        }
        else {
            clientMeetIndex = UnityEngine.Random.Range(0, clientMeetingsTemplates.Length);
            if (clientMeetingsTemplates[clientMeetIndex].canBeUsedMoreThanOnes)
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
        for (int i = 0; i < clientMeetingsTemplates[clientMeetIndex].clientThatHaveUsed.Count; i++)
        {
            if (clientMeetingsTemplates[clientMeetIndex].clientThatHaveUsed[i] == client.clientName)
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
    /// This function is called when a clientMeeting have been createde and if there is one already the old on is destoryed and return the currentClient
    /// </summary>
    public ClientData setCurrentClientMeeting(ClientMeeting clientMeeting) {
        
        currentClientMeeting = clientMeeting;
        return cm.currentClient;
    }
    /// <summary>
    /// Takes the client meeting prefab and create a client meeting from it
    /// </summary>
    /// <param name="prefab"></param>
    public void createClientMeeting(GameObject prefab, ClientData cClient) {
        destoryCurrentClientMeeting();
        cm.currentClient = cClient;
        GameObject obj = Instantiate(prefab,Vector3.zero,quaternion.identity);
        obj.transform.parent = clientMeetingTransform;

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
        createClientMeeting(clientMeetingsTemplates[clientMeetIndex].meetingPrefab, cm.getrRandomClient());

    }


    void destoryCurrentClientMeeting() {
        //Log what is need to be logged

        //Destory the current object
        if (currentClientMeeting != null)
        {
            Destroy(currentClientMeeting.gameObject);
        }

        if (cm.currentClient != null) {
            cm.currentClient = null;
        }
        clientMeetIndex = -1;

        if (!cm.ClientObject.active) {
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


