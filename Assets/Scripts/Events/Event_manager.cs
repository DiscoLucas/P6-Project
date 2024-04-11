using System.Collections.Generic;
using UnityEngine;

public class Event_manager : MonoBehaviour
{
    public List<Turn> days;
    public int dayIndex = 0;
    public static Event_manager instance;
    public Transform EI_transform;
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
        days[dayIndex].startDay();
    }

    public void changeDay()
    {
        dayIndex++;
        days[dayIndex].startDay();
    }
    public void updateDay()
    {
        if (dayIndex >= days.Count)
            Debug.Log("Færdig");
        else
            days[dayIndex].updateActions();

    }

    public actionsDisplay instiate_interface(GameObject obj)
    {
        GameObject go = Instantiate(obj, EI_transform.position, EI_transform.rotation);
        go.transform.parent = EI_transform;
        return go.GetComponent<actionsDisplay>();
    }
}
