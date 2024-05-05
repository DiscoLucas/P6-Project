using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;

public class GUIManager : MonoBehaviour
{
    [Header("Menu Stuff")]
    public GameObject action_Menu;
    public GameObject talkClient_BTN;

    RectTransform dialogBox;
    Canvas canvas;

    private void Awake()
    {
        if (dialogBox == null) dialogBox = GameObject.FindWithTag("Dialog Box").GetComponent<RectTransform>();
            
        else throw new System.Exception("There is either more than one Dialog Box in the scene, or none");

        if (canvas == null)
            canvas = GameObject.FindWithTag("Main Canvas").GetComponent<Canvas>();
        else throw new System.Exception("There is either more than one Main Canvas in the scene, or none");
    }

    public void showActionMenu() { 
        action_Menu.SetActive(true);
        talkClient_BTN.SetActive(true);
    }

    public void hideActionMenu() {
        action_Menu.SetActive(false);
    }

    /// <summary>
    /// Calculate the target position based on the canvas and the dialog box size
    /// </summary>
    public void ShowDialogBox()
    {
        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;
        Vector2 dialogBoxSize = dialogBox.GetComponent<RectTransform>().sizeDelta;
        Vector2 targetPosition = new Vector2(canvasSize.x / 2 - dialogBoxSize.x / 2, -canvasSize.y / 2 + dialogBoxSize.y / 2);

        dialogBox.anchoredPosition = targetPosition;
    }
}