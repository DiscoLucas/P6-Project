using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Case
{
    public string caseName = "_case";
    public MeetingCollection[] meetings;
    public int convertionMeetingIndex = 1;
    [Tooltip("The type of customer who can partake in these meetings")] public CustomerType type;
    public ClientData client;
    public float loanAmount;
    public float debt;
    public Loan loan;
    [Tooltip("Which meeting they need to be in")]public int meetingIndex = 0;
    public int sentincesIndex = 0;
    public bool needLoan;
    public bool closeCase =false;
    public string caseDiscription;
    public int nextImportenTurn = -1;
    public Case(CaseTemplate template,ClientData _client) {
        caseName = template.caseName;
        meetings= template.meetings;
        client = _client;
        type= template.type;
        caseDiscription = template.caseDiscription;
        needLoan= template.needLoan;
    }
    public string getMeetingId() {
        return caseName;
    }

    public void contiuneToNextTypeOfMeeting() {
        meetingIndex++;
        sentincesIndex = 0;
    }

    public MeetingCollection getCurrentMeeting() {
        if(meetingIndex >= meetings.Length)
            return meetings[meetings.Length-1];
        else
            return meetings[meetingIndex];
    }
    public bool checkIfDoneTalking() {
        return false;
    }

    public bool checkIfCaseIsDone() {
        return closeCase;// loan.RemainingLoanAmount <= 0;
    }

    public bool checkCaseUpdate() { 
        if(loan != null)
        {
            bool update = (GameManager.instance.monthNumber - loan.initialMonth >= 360);
            
            if (update) {
            }
            return update; 

        }
        return false;
    }

    public bool updateSentince()
    {
        sentincesIndex++;
        return false;
    }

    public int returnSentince()
    {
        Debug.Log("Meeting index: " + meetingIndex);
        int index = meetingIndex;
        if (meetingIndex >= meetings.Length)
            index = meetings.Length - 1;    
        int i = meetings[index].meetingDialog;
        return i;
    }

    public bool assistantsSentinceUpdate()
    {
        sentincesIndex++;
        return (sentincesIndex < meetings[meetingIndex].assistantSaulSentensis.Length);
    }

    public int assistantSentinceReturn()
    {
        int i = meetings[meetingIndex].assistantSaulSentensis[sentincesIndex];
        return i;
    }

}
[Serializable]
public struct MeetingCollection {
    public string name;
    public bool haveEncountered;
    public GameObject meetingPrefab;
    public int meetingDialog;
    public bool needToFinnishToProgress;
    public int[] assistantSaulSentensis;
    public int turtoialIndex;
}

