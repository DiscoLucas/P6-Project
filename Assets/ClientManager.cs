using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField]
    ClientTemplate[] templates;
    [SerializeField]
    List<ClientData> clients;
    
    DialogueInteractor di;
    [SerializeField]

    public bool hastalked = false; 

    [Header("Animation/Visuals")]
    [SerializeField]
    public Animator an;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    public GameObject ClientObject; // Client er det client gameobject vi har lige nu.
    [SerializeField]
    string walkInAnimation, walkOutAnimation;
    bool startedPrestentation = false;

    void Start()
    {
        if(an == null)
            an = ClientObject.GetComponent<Animator>();
        if(di == null)
            di = ClientObject.GetComponent <DialogueInteractor>();
        if(spriteRenderer == null)
            spriteRenderer= ClientObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Create client data from templeate
    /// </summary>
    /// <param name="template"></param>
    public void generateClientFromtTemplate(ClientTemplate template) {
        clients.Add(new ClientData(template));
    }

    /// <summary>
    /// Find the client by name (Not suggested to use)
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ClientData getClient(string name) {
        foreach (ClientData c in clients) {
            if (name.ToLower() == c.clientName.ToLower()) {
                return c;
            }
        }
        Debug.LogError("Could not find client");
        return null;
    }


    /// <summary>
    /// get the client with the index and if the index is more than the current length of client a new client is createde
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Create a new client
    /// </summary>
    /// <returns></returns>
    public ClientData getNewClient() {
        return getClient(clients.Count);
    }

    /// <summary>
    /// use to introuduce a client
    /// </summary>
    /// <param name="c"></param>
    public void startClientIntro(ClientData c) {
        spriteRenderer.sprite = c.sprite;
        if (an != null)
        {
            an.Play(walkInAnimation);
            Debug.Log("Prestentation of client\nPlease Add the dialog starter her\nexample of how is here");
            startedPrestentation= true;
            //when the dialog is done please do this:
            an.SetBool("WalkOut", true);
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
        //Check if the cut out have waleked out and if it is done it rest the stuff
        /*if (startedPrestentation&& !(an.GetCurrentAnimatorClipInfo(0)[0].clip.name == walkOutAnimation|| an.GetCurrentAnimatorClipInfo(0)[0].clip.name == walkInAnimation)) {
            an.SetBool("WalkOut", false);
            startedPrestentation = false;
        }*/

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
