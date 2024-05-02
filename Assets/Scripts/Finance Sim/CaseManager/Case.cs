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
    public Loan loan;
    [Tooltip("Which meeting they need to be in")]public int meetingIndex = 0;
    public int sentincesIndex = 0;
    public bool caseClosed = false;
    public bool canMoveToNext = false;
    public bool needLoan;
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


    public MeetingCollection getCurrentMeeting() {
        return meetings[meetingIndex];
    }
    public bool checkIfDoneTalking() {
        return (sentincesIndex < meetings[meetingIndex].meetingSentences.Length);
    }

    public bool checkIfCaseIsDone() {
        caseClosed = (meetingIndex >= meetings.Length);
        Debug.Log("Meeting Done and there are more : " + !caseClosed);
        return caseClosed;
    }

    public void goToNextClientMeeting() {
        if (meetings[meetingIndex].needToFinnishToProgress)
        {
            meetingIndex++;
            sentincesIndex = 0;
            canMoveToNext = false;
        }
        else {
            Debug.Log("Kan stop converting " + canMoveToNext);
            if (canMoveToNext) {
                meetingIndex++;
            }
            sentincesIndex = 0;
            canMoveToNext = false;
        }
    }

    public int returnSentince()
    {
        int i = meetings[meetingIndex].meetingSentences[sentincesIndex];
        sentincesIndex++;
        return i;
    }

    public int assistantSentinceReturn()
    {
        int i = meetings[meetingIndex].assistantSaulSentensis[sentincesIndex];
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
    public bool needToFinnishToProgress;
    public int[] assistantSaulSentensis;
}

