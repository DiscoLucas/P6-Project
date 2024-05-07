using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClientCutOutController : MonoBehaviour
{
    public ClientManager cm;
    public Assistant assistant;
    private void Start()
    {
        DialogueManager.instance.sentinceDone.AddListener(dialogueDone);
    }
    public void updateState(ClientPresState state) {
        Debug.Log("Client walk in");
        cm.changeClientPresState(state);
        Debug.LogAssertion("ALLO MAND OBJECT ER" + (GameManager.instance.csm.currentClientMeeting != null ));
        if (state == ClientPresState.talking) {
            Debug.Log("Set next sentinces");
            int nextSen = GameManager.instance.csm.getCurrentCase()
                .returnSentince();
            DialogueManager.instance.nextSentince = nextSen;
            Debug.Log("next sen: " + nextSen);
            if (GameManager.instance.csm.getCurrentCase().updateSentince())
            {
                dialogueDone();
            }
            Debug.Log("startTalking");
            cm.clientStartTalking();
        }

    }

    public void stopClient() {
        GameManager.instance.endTurnButton.SetActive(true);
    }

    public void dialogueDone()
    {
        if (assistant.tutorialRunning)
        {
            if (assistant.checkIfTutorialDone())
            {
                DialogueManager.instance.hasRun = true;
            }
            else
            {

            }
        }
        else
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
}
