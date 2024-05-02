using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Mail
{
    public string Reciever;
    public string Sender;
    public string Subject;
    [Multiline]
    public string info;

    public TMP_Text test;
}
