using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ActivateComputerBehavoir : MonoBehaviour
{
    public GameObject light;
    public CameraBehaviour cb;
    bool zoomedIn = false;
    public GameObject ComputerScreen;
    public GameObject OtherCanvas, mountCounter; //Måske gør det her en liste, så den kan have flere other canvasses?
    private void OnMouseOver()
    {
        if (!zoomedIn && !DialogueManager.instance.dialogueVissible && GameManager.instance.assistant.tutorialHasPlayed && !mountCounter.active)
        {
            light.SetActive(true);
            if(Input.GetMouseButtonDown(0)) { //WHEN IT CLICK
                OtherCanvas.SetActive(false);
                ComputerScreen.SetActive(true);
                light.SetActive(false);
                cb.zoomIn();
                zoomedIn = true;
            }
        }
    }

    private void OnMouseExit()
    {
        light.SetActive(false);
    }

    public void ZoomOut()
    {
        ComputerScreen.SetActive(false);
        OtherCanvas.SetActive(true);
        zoomedIn = false;
        cb.zoomOut();
    }
}
