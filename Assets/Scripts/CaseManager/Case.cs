using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Case
{
    public string caseName = "_case";
    public MeetingCollection[] meetings;
    [Tooltip("The type of customer who can partake in these meetings")] public CustomerType type;
    public ClientData client;
    public float loanAmount;
    public float ovenAmount;
    [Tooltip("Which meeting they need to be in")]public int meetingIndex = 0;
    public int sentincesIndex = 0;
    public bool caseClosed = false;
    public string caseDiscription;

    public Case(CaseTemplate template,ClientData _client) {
        caseName = template.caseName;
        meetings= template.meetings;
        client = _client;
        type= template.customerType;
        caseDiscription = template.caseDiscription;
    }
    public string getMeetingId() {
        return caseName;
    }


    public MeetingCollection getCurrentMeeting() {
        return meetings[meetingIndex];
    }
    public bool checkIfDoneTalking() {
        return (sentincesIndex < meetings[meetingIndex].meetingSentences.Length);
    }

    public bool checkIfCaseIsDone() {
        caseClosed = (meetingIndex >= meetings.Length);
        return caseClosed;
    }

    public void goToNextClientMeeting() { 
        meetingIndex++;
    }

    public int returnSentince()
    {
        int i = meetings[meetingIndex].meetingSentences[sentincesIndex];
        sentincesIndex++;
        return i;
    }

}
[Serializable]
public struct MeetingCollection {
    public string name;
    public bool haveEncountered;
    public GameObject meetingPrefab;
    public int[] meetingSentences;
}

