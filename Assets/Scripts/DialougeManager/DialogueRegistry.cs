using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueRegistry : MonoBehaviour
{
    public static DialogueRegistry instance;
    [SerializeField]
    string[] tags = {
        "[Tjener]",
        "[Navn]",
        "[Lån på]",
    };

    [TextArea(3, 10)]
    public string[] sentinces;

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

    public int GetIndex()
    {
        int randomNr = (Random.Range(0, sentinces.Length));
        //Debug.Log(randomNr);
        return randomNr;
    }
}
