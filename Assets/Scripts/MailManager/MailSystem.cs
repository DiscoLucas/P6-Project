using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailSystem : MonoBehaviour
{
    public ClientData clientData;
    public Mail mail;
    public RectTransform rect;
    public TMP_Text clientInfo;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void TextUpdater()
    {
        string name = "Navn: " + clientData.clientName;
        string age = "Alder: " +  clientData.age;
        string job = "Job: " + clientData.job;
        string city = "By: " + clientData.city;
        clientInfo.text = name + "\n" + age + "\n" +job + "\n" + city;
    }
}
