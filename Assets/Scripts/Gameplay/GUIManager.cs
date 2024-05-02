using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [Header("Menu Stuff")]
    public GameObject action_Menu;
    public GameObject talkClient_BTN;

    public void showActionMenu() { 
        action_Menu.SetActive(true);
        talkClient_BTN.SetActive(true);
    }

    public void hideActionMenu() {
        action_Menu.SetActive(false);
    }
}
