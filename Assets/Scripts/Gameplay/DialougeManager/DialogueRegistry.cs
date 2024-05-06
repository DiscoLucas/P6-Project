using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;

public class DialogueRegistry : MonoBehaviour
{
    public static DialogueRegistry instance;
    //public string[][] sentinces;
    //[TextArea(3, 10)]

    [Tooltip("The x axis are the cases, and they y axis are the dialogue options")] public Array2DString sentinces;
    //[SerializeField]
    public string[] tags = {
        "[Tjener]",
        "[Navn]",
    };
    public string[] values =
    {
        "[value1]",
    };

    private void Awake()
    {
        //Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }



    /// <summary>
    /// This funktion takes an int that selects the coresponding element from the Sentince[]
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetSentincesIndex(int caseIndex, int dialogueIndex)
    {
        
        if (caseIndex >= 0 && caseIndex < sentinces.GridSize.y && dialogueIndex >= 0 && dialogueIndex < sentinces.GridSize.x)
        {
            return sentinces.GetCell(dialogueIndex, caseIndex);
        }
        else
        {
            Debug.LogWarning("Index out of range!");
            return null;
        }
    }
    public string replaceString(string message, Case currentCase) {
        if (currentCase == null) { 
            return message;
        }
        string[] values = {
            currentCase.client.clientName,
            currentCase.client.age.ToString() ,
            currentCase.client.job.ToString() ,
            currentCase.client.city.ToString() ,
            currentCase.client.maritalStatus.ToString() ,
            currentCase.client.Finance.monthlyIncome.ToString("N") ,
            currentCase.client.Finance.monthlyExpenses.ToString("N") ,
            currentCase.client.Finance.monthlySavings.ToString("N") ,
            currentCase.client.Finance.totalSavings.ToString("N"),
            currentCase.client.Finance.debt.ToString("N") ,
            currentCase.client.Finance.debtFactor.ToString(),
            currentCase.loanAmount.ToString("N"),
            
            (currentCase.loan == null)?"NAN":currentCase.loan.loanAmount.ToString("N")
        };

        return replaceString(message, tags, values);
        
    }
    public string replaceString(string message, string[] t, string[] v)
    {
        string output = message;
        if (t.Length != v.Length)
        {
            Debug.LogError("Tags length and " + v + " does not macht");
            return message;
        }

        for (int i = 0; i < tags.Length; i++)
        {
            output = output.Replace(t[i], v[i]);

        }

        return output;
    }

    public int GetIndex()
    {
        int randomNr = (Random.Range(0, sentinces.GridSize.x));
        //Debug.Log(randomNr);
        return randomNr;
    }
}
