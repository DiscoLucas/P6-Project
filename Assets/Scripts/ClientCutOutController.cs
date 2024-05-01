using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClientCutOutController : MonoBehaviour
{
    public ClientManager cm;
    private void Start()
    {
        DialogueManager.instance.sentinceDone.AddListener(dialogueDone);
    }
    public void updateState(ClientPresState state) {
        cm.changeClientPresState(state);
        Debug.LogAssertion("ALLO MAND OBJECT ER" + (GameManager.instance.csm.currentClientMeeting != null ));
        if (state == ClientPresState.talking) {
            DialogueManager.instance.nextSentince = 
                GameManager.instance.csm.clientMeetingsTemplates[
                    GameManager.instance.csm.clientMeetIndex
                    ]
                .returnSentince();

            Debug.Log("startTalking");
            cm.clientStartTalking();
        }
    }

    public void stopClient() {
        GameManager.instance.updateTurn();
    }

    public void dialogueDone()
    {
        
        if (GameManager.instance.csm.clientMeetingsTemplates[GameManager.instance.csm.clientMeetIndex].updateSenIndex())
        {
            DialogueManager.instance.nextSentince = GameManager.instance.csm.clientMeetingsTemplates[GameManager.instance.csm.clientMeetIndex].returnSentince();
        }
        else
        {
            DialogueManager.instance.hasRun = true;
        }
        
    }
}
