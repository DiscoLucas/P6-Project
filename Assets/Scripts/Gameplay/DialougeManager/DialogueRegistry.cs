using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueRegistry : MonoBehaviour
{
    public static DialogueRegistry instance;

    [TextArea(3, 10)]
    public string[] sentinces;
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
    public string GetSentincesIndex(int index)
    {
        if (index >= 0 && index < sentinces.Length) //Vi skal ændre index til at blive sent fra ClientData? eller et andet sted fra?
        {
            return sentinces[index];
        }
        else
        {
            Debug.LogWarning("Index out of range!");
            return null;
        }
    }
    public string replaceString(string message, ClientData client) {
        string[] values = {
            client.clientName,
            client.age.ToString() ,
            client.job.ToString() ,
            client.city.ToString() ,
            client.maritalStatus.ToString() ,
            client.Finance.monthlyIncome.ToString() ,
            client.Finance.monthlyExpenses.ToString() ,
            client.Finance.monthlySavings.ToString() ,
            client.Finance.totalSavings.ToString(),
            client.Finance.debt.ToString() ,
            client.Finance.debtFactor.ToString()
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
        int randomNr = (Random.Range(0, sentinces.Length));
        //Debug.Log(randomNr);
        return randomNr;
    }
}
