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

    public string replaceString(string message, string[] tags, string[] values)
    {
        string output = message;
        if (tags.Length != values.Length)
        {
            Debug.LogError("Tags length and " + values + " does not macht");
            return message;
        }

        for (int i = 0; i < tags.Length; i++)
        {
            Debug.Log(tags[i] += values[i]);
            output = output.Replace(tags[i], values[i]);

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
