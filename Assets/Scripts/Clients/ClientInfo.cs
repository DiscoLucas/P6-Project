using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientInfo : MonoBehaviour
{
    [Tooltip("Reference to the client object created from the ClientTemplate")]
    public ClientTemplate clientObject;
    [Header("Client Info")]
    public string clientName;
    public int age;
    public string job;
    public string city;
    public MaterialStatus maritalStatus;
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
        // Copy the values from the clientTemplate object to the ClientInfo
        clientName = clientObject.clientName;
        age = clientObject.age;
        job = clientObject.job;
        city = clientObject.city;
        maritalStatus = clientObject.maritalStatus;
        caseDescription = clientObject.caseDescription;
        monthlyIncome = clientObject.monthlyIncome;
        monthlyExpenses = clientObject.monthlyExpenses;
        monthlySavings = clientObject.monthlySavings;
        totalSavings = clientObject.totalSavings;
        neededLoan = clientObject.neededLoan;
        Debug.Log("clientName: " + clientName);
    }
}
