using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum MaritalStatus {
    Single,
    Married,
    Partner
}

[System.Serializable]
public class ClientData
{
    public string clientName;
    public int age;
    public string job;
    public string city;
    public MaritalStatus maritalStatus;
    public FinanceData Finance;
    public Sprite sprite;
    public bool haveBeenPresented = false;
    public int startMeet;
    [Header("Audio properties")]
    public float minPitch;
    public float maxPitch;
    [Header("Case Discription")]
    public string caseDiscription;
    public ClientData(ClientTemplate template) {
        clientName = template.clientName;
        age = template.age;
        job = template.job;
        city = template.city;
        maritalStatus = template.maritalStatus;
        sprite = template.chacterSprite;
        Finance = new FinanceData(template);
        caseDiscription = template.caseDescription;
        minPitch = template.minPitch;
        maxPitch = template.maxPitch;
        startMeet = template.startMeet;
    }
}
[System.Serializable]
public class FinanceData
{
    public float monthlyIncome;
    public float monthlyExpenses;
    public float monthlySavings;
    public float totalSavings;
    public float yearlyIncome;
    public float debt;
    public float debtFactor = 4;
    public float neededLoan;

    public FinanceData(ClientTemplate template)
    {
        monthlyExpenses= template.monthlyExpenses;
        monthlySavings= template.monthlySavings;
        totalSavings= template.totalSavings;
        neededLoan= template.neededLoan;
        monthlyIncome= template.monthlyIncome;
        debt= template.debt;
    }
}
