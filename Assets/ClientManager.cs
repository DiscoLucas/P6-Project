using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField]
    ClientTemplate[] templates;
    [SerializeField]
    List<ClientData> clients;
    
    public GameObject ClientObject; // Client er det client gameobject vi har lige nu.
    [SerializeField]
    public Animation an;
    DialogueInteractor di;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    public bool hastalked = false;

    void Start()
    {
        if(an == null)
            an = ClientObject.GetComponent<Animation>();
        if(di == null)
            di = ClientObject.GetComponent <DialogueInteractor>();
        spriteRenderer= gameObject.GetComponent<SpriteRenderer>();
    }
    public void generateClientFromtTemplate(ClientTemplate template) {
        clients.Add(new ClientData(template));
    }
    public ClientData getClient(string name) {
        foreach (ClientData c in clients) {
            if (name.ToLower() == c.clientName.ToLower()) {
                return c;
            }
        }
        Debug.LogError("Could not find client");
        return null;
    }

    public ClientData getClient(int index)
    {
        Debug.Log("Index: " + index + " client count: " + clients.Count);
        if (clients.Count == 0 || clients.Count >= index) {
            ClientTemplate template = templates[Random.Range(0, templates.Length)];
            ClientData c = new ClientData(template);
            clients.Add(c);
            Debug.Log("UPDATEDE:Index: " + index + " client count: " + clients.Count);
            return c;
        }

        if (index < 0) {
            Debug.LogError("The given index is less than zero");
            return clients[1];
        }
        if (index >= clients.Count)
        {
            Debug.LogError("The given index more than the length of the list");
            return clients[1];
        }


        return clients[index];
    }

    public ClientData getNewClient() {
        return getClient(clients.Count);
    }

    public void startClientIntro(ClientData c) {
        spriteRenderer.sprite = c.sprite;
        if (an != null)
        {
            an.Play("WalkIn");
        }
        else {
            Debug.LogError("Could no find Animation");
        }

    }

    public void havePresentedeClient() {
        Event_manager.instance.turns[Event_manager.instance.turnIndex].currentActionDisplay.gameObject.SetActive(true);
    }
    void Update()
    {
        //TODO: FUCKING FIX::..... vi skifter til animatoren i stedet og tjekker navnet (gidder ikke at løse)
       /* if (ClientObject.transform.position.x == -12 && !an.isPlaying) //GRIM LØSNING, IKKE RØR ANIMATIONEN HVOR DEN GÅR UD FØR DET HER ER FIKSET!
        {
            ClientObject.SetActive(false);
            Event_manager.instance.turns[Event_manager.instance.turnIndex].currentActionDisplay.gameObject.SetActive(true);
        }*/

        /*if (!an.isPlaying && !hastalked && ClientObject.activeInHierarchy) //When client has walked in
        {
            hastalked= true;
            di.TiggerDialogue();
        }

        

        if (Input.GetKeyDown("up")) //Det her if statment skal bare byttes ud med hvad end conditionen for at en client kommer IND.
        {
            hastalked = false;
            ClientObject.SetActive(true);
            an.Play("WalkIn");
        }

        if (Input.GetKeyDown("down")) //Det her if statment skal bare byttes ud med hvad end conditionen for at en client kommer UD.
        {
            an.Play("WalkOut");
        }*/
    }
}
