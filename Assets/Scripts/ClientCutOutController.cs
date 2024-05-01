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
                GameManager.instance.csm.currentCases[
                    GameManager.instance.csm.currentCaseIndex
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
        
        if (GameManager.instance.csm.currentCases[GameManager.instance.csm.currentCaseIndex].checkIfDoneTalking())
        {
            DialogueManager.instance.nextSentince = GameManager.instance.csm.currentCases[GameManager.instance.csm.currentCaseIndex].returnSentince();
        }
        else
        {
            DialogueManager.instance.hasRun = true;
        }
        
    }
}
