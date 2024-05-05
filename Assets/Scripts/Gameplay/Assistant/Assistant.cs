using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Assistant : MonoBehaviour
{
    public UnityEvent turtialDone;
    public GameManager gameManager;
    TurnType turnType;
    TurnEvent turnEvent;
    [Header("Components")]
    public Animation intro;
    public Animation reverseIntro;
    public AnimationClip reverseIntro2;

    [Header("Dialogues")]
    public int dialogueToSay;
    public int tutorialDialogue = 0;
    [SerializeField] string assistant_Name = "Saul";

    [Header("Tutorial Bools")]
    public bool tutorialHasPlayed = false;
    public bool firstTimeMakeLoan = false;
    public bool firstTimeBuyout = false;
    public bool firstTimeConvert = false;
    public bool tutorialRunning = false;


    public ClientData assisentData;
    public int currentIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        ResetBool();
        DialogueManager.instance.dialogDone.AddListener(endAssistantTalk);
    }

    void ResetBool()
    {
        tutorialHasPlayed = false;
        firstTimeMakeLoan = false;
        firstTimeBuyout = false;
        firstTimeConvert = false;
    }

    void PlayTutorial(int dialogueIndex)
    {
        currentIndex = dialogueIndex;
        intro.Play();
    }

    public void startTurtialText(){
        DialogueManager.instance.clientData = assisentData;
        tutorialRunning = true;
        DialogueManager.instance.nameText.text = assistant_Name;
        DialogueManager.instance.StartDia(currentIndex);
        currentIndex =-1;
        if (GameManager.instance.csm.getCurrentCase().assistantsSentinceUpdate())
        {

        }
    }

    public void playSpecificTutorial(int tutorialNR)
    {
        PlayTutorial(tutorialNR);
        turnEvent.tutorial = true;
    }

    public bool checkTurnTypeBool()
    {
        if (turnEvent.tutorial == true)
        {
            return true;
        }else 
        return false;
    }

    public void tutorialStart()
    {
        if (!tutorialHasPlayed)
        {
            PlayTutorial(tutorialDialogue);
            newLoanTutorial();
            buyOutTutorial();
            convertionTutorial();
        }
    }

    public void newLoanTutorial()
    {
        PlayTutorial(2);
        firstTimeMakeLoan = true;
    }

    public void buyOutTutorial()
    {
        PlayTutorial(5);
        firstTimeBuyout = true;
    }

    public void convertionTutorial()
    {
        PlayTutorial(7);
        firstTimeConvert = true;
    }

    public bool checkIfTutorialDone()
    {
        return false;
    }

    public void endAssistantTalk()
    {
        if (!tutorialHasPlayed) {
            reverseIntro.clip = reverseIntro2;
            reverseIntro.PlayQueued(reverseIntro2.name);
            reverseIntro.Play();
            tutorialHasPlayed = true;
            Debug.Log("I love you");
            turtialDone.Invoke();
        }
    }
}
