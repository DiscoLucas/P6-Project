using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClientCutOutController : MonoBehaviour
{
    public ClientManager cm;
    public sentinceList[] clientTalks;
    private void Start()
    {
        DialogueManager.instance.sentinceDone.AddListener(dialogueDone);
    }
    public void updateState(ClientPresState state) {
        cm.changeClientPresState(state);
        if (state == ClientPresState.talking) {
            DialogueManager.instance.nextSentince = clientTalks[0].sentinces[clientTalks[0].index];
            Debug.Log("startTalking");
            cm.clientStartTalking();
        }
    }

    public void stopClient() {
        GameManager.instance.updateTurn();
    }

    public void dialogueDone()
    {
        clientTalks[0].index++;
        if (clientTalks[0].index < clientTalks[0].sentinces.Length)
        {
            DialogueManager.instance.nextSentince = clientTalks[0].sentinces[clientTalks[0].index];
        }
        else
        {
            DialogueManager.instance.hasRun = true;
        }
        
    }
}
[Serializable]
public class sentinceList
{
    public int index=0;
    public int[] sentinces;
}
