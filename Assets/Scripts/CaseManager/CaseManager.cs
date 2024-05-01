using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CaseManager : MonoBehaviour
{
    public Case _case;

    [Header("Client Meeting")]
    [SerializeField]
    CaseTemplate[] caseTemplate;
    public List<Case> currentCases;
    public int currentCaseIndex = -1;

    public int closedCases = 0;
    public int needClosedCases = 3;
    [SerializeField]
    public ClientMeeting currentClientMeeting;

    [SerializeField] public Transform clientMeetingTransform;

    public void endMeeting() {
        currentCases[currentCaseIndex].goToNextClientMeeting();
        if (currentCases[currentCaseIndex].checkIfCaseIsDone()) {
            currentCases.RemoveAt(currentCaseIndex);
            closedCases++;
            if (closedCases >= needClosedCases) {
                Debug.Log("SPILLET ER FÆRDIG TABER");
                Debug.Break();
            }
        }
        currentCaseIndex= -1;
        Destroy(currentClientMeeting.gameObject);
    }

    public void createCase(ClientData clientData) {
        var templates = caseTemplate.Where(c => c.type == clientData.customerType).ToArray();
        int count = templates.Length;
        if (count == 0)
            Debug.LogError("No template of that type");
        CaseTemplate ct = templates[Random.Range(0, count)];
        Case c = new Case(ct, clientData);
        currentCases.Add(c);
        if (needClosedCases >= (closedCases + GameManager.instance.clm.getClientsCount()))
            GameManager.instance.clm.cantCreateMore();
        currentCaseIndex = currentCases.Count - 1;
        
    }

    public Case getCurrentCase() {
        return currentCases[currentCaseIndex];
    }

    /// <summary>
    /// This function is called from the action menu and start the clietn talked 
    /// It is controlled by the turnT if it should be a client introduction or client metting
    /// </summary>
    public void startConviencation()
    {

        GameManager.instance.guim.showActionMenu();
        TurnType turnT = GameManager.instance.getCurrentTurnType();
        if (turnT == TurnType.Change_forCustomer)
        {
            GameManager.instance.clientMeeting();
        }
        else if (turnT == TurnType.New_customer)
        {
            GameManager.instance.newCustomer();
        }
    }


    public Case getCasesThatCanUpdate()
    {
        Case c = null;
        for (int i = 0; i < currentCases.Count; i++) {
            Case cas = currentCases[i];
            if (!cas.needLoan)
            {
                currentCaseIndex = i;
                c = cas;
                break;
            }
        }
        return c;   
    }
    /// <summary>
    /// Give the case that need to be opdatede
    /// 
    /// </summary>
    /// <returns></returns>
    public Case getCasesThatNeedUpdate(int mountCounter) {
        Debug.LogWarning("THIS METHOD IS NOT IMPLEMENTEDE YET!");
        Case c = null;
        for (int i = 0; i < currentCases.Count; i++)
        {
            Case cas = currentCases[i];
            if (cas.nextImportenTurn >= mountCounter)
            {
                c = cas;
            }
        }
        return c;
    }
}
