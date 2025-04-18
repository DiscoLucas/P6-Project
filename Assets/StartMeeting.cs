using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartMeeting : MonoBehaviour
{
    public GameObject interactionLight;
    public UnityEvent onClick;
    public bool zoomedIn = false;
    public GameObject mountCounter, endTurn;
    private void OnMouseOver()
    {
        if (!zoomedIn && !DialogueManager.instance.dialogueVissible && 
        !GameManager.instance.meetingOngoing && 
        GameManager.instance.assistant.tutorialHasPlayed && 
        !GameManager.instance.assistant.tutorialRunning && 
        !mountCounter.active && 
        !endTurn.active)
        {
            interactionLight.SetActive(true);
            if (Input.GetMouseButtonDown(0)) 
            { //WHEN IT CLICK
                interactionLight.SetActive(false);
                onClick.Invoke();
                zoomedIn = true;
            }
        }
    }

    private void OnMouseExit()
    {
        interactionLight.SetActive(false);
        zoomedIn = false;
    }
}
