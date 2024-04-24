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
    private void OnMouseOver()
    {
        if (!zoomedIn)
        {
            light.SetActive(true);
            if(Input.GetMouseButtonDown(0)) { 
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
        zoomedIn = false;
        cb.zoomOut();
    }
}
