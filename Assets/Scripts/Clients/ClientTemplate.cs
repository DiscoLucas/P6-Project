using UnityEngine;
[CreateAssetMenu(fileName = "Client Template", menuName = "Clients/SpawnClientTemplate", order = 0)]

public class ClientTemplate : ScriptableObject
{
    [Header("Client Info")]
    public string clientName;
    public int age;
    public string job;
    public string city;
    public string maritalStatus;
    public string caseDescription;

    [Header("Client financial info")]
    public float monthlyIncome;
    public float monthlyExpenses;
    public float monthlySavings;
    public float totalSavings;
    public float neededLoan;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
