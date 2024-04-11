using UnityEngine;

public class KundeTemplate : MonoBehaviour
{
    [SerializeField]
    ClientData[] clients;
    public GameObject Client; // Client er det client gameobject vi har lige nu.
    Animation an;
    DialogueInteractor di;

    bool hastalked = false;

    void Start()
    {
        an = Client.GetComponent<Animation>();
        di = Client.GetComponent <DialogueInteractor>();
    }

    void Update()
    {
        if (!an.isPlaying && !hastalked && Client.activeInHierarchy)
        {
            //sig noget lort @jacob please lav det her
            hastalked= true;
            di.TiggerDialogue();
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
