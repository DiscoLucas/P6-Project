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
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
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
    public CommentSystem cm;
    public MailSystem ms;

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
    public int endSceneIndex = 0;
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
    public float points = 0;
    public Assistant assistant;
    public Vector2 pointsCol ;
    public TurnEvent turnEvent;
    public bool meetingOngoing = false;
    public string introductionMailHeader = "Introduktion";
    public float chanceToscipConvertion = 0.4f;
    private void Awake()
    {
        //Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject.transform.parent);
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
        assistant.turtialDone.AddListener(turtroialDone);
    }

    public void turtroialDone() {
        if (monthNumber == 1) {
            guim.showActionMenu();
        }
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
        bool needChange = (MathF.Abs(mn_lastIncedient - monthNumber) > timeSkipCacth);
        mm.simulateIR();

        Case @case = csm.getCasesThatNeedUpdate(monthNumber);
        int turnTypeIndex = 0;
        if (@case != null)
        {
            if (@case.loan.fixedIR || @case.loan.lastPeriod )
            {
                Debug.Log("CHANGE FOR CASE!!");
                turnT = TurnType.Change_forCustomer;
                mn_lastIncedient = monthNumber;
                showTurnCounter();
                return;
            }
            else {
                float roll = UnityEngine.Random.Range(0.0f, 1f);
                bool needToconvert = (roll > chanceToscipConvertion);
                Debug.Log("Need to convert?: " + needToconvert + " the roll was: " + roll);
                if (needToconvert)
                {
                    Debug.Log("CHANGE FOR CASE!!");
                    turnT = TurnType.Change_forCustomer;
                    mn_lastIncedient = monthNumber;
                    showTurnCounter();
                    return;
                }
                else {
                    @case.loan.convertLoan(monthNumber, @case.loan.loanTypes.loanTime, @case.loan.IRForTime[@case.loan.IRForTime.Count - 1], @case.loan.loanTypes.volatility, @case.loan.loanTypes);
                }
            }

        }
        else {
            turnTypeIndex = decideWhatShouldHappend();
        }
        

        if (needChange)
        {

            if (clm.canGenerateMoreClients)
            {
                turnT = TurnType.New_customer;
                mn_lastIncedient = monthNumber;
                showTurnCounter();
            }
            else
            {
                if ((MathF.Abs(mn_lastIncedient - monthNumber) > timeSkipCacth))
                {
                    turnT = TurnType.Evnet;
                    mn_lastIncedient = monthNumber;
                    showTurnCounter();
                }
                else {
                    newMounth();
                }
            }


        }else if (turnType[turnTypeIndex].type == TurnType.New_customer && clm.canGenerateMoreClients && !needChange)
        {
            turnT = TurnType.New_customer;
            mn_lastIncedient = monthNumber;
            showTurnCounter();
        }
        else if (turnType[turnTypeIndex].type == TurnType.Evnet)
        {
            turnT = TurnType.Evnet;
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
            if (monthNumber == 1)
            {
                assistant.tutorialStart();
            }
            else
            {
                guim.showActionMenu();
            }
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
            DialogueManager.instance._mailtext.header = introductionMailHeader;
            DialogueManager.instance.addToMail = true;
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
        Debug.Log("Client meeting have startede");
        Case c = csm.getCurrentCase();
        clm.currentClient = c.client;
        DialogueManager.instance.clientData = c.client;
        /*
        int turnTypeIndex = 0;
        turnTypeIndex = decideWhatShouldHappend();
        TurnType turnT = getCurrentTurnType();
        if (!assistant.checkTurnTypeBool(turnT))
        {
            assistant.playSpecificTutorial(turnEvent.tutorialNR);
        }else
        */

        bool haveCompletede = assistant.turtoialID[c.getCurrentMeeting().turtoialIndex].haveCompletede;
        assistant.turtoialID[c.getCurrentMeeting().turtoialIndex].haveCompletede = true;
        Debug.Log("Have the tutroial completede: " + haveCompletede);
        if (haveCompletede)
        {
            Debug.Log("client intro startet: " + c.client);
            clm.startClientIntro(c.client);
        }
        else {
            Debug.Log("This part of turtoual done: "+ assistant.turtoialID[c.getCurrentMeeting().turtoialIndex].haveCompletede);
            assistant.turtoialID[c.getCurrentMeeting().turtoialIndex] = assistant.startTurtoialCheck(assistant.turtoialID[c.getCurrentMeeting().turtoialIndex]);
        }
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
    public Case setCurrentCase(ClientMeeting clientMeeting) {

        csm.currentClientMeeting = clientMeeting;
        meetingOngoing = true;
        return csm.currentCases[csm.currentCaseIndex];
    }
    /// <summary>
    /// Takes the client meeting prefab and create a client meeting from it
    /// </summary>
    /// <param name="prefab"></param>
    public void createClientMeeting(GameObject prefab, ClientData cClient) {
        destoryCurrentClientMeeting();
        clm.currentClient = cClient;
        GameObject obj = Instantiate(prefab, csm.clientMeetingTransform);
        Debug.Log("Meeting have been instantiatet it parrent is: " + obj.transform.parent.name + " obj is " + obj.name);

    }

    public void closeMeeting() 
    {
        //TODO: let the player create a loan if they want to, through the Loan class.
        //loanManager.CreateLoan(currentClientMeeting.currentClient.clientName, 12);
        destoryCurrentClientMeeting();
        guim.hideMeetingPopUp();
        clientMeetingDone.Invoke();
    }

    public void createClientMeeting()
    {
        Debug.Log("Creating meeting");
        var currentCase = csm.currentCases[csm.currentCaseIndex];
        var cureentMeeting = currentCase.getCurrentMeeting();
        var meetingprefab = cureentMeeting.meetingPrefab;
        Debug.Log("not null[\nCurrent case: " + (currentCase != null) + "\nprefab: " + (meetingprefab != null) + "\n]");
        createClientMeeting(meetingprefab, currentCase.client);
        guim.showMeetingPopUp();

    }


    void destoryCurrentClientMeeting() {
        meetingOngoing = false;
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

    public void endGame() {
        pointsCol = new Vector2(cm.score, cm.bestPossibleScore);
        SceneManager.LoadScene(endSceneIndex);
    }

    public void destoryAllManagers() {
        instance = null;
        Destroy(transform.parent);
}

    void tryTodestoy(GameObject obj) {
        try
        {
            Destroy(obj);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
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
    public bool tutorial = false;
    public int tutorialNR;
}


