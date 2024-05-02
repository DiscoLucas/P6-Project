using System;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "Action", menuName = "Actions/Default_Action", order = 1)]
public class ActionTemplate : ScriptableObject
{
    public string aName;
    [TextArea(3, 10)]
    public string description;
    public bool isDone = false;
    public ActionTemplate relatedEvent;
    public GameObject eInterfacePrefab;
    public ClientTemplate client;

    public string replaceString(string message, string[] tags, string[] values) {
        string output = message;
        if (tags.Length != values.Length) {
            Debug.LogError("Tags length and values does not macht");
        }
        
        for (int i = 0; i < tags.Length; i++)
        {
            Debug.Log(tags[i]+ " = " +values[i]);
            output = output.Replace(tags[i], values[i]);
            
        }

        return output;
    }

    public virtual void start() {
        
    }

}
