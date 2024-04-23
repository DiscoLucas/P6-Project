using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientCutOutController : MonoBehaviour
{
    public List<int[]> clientDialogueInt;
    public ClientManager cm;

    public void updateState(ClientPresState state) {
        cm.changeClientPresState(state);
        if (state == ClientPresState.talking) {
            Debug.Log("startTalking");
            cm.clientStartTalking();
        }
    }

    public void stopClient() {
        GameManager.instance.updateTurn();
    }
}
