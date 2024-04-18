using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailManager : MonoBehaviour
{
    [SerializeField]
    private List<Mail> mailList = new List<Mail>();

    private string baseMessage = "To {0}\nFrom: {1}\nSubject: {2}";

    [SerializeField] private GameObject mailPrefab; // Assign this to the mail button prefab

    // Start for testing :)
    private void Start()
    {
        //NewMail();
    }

    void NewMail(ClientData clientData = null)
    {
        Mail clientMail = new Mail();
        clientMail.Reciever = "Dig";
        clientMail.Sender = "BossMand";
        clientMail.Subject = "Håber du har en god dag :)"; // Opdater nok til clientData.clientName

        clientMail.info = "Mange lange meget info\n\nUndestøtter flere linjer Ö";

        // Gem denne mail som en ny mail
        mailList.Add(clientMail);
    }

    /*ChatGPT Attempt
    public ClientData clientData;
    
    private List<string> mailCases = new List<string> ();

    

   public void NewMail(ClientData clientData)
    {
        //This function will make the new cases pop up in the form of new mail.
        //It is bassicly a log over the work. 
        Gameobject newMail = Instantiate(clientData);
        newMail.GetComponent<MailManager>().UpdateContent(cases);
        mails.Add(newMail);
    }

    public void UpdateMail(ClientData clientData)
    {
       //This function is updating already existing logs/cases/mail when their are new information.
       foreach (string mail in mailCases)
        {
            mail mailScript = mail.GetComponent<Mail>();
            if (mailScript.clientName == clientData.clientName)
            {
                mailScript.UpdateContent(clientData);
                break;
            }
        }

    }
    */
}
