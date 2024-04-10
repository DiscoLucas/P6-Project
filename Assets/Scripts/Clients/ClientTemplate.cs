using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class ClientTemplate : MonoBehaviour
{
    public ClientData clientData;
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
        Debug.Log("Client Name: " + clientData.clientName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
