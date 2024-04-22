using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class popper : MonoBehaviour
{
    public int eventID;
    public TextMeshProUGUI eventDescription;
    public TextMeshProUGUI eventEffect;
    public GameObject Popup;

    public string[] eventsDescriptionList;
    public string[] eventsEffectList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up")){
            eventID = Random.Range(0, eventsDescriptionList.Length);

            eventDescription.text = eventsDescriptionList[eventID];
            eventEffect.text = eventsEffectList[eventID];
        }
    }
}
