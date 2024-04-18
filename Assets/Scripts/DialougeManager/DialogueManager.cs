using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;



//This Manager is inspired by Bakyes at https://www.youtube.com/watch?v=_nRzoTzeyxU
public class DialogueManager : MonoBehaviour
{
    public UnityEvent dialogDone;
    public TMP_Text Diatext;
    public TMP_Text nameText;
    public ClientTemplate clietntTemp;
    public ClientData clientData;
    public ClientInfo clientInfo;


    public Animator animator;

    private Queue<string> sentensis;
    public static DialogueManager instance;

    [Range(1, 3)]
    [SerializeField] private int textFreq = 2;
    [SerializeField] private float TypeSpeed = 0.04f;
    [HideInInspector]
    public Dialogue dialoguec;
    public DialogueRegistry dialogueRegistry;
    [SerializeField] private string[] VoiceClip;
    public bool hasRun = false;



    private void Awake()
    {
        
        clientData = new ClientData(clietntTemp);
        //Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            dialogDone = new UnityEvent();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        sentensis = new Queue<string>();
    }

    // DialogueManager.instance.StartDia("dialogue name")
    /// <summary>
    /// The StartDia() takes an int it uses to find the correct string in the DialogueRegistry.sentinsis[] array that contains the dialogue options
    /// </summary>
    /// <param name="registryIndex"></param>
    public void StartDia(int registryIndex)
    {
        DialogueRegistry.instance.GetSentincesIndex(registryIndex);
        string sentince = DialogueRegistry.instance.sentinces[registryIndex]; // Get the tag from DialogueRegistry
        //Debug.Log(sentince);
        //string[] sentinces = (registryIndex, DialogueRegistry.instance.sentinces);
        //string[] sentinces = DialogueRegistry.instance.sentinces; // Get the sentences array based on the tag
        //StartDialogue(sentinces);
        //Debug.Log(sentinces);
        DisplayOneSentince(sentince);

    }

    // Method to start dialogue with the given sentences
    public void StartDialogue(string[] sentences)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = clientData.clientName;

        sentensis.Clear();

        foreach (string sentence in sentences)
        {
            sentensis.Enqueue(sentence);
        }

        DisplayNextScentence();
    }

    private void DisplayCaseSummary()
    {
        string caseSum = clientInfo.caseDescription;
        DisplayOneSentince(caseSum);
    }

    public void DisplayNextScentence()
    {
        if (hasRun == false) //if the queue created by Endqueue() reaches 0 EndDialogue() is called.
        {
            DialogueRegistry.instance.GetSentincesIndex(DialogueRegistry.instance.GetIndex());
            string newSentence = DialogueRegistry.instance.sentinces[DialogueRegistry.instance.GetIndex()];
            DisplayOneSentince(newSentence);
            return;
        }
        else if (hasRun)
        {
            DisplayCaseSummary();
            hasRun = true;
            return;
        }
    }

    /// <summary>
    /// This function takes a string it inserts in to a Coroutine that writes the dialogue. It is also here the Dialogue box UI opens via an animator,
    /// ClientName is set to be displayed on the correct place on the UI.
    /// The coroutine takes TypeSpeed from the DialogueManager, VoiceClip set on the DialougManager, and Max + Min Pitch set in the ClientData
    /// </summary>
    /// <param name="sentinceToDisplay"></param>
    public void DisplayOneSentince(string sentinceToDisplay)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = clientData.clientName;
        StopAllCoroutines(); //This stpos Coroutines so that the animation text writing animation dosen't break.
        //Debug.Log(sentinceToDisplay);
        StartCoroutine(TypeDia(sentinceToDisplay, TypeSpeed, VoiceClip, clientData.maxPitch, clientData.minPitch)); //Runs the IEnumerator TypeDia for the text writing animation.
    }

    /// <summary>
    /// This Coroutine takes multible paramiters, a sting: sentence, a TypeSpeed that is used to set how fast each letter appears on the UI, 
    /// an array of voice clips a second corutine uses along with max + min pitch.
    /// The Display Text is set to be ___ and a loop creates an array for each letter in the sentince. This loop adds 1 letter each time, and waits for "Typespeed" seconds before looping.
    /// </summary>
    /// <param name="sentence"></param>
    /// <param name="TypeSpeed"></param>
    /// <param name="VoiceClip"></param>
    /// <param name="maxPitch"></param>
    /// <param name="minPitch"></param>
    /// <returns></returns>
    IEnumerator TypeDia (string sentence, float TypeSpeed, string[] VoiceClip, float maxPitch, float minPitch) 
    {
        Diatext.text = ""; //Creates a blank text space when the DiaText starts.
        int i = 0;
        foreach (char letter in sentence.ToCharArray()) // Stores each letter of the sentince in an Array[]
        {
            PlayCharSound(i, VoiceClip, maxPitch, minPitch);
            Diatext.text += letter; //Addes a letter from the Array[]
            i++;
            yield return new WaitForSeconds(TypeSpeed);
        }
    }

    private void PlayCharSound(int currentDisplayedCharacterCount, string[] VoiceClip, float maxPitch, float minPitch)
    {
        //Debug.Log(currentDisplayedCharacterCount);
        if (currentDisplayedCharacterCount % textFreq == 0)
        {
            string vClip = VoiceClip[Random.Range(0, VoiceClip.Length)];
            AudioManager.instance.DisableAudioSource(vClip);
            //Debug.Log(vClip);

            float randomPitch = Random.Range(clientData.minPitch, clientData.maxPitch);
            Voice v = AudioManager.instance.GetVoiceClip(vClip);
            v.source.pitch = randomPitch;
            AudioManager.instance.PlayVoice(vClip); // this adds the voice clip for the charector
            //Debug.Log("PlayCharSound is working " + currentDisplayedCharacterCount);
        }
    }

    /// <summary>
    /// Thus function is to close the dialogue UI
    /// </summary>
    public void EndDialogue() //All this does is change the animation state of the Dialogue Plane / Canvas
    {
        dialogDone.Invoke();
        animator.SetBool("IsOpen", false);
    }
}


/*
public void StartDia(DialogueRegistry dialogue)
{
    this.dialogueRegistry = dialogue;
    animator.SetBool("IsOpen", true); //This is for controlling the animaton state of the dialogue canvas.
    nameText.text = clientData.clientName;  //This replaces the "Name" text field on the dialogue canvas.
    DialogueRegistry.instance.GetSentincesIndex(DialogueRegistry.instance.GetIndex());

    sentensis.Clear();

    if (dialoguec.hasRun)
    {
        DisplayCaseSummary();
        return;
    }

    foreach (string sentince in dialogueRegistry.sentinces) //Takes each sentince variable in "sentinsis" array from the dialogue class and Enqueues them.
    {
        sentensis.Enqueue(sentince);
    }

    DisplayNextScentence();

        if (sentensis.Count == 0) //if the queue created by Endqueue() reaches 0 EndDialogue() is called.
    {
        dialoguec.hasRun = true;
        EndDialogue();
        return;
    }

    string sentince = sentensis.Dequeue(); //Dequeues the "sentinsis" array so it goes further down.
    StopAllCoroutines(); //This stpos Coroutines so that the animation text writing animation dosen't break.
    Debug.Log(sentince);
    StartCoroutine(TypeDia(sentince, TypeSpeed, VoiceClip, clientData.maxPitch, clientData.minPitch)); //Runs the IEnumerator TypeDia for the text writing animation.

    //Debug.Log("Next Sentince");



        string sentince = sentensis.Dequeue(); //Dequeues the "sentinsis" array so it goes further down.
        StopAllCoroutines(); //This stpos Coroutines so that the animation text writing animation dosen't break.
        Debug.Log(sentince);
        StartCoroutine(TypeDia(sentince, TypeSpeed, VoiceClip, clientData.maxPitch, clientData.minPitch)); //Runs the IEnumerator TypeDia for the text writing animation.

        //Debug.Log("Next Sentince");
}
*/
