using UnityEngine;
[CreateAssetMenu(fileName = "Client Template", menuName = "Clients/SpawnClientTemplate", order = 0)]

/// <summary>
/// This class is used to create an object that can be filled out with the information of a client
/// </summary>
public class ClientTemplate : ScriptableObject
{
    [Header("Client Info")]
    public string clientName;
    public int age;
    public string job;
    public string city;
    public MaritalStatus maritalStatus;
    public string caseDescription;
    public Sprite chacterSprite;

    [Header("Client financial info")]
    public float monthlyIncome;
    public float monthlyExpenses;
    public float monthlySavings;
    public float totalSavings;
    public float neededLoan;
}
