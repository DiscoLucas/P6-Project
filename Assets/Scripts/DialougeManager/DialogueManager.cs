using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



//This Manager is inspired by Bakyes at https://www.youtube.com/watch?v=_nRzoTzeyxU
public class DialogueManager : MonoBehaviour
{
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



    private void Awake()
    {
        clientData = new ClientData(clietntTemp);
        //Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        sentensis = new Queue<string>(); //This creates the "List" of dialogue on a given NPC
    }

    // DialogueManager.instance.StartDia("dialogue name")
    public void StartDia(Dialogue dialogue)
    {
        this.dialoguec = dialogue;
        animator.SetBool("IsOpen", true); //This is for controlling the animaton state of the dialogue canvas.
        //Debug.Log("Srat " + dialogue.name);

        Debug.Log(clientData.clientName);
        //dialogue.name = clientData.clientName; //This replaces the "Name" text field on the dialogue canvas.
        nameText.text = clientData.clientName;

        sentensis.Clear();

        if (dialoguec.hasRun)
        {
            DisplayCaseSummary();
            return;
        }

        foreach (string sentince in dialogue.sentensis) //Takes each sentince variable in "sentinsis" array from the dialogue class and Enqueues them.
        {
            sentensis.Enqueue(sentince);
        }
        
        DisplayNextScentence();
    }

    private void DisplayCaseSummary()
    {
        sentensis.Enqueue(clientInfo.caseDescription);
        DisplayNextScentence();
    }

    public void DisplayNextScentence()
    {
        if (sentensis.Count == 0) //if the queue created by Endqueue() reaches 0 EndDialogue() is called.
        {
            dialoguec.hasRun = true;
            EndDialogue();
            return;
        }

        string sentince = sentensis.Dequeue(); //Dequeues the "sentinsis" array so it goes further down.
        StopAllCoroutines(); //This stpos Coroutines so that the animation text writing animation dosen't break.
        StartCoroutine(TypeDia(sentince, TypeSpeed, dialoguec.VoiceClip, clientData.maxPitch, clientData.minPitch)); //Runs the IEnumerator TypeDia for the text writing animation.
        //Debug.Log(sentince);
        Debug.Log("Next Sentince");
    }

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

            float randomPitch = Random.Range(clientData.minPitch, clientData.maxPitch);
            Voice v = AudioManager.instance.GetVoiceClip(vClip);
            v.source.pitch = randomPitch;
            AudioManager.instance.PlayVoice(vClip); // this adds the voice clip for the charector
            //Debug.Log("PlayCharSound is working " + currentDisplayedCharacterCount);
        }
    }

    public void EndDialogue() //All this does is change the animation state of the Dialogue Plane / Canvas
    {
        animator.SetBool("IsOpen", false);
        //Debug.Log("End");
    }
}
