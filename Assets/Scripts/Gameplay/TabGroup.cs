using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// sauce: <seealso cref="https://youtu.be/211t6r12XPQ?si=4hbGyZtpcthch0aV"/>
/// </summary>
public class TabGroup : MonoBehaviour
{
    public List<TabBtn> tabButtons;
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;

    public TabBtn selectedTab;

    public List<GameObject> objectsToSwap;
    
    public void Subscribe(TabBtn button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabBtn>();
        }
        
        tabButtons.Add(button);
    }

    public void OnTabEnter(TabBtn button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
        }
            
    }

    public void OnTabExit(TabBtn button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabBtn button)
    {
        selectedTab = button;
        
        ResetTabs();
        button.background.color = tabActive; 
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabBtn button in tabButtons)
        {
            if (selectedTab!=null && button == selectedTab) { continue; }
            {
                button.background.color = tabIdle;
            }
        }
    }
}
