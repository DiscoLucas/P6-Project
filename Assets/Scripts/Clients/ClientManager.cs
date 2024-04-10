using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KundeTemplate : MonoBehaviour
{
    public GameObject client; // Client er det client gameobject vi har lige nu.
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("up")) //Det her if statment skal bare byttes ud med hvad end conditionen for at en client kommer ind.
        {
            Instantiate(client);
        }
    }
}
