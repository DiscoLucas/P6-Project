using System.Collections.Generic;
using UnityEngine;

public class Event_manager : MonoBehaviour
{
    public List<Turn> turns;
    public int turnIndex = 0;
    public static Event_manager instance;
    public Transform EI_transform;
    public ClientManager clientManager;
    public void Awake()
    {
        //tjekker om der er en instance og hvis der ikke er
        //gøre denne til instance ellers ødlæg dette gameobject
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        turns[turnIndex].startDay();
    }

    public void changeDay()
    {
        turnIndex++;
        turns[turnIndex].startDay();
    }
    public void updateDay()
    {
        if (turnIndex >= turns.Count)
            Debug.Log("Færdig");
        else
            turns[turnIndex].updateActions();

    }

    public actionsDisplay instiate_interface(GameObject obj)
    {
        GameObject go = Instantiate(obj, EI_transform.position, EI_transform.rotation);
        go.transform.parent = EI_transform;
        return go.GetComponent<actionsDisplay>();
    }

    
}
