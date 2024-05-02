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
        //g�re denne til instance ellers �dl�g dette gameobject
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
            Debug.Log("F�rdig");
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
