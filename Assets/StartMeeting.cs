using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartMeeting : MonoBehaviour
{
    public GameObject light;
    public UnityEvent onClick;
    public bool zoomedIn = false;
    public GameObject mountCounter, endTurn;
    private void OnMouseOver()
    {
        if (!zoomedIn && !DialogueManager.instance.dialogueVissible && !GameManager.instance.meetingOngoing && GameManager.instance.assistant.tutorialHasPlayed&& !mountCounter.active&& !endTurn.active)
        {
            Debug.Log("M Over ");
            light.SetActive(true);
            if (Input.GetMouseButtonDown(0)) 
            { //WHEN IT CLICK
                light.SetActive(false);
                onClick.Invoke();
                zoomedIn = true;
            }
        }
    }

    private void OnMouseExit()
    {
        light.SetActive(false);
        zoomedIn = false;
    }
}
