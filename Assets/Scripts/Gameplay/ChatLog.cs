using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChatLog : MonoBehaviour
{
   /* public List<MailThread> Threads; //Threads er alle de mailtråde med de forskellige klienter som mail tabben har
    public TextMeshProUGUI ChatLogtmpu;
    int ThreadNumber;

    // Start is called before the first frame update
    public void AppendMails()
    {
        Threads.Append(new MailThread()); //test, der skal senere dymaniskt tilføjes værdier hertil
    }

    public void UpdateThreadNumber(int threadnumber)
    {
        ThreadNumber = threadnumber;
    }
    // Update is called once per frame
    void Update()
    {
        Threads[ThreadNumber].DisplayMail(ChatLogtmpu); //display mails i den valgte tråd
    }*/
}

//strange monolithic code
public class MailThread : MonoBehaviour
{
   /* public string Name;
    public List<string> Messages;

    public void NewMessage(string message) //TAG FAT I DET HER MED CLIENT TINGEN HVER GANG EN CLIENT TALER
    {
        Messages.Append(message);
    }

    public void DisplayMail(TextMeshProUGUI tmpu) //Smider mails ind i tekstfeltet
    {
        //byg stringet af alle beskeder
        // - allebeskeder = foreach mails

        //display allebeskeder
        // - tmpu.text = allebeskeder
        Debug.Log(Messages);
    }*/
}