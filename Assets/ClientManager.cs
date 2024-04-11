using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KundeTemplate : MonoBehaviour
{
    [SerializeField]
    ClientData[] clients;
    public GameObject Client; // Client er det client gameobject vi har lige nu.
    Animation an;

    bool hastalked = false;

    void Start()
    {
        an = Client.GetComponent<Animation>();
    }

    void Update()
    {
        if (!an.isPlaying && !hastalked && Client.activeInHierarchy)
        {
            //sig noget lort @jacob please lav det her
            Debug.Log("hej jeg er albert");
            hastalked= true;
        }

        if (Input.GetKeyDown("up")) //Det her if statment skal bare byttes ud med hvad end conditionen for at en client kommer IND.
        {
            hastalked = false;
            Client.SetActive(true);
        }

        if (Input.GetKeyDown("down")) //Det her if statment skal bare byttes ud med hvad end conditionen for at en client kommer UD.
        {
            Client.SetActive(false);
        }
    }
}
