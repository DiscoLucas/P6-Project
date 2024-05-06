using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChatLog : MonoBehaviour //Chatlog burde g�re s�: Klik p� chatloggen > Display mails fra et tilh�rende thread.
{
    public List<MailThread> Threads; //Threads er alle de mailtr�de med de forskellige klienter som mail tabben har
    
    public void loadThread(MailThread mailThread, TextMeshPro textMeshPro) //Den her skal g�res hver
    {                                                                      //gang nogen trykker p� en klients knap
        string name = "<header>" + mailThread.Name + "</header>" + "\n";
        string allStrings = "<normal>" + string.Join("\n", mailThread.Messages) + "</normal>";
        textMeshPro.text = name + allStrings;
    }
}

//strange monolithic code
public class MailThread : MonoBehaviour //Er en mailtread for en klient. Indeholder alle beskeder klienten har sendt.
{
    //noget til den klient det g�r ud fra
    public string Name;
    public List<string> Messages; //string list af alle ting klienten har sagt

    public void NewMessage(string message) //TAG FAT I DET HER MED CLIENT TINGEN HVER GANG EN CLIENT TALER
    {
        Messages.Append(message);
    }

    //TODO - Mangler dynamsikt at tilf�je lortet
}