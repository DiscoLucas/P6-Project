using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseManager : MonoBehaviour
{
    public static CaseManager instance;
    public Case _case;

    [Header("Client Meeting")]
    [SerializeField]
    public Case[] clientMeetingsTemplates;
    public int clientMeetIndex = -1;
    [SerializeField]
    public ClientMeeting currentClientMeeting;
    [SerializeField] public Transform clientMeetingTransform;

    private void Awake()
    {
        //Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Uga()
    {

    }
}
