using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailManager : MonoBehaviour
{
    [SerializeField]
    private List<Mail> mailList = new List<Mail>();

    private string baseMessage = "To {0}\nFrom: {1}\nSubject: {2}";

    public GameObject mailPrefab; // Assign this to the AMail button prefab

    public GameObject mailInfo;    // Assign this to the Mail RawImage prefab

    public Transform parentObject, MailAnchorPostion;
    
    public int clientData;

    [SerializeField] public TMP_Text reciver;
    [SerializeField] public TMP_Text sender;
    [SerializeField] public TMP_Text subject;
    [SerializeField] public TMP_Text info;

    public DialogueRegistry dialogueRegistry;

    // Start for testing :)
    private void Start()
    {
        mailPrefab.SetActive(true);
        //NewMail(clientData);
    }

    void NewMail(int clientData)
    {


        Mail clientMail = new Mail();
        reciver.text = clientMail.Reciever;
        sender.text = clientMail.Sender;
        subject.text = clientMail.Subject; // Opdater nok til clientData.clientName

        info.text = clientMail.info;

        // Gem denne mail som en ny mail
        mailList.Add(clientMail);
    }

    public void MailSetActive()
    {
        
        GameObject newMailObject = Instantiate(mailPrefab, parentObject,false);
        Vector3 originalMailPosition = MailAnchorPostion.position ;
        MailSystem mailSystem = newMailObject.GetComponent<MailSystem>();
        if (mailSystem == null)
            Debug.LogError("Mail system not found");
        newMailObject.transform.position = originalMailPosition- (mailSystem.rect.rect.height*mailList.Count)*Vector3.up;

        mailList.Add(null);
    }

    public void UpdateMailInfo()
    {
        /*info.text = DialogueManager.instance.thisSentince(clientData);
        Debug.Log(DialogueManager.instance.thisSentince(clientData));*/
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
