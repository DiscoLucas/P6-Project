using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ClientManager : MonoBehaviour
{
    public bool canGenerateMoreClients = true;
    [SerializeField]
    List<ClientTemplate> templates;
    [SerializeField]
    List<ClientData> clients;
    public ClientData currentClient;
    [SerializeField]
    int precentationIndex = 0;
    DialogueInteractor di;
    [SerializeField]
    ClientPresState clientPresState = ClientPresState.none;

    [Header("Animation/Visuals")]
    [SerializeField]
    public Animator an;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    public GameObject ClientObject; // Client er det client gameobject vi har lige nu.
    [SerializeField]
    string walkInAnimation, walkOutAnimation;

    [SerializeField]
    bool startedPrestentation = false;
    public bool hastalked = false;


    void Start()
    {
        if(an == null)
            an = ClientObject.GetComponent<Animator>();
        if(di == null)
            di = ClientObject.GetComponent <DialogueInteractor>();
        if(spriteRenderer == null)
            spriteRenderer= ClientObject.GetComponent<SpriteRenderer>();
        DialogueManager.instance.dialogDone.AddListener(clientDoneTalking);
        GameManager.instance.clientMeetingDone.AddListener(stopMeeting);

    }
    /// <summary>
    /// The the amount of clients templates
    /// </summary>
    /// <param name="template"></param>
    public int getClientsTempCount() {
        return templates.Count;
    }

    /// <summary>
    /// The the amount of clients generatede
    /// </summary>
    /// <param name="template"></param>
    public int getClientsCount()
    {
        return clients.Count;
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
            int i = Random.Range(0, templates.Count);
            ClientTemplate template = templates[i];
            ClientData c = new ClientData(template);
            clients.Add(c);
            Debug.Log("UPDATEDE:Index: " + index + " client count: " + clients.Count);
            templates.RemoveAt(i);
            return c;
        }

        if (index < 0) {
            Debug.LogError("The given index is less than zero");
            return clients[1];
        }
        if (index >= clients.Count)
        {
            Debug.LogError("The given index exceeds the length of the list");
            return clients[1];
        }


        return clients[index];
    }

    public void cantCreateMore() {
        canGenerateMoreClients = false;
    }

    /// <summary>
    /// Create a new client
    /// </summary>
    /// <returns></returns>
    public ClientData getNewClient() {
        return getClient(clients.Count);
    }

    /// <summary>
    /// Introuduces a client
    /// </summary>
    /// <param name="c"></param>
    public void startClientIntro(ClientData c) {
        spriteRenderer.sprite = c.sprite;
        currentClient = c;
        if (an != null)
        {
            an.Play(walkInAnimation);
            //when the dialog is done please do this:
            //an.SetBool("WalkOut", true);
        }
        else {
            Debug.LogError("Could not find Animation");
        }

    }

    /// <summary>
    /// Gets a random client from the createde clients
    /// </summary>
    public ClientData getrRandomClient() {
        return clients[Random.Range(0, clients.Count)];
    }


    public void havePresentedeClient() {
        //Event_manager.instance.turns[Event_manager.instance.turnIndex].currentActionDisplay.gameObject.SetActive(true);
    }

    public void changeClientPresState(ClientPresState cps) {
        clientPresState = cps;
    }
    public void clientStartTalking()
    {
        if (clientPresState == ClientPresState.talking) {
            DialogueManager.instance.StartDia(precentationIndex);
        }
    }
    public void clientDoneTalking() {
        Debug.Log("DoneTalking");
        if (clientPresState == ClientPresState.talking)
        {
            Debug.Log("Starting shit");
            clientPresState = ClientPresState.filling;
            GameManager.instance.createClientMeeting();
        }
    }

    public void stopMeeting() {
        if (clientPresState == ClientPresState.filling) {
            clientPresState = ClientPresState.none;
            an.Play(walkOutAnimation);
        }
    }

}
[Serializable]
public enum ClientPresState {
    walkin,
    talking,
    filling,
    walkingOut,
    none
}
