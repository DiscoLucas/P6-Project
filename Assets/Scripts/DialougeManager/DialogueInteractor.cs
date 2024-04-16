using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractor : MonoBehaviour
{
    //This script is to set dialogue properties on NPC's
    [Header("Dialogue Details")]
    [Tooltip("Input the Character Name to be displayed on the UI, Set the number of text elements the character and white the diffrent dialogue boxes")]
    public Dialogue dialogue;
    private bool InDialogue = false;


    public void TiggerDialogue()
    {
        DialogueManager.instance.StartDia(dialogue);
    }
}
