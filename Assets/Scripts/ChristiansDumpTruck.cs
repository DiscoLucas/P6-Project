using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ChristiansDumpTruck : MonoBehaviour
{
    public GameObject light;
    public CameraBehaviour cb;
    bool zoomedIn = false;
    public GameObject ComputerScreen;
    public GameObject OtherCanvas; //M�ske g�r det her en liste, s� den kan have flere other canvasses?
    private void OnMouseOver()
    {
        if (!zoomedIn)
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
