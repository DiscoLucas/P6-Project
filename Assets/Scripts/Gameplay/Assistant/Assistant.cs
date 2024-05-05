using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assistant : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        ResetBool();
        tutorialStart();
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
        tutorialRunning = true;
        intro.Play();
        DialogueManager.instance.nameText.text = assistant_Name;
        DialogueManager.instance.StartDia(dialogueIndex);
        if (GameManager.instance.csm.getCurrentCase().assistantsSentinceUpdate())
        {

        }
    }

    public void tutorialStart()
    {
        if (!tutorialHasPlayed)
        {
            PlayTutorial(tutorialDialogue);
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
        reverseIntro.clip = reverseIntro2;
        reverseIntro.Play();
        tutorialHasPlayed = true;
        Debug.Log("I love you");
    }
}
