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
}
