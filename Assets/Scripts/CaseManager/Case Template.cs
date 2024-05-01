using UnityEngine;
[CreateAssetMenu(fileName = "Case Template", menuName = "Cases/caseTemplate", order = 0)]

public class CaseTemplate : ScriptableObject
{
    public string caseName = "_case";
    public MeetingCollection[] meetings;
    public CustomerType customerType;
    public string caseDiscription;
    [Tooltip("The type of customer who can partake in these meetings")]public CustomerType type;


}
