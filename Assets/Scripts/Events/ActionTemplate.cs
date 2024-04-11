using System;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "Action", menuName = "Actions/Default_Action", order = 1)]
public class ActionTemplate : ScriptableObject
{
    public string aName;
    public string description;
    public bool isDone = false;
    public ActionTemplate relatedEvent;
    public GameObject eInterfacePrefab;




}
