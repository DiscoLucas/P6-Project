using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ClientMeetingInfomation
{
    [SerializeField]
    private string meetingId;
    [SerializeField]
    public GameObject meetingPrefab;
    public int[] sentinces;
    public int senIndex = 0;
    public string getMeetingId() {
        return meetingId;
    }

    public void resetSentinces() { 
        senIndex= 0;
    }

    public bool updateSenIndex() {
        senIndex++;

        if (senIndex >= sentinces.Length) {
            resetSentinces();
            return false;
        }

        return true;
    }
    public int returnSentince()
    {
        return sentinces[senIndex];
    }

}
