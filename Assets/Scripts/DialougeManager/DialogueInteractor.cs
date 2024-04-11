using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractor : MonoBehaviour
{
    //[Header("Character Voice")]
    //[Tooltip("Write the name of the audio clip that needs to be played")]
    //public Dialogue VoiceClip;
    [Header("Dialogue Details")]
    [Tooltip("Input the Character Name to be displayed on the UI, Set the number of text elements the character and white the diffrent dialogue boxes")]
    public Dialogue dialogue;
    private bool InDialogue = false;


    public void TiggerDialogue()
    {
        DialogueManager.instance.StartDia(dialogue);
    }

    /*
    public override void interaction()
    {
        //DialogueManager.instance.StartDia(dialogue);
        if (!InDialogue)
        {
            TiggerDialogue();
            //AudioManager.instance.PlayVoice(dialogue.VoiceClip);
            InDialogue = true;
        }
        else
        {
            DialogueManager.instance.DisplayNextScentence();
            //AudioManager.instance.PlayVoice(dialogue.VoiceClip);
        }

    }

    public override void outOfReach()
    {
        InDialogue = false;
        base.outOfReach();
        DialogueManager.instance.EndDialogue();
    }
    */

}
