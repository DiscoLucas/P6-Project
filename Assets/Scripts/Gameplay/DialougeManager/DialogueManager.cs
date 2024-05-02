using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;



//This Manager is inspired by Bakyes at https://www.youtube.com/watch?v=_nRzoTzeyxU
public class DialogueManager : MonoBehaviour
{
    public UnityEvent dialogDone;
    public UnityEvent sentinceDone;
    public TMP_Text Diatext;
    public TMP_Text nameText;
    public ClientTemplate clietntTemp;
    public ClientData clientData;
    public ClientInfo clientInfo;

    public Animator animator;
    GUIManager guiManager;

    public static DialogueManager instance;
    int currentCaseIndex = -1;
    int currentDialogueIndex = 0;

    [Range(1, 3)]
    [SerializeField] private int textFreq = 2;
    [SerializeField] private float TypeSpeed = 0.04f;
    [HideInInspector]
    public Dialogue dialoguec;
    public DialogueRegistry dialogueRegistry;
    [SerializeField] private string[] VoiceClip;
    public bool hasRun = false;
    public bool isWorking = false;

    [SerializeField] private GameObject gameObject_continue;
    [SerializeField] private GameObject gameObject_end;

    public int nextSentince;


    private void Awake()
    {
        if (dialogueRegistry == null)
        {
            Debug.LogError("No DialogueRegistry was assigned, dumbass");
        }
        clientData = new ClientData(clietntTemp);
        //Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            dialogDone = new UnityEvent();
            sentinceDone = new UnityEvent();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        gameObject_continue.SetActive(true);
        gameObject_end.SetActive(false);
    }

    // DialogueManager.instance.StartDia("dialogue name")
    /// <summary>
    /// The StartDia() takes an int it uses to find the correct string in the DialogueRegistry.sentinsis[] array that contains the dialogue options
    /// </summary>
    /// <param name="registryIndex"></param>
    public void StartDia(int caseIndex)
    {
        if (hasRun)
        {
            DisplayCaseSummary();
            return;
        }
        isWorking = true;
        currentCaseIndex = caseIndex;
        currentDialogueIndex = 0;
        //string sentince = thisSentince(registryIndex);
        DisplayNextSentence();
        
    }


    private void DisplayCaseSummary()
    {
        string caseSum = GameManager.instance.csm.getCurrentCase().caseDiscription;
        DisplayOneSentince(caseSum);
    }
    /*
    public string thisSentince(int caseIndex)
    {
        DialogueRegistry.instance.GetSentincesIndex(caseIndex);
        clientData = GameManager.instance.clm.currentClient;
        string sent = DialogueRegistry.instance.sentinces[caseIndex]; // Get the tag from DialogueRegistry
        string sentince = DialogueRegistry.instance.replaceString(sent, clientData);

        return sentince;
    }*/


    public void DisplayNextSentence()
    {
        if (currentCaseIndex >= 0 && currentCaseIndex < dialogueRegistry.sentinces.Length)
        {
            if ( currentDialogueIndex < dialogueRegistry.sentinces[currentCaseIndex].Length)
            {
                string sentence = dialogueRegistry.sentinces[currentCaseIndex][currentDialogueIndex];
                DisplayOneSentince(sentence);
                currentDialogueIndex++;

                // checking if this was the last sentence of the case
                if (currentDialogueIndex >= dialogueRegistry.sentinces[currentCaseIndex].Length)
                {
                    hasRun = true;
                    EndDialogue();
                }
            }
            else
            {
                   Debug.LogWarning("Dialogue Index out of range for case " + currentCaseIndex);
            }
        }
        else
        {
            Debug.LogWarning("Invalid Case Index: " + currentCaseIndex);
        }


        /* old stuff
        if (hasRun == false)
        {
            string newSentence = thisSentince(nextSentince);
            DisplayOneSentince(newSentence);
            return;
        }
        else if (hasRun)
        {
            EndDialogue();
            DisplayCaseSummary();
            hasRun = true;
            return;
        }*/
    }

    /// <summary>
    /// This function takes a string it inserts in to a Coroutine that writes the dialogue. It is also here the Dialogue box UI opens via an animator,
    /// ClientName is set to be displayed on the correct place on the UI.
    /// The coroutine takes TypeSpeed from the DialogueManager, VoiceClip set on the DialougManager, and Max + Min Pitch set in the ClientData
    /// </summary>
    /// <param name="sentinceToDisplay"></param>
    public void DisplayOneSentince(string sentinceToDisplay)
    {
        if (hasRun == false)
        {
            sentinceDone.Invoke();
        }
        else if (hasRun)
        {
            gameObject_continue.SetActive(false);
            gameObject_end.SetActive(true);
        }
        animator.SetBool("IsOpen", true);
        nameText.text = clientData.clientName;
        StopAllCoroutines();
        StartCoroutine(TypeDia(sentinceToDisplay, TypeSpeed, VoiceClip, clientData.maxPitch, clientData.minPitch));
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
        isWorking= false;
        animator.SetBool("IsOpen", false);
        Debug.Log("End of Dialogue");
    }
}

