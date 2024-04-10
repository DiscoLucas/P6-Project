using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_manager : MonoBehaviour
{
    [SerializeField]
    List<DailyAction> daylieyActions;
    int dayIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        daylieyActions[0].updateEvent();
    }
}
