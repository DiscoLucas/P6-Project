using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartMeeting : MonoBehaviour
{
    public GameObject light;
    public UnityEvent onClick;
    bool zoomedIn = false;
    private void OnMouseOver()
    {
        if (!zoomedIn && !DialogueManager.instance.dialogueVissible)
        {
            light.SetActive(true);
            if (Input.GetMouseButtonDown(0) && GameManager.instance.guim.talkClient_BTN) ;
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
    }
}
