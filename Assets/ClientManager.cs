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
        if (!an.isPlaying && !hastalked && Client.activeInHierarchy) //When client has walked in
        {
            hastalked= true;
            di.TiggerDialogue();
        }

        if(Client.transform.position.x == -12 && !an.isPlaying) //GRIM L�SNING, IKKE R�R ANIMATIONEN HVOR DEN G�R UD F�R DET HER ER FIKSET!
        {
            Client.SetActive(false);
        }

        if (Input.GetKeyDown("up")) //Det her if statment skal bare byttes ud med hvad end conditionen for at en client kommer IND.
        {
            hastalked = false;
            Client.SetActive(true);
            an.Play("WalkIn");
        }

        if (Input.GetKeyDown("down")) //Det her if statment skal bare byttes ud med hvad end conditionen for at en client kommer UD.
        {
            an.Play("WalkOut");
        }
    }
}
