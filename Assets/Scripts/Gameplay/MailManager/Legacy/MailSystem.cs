using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;
using TMPro;
public class MailSystem : MonoBehaviour
{
    Dictionary<string, MailVisualizer> mails = new Dictionary<string, MailVisualizer>();
    public Transform mailButtonContainer;
    public GameObject mailprefab;
    public TMP_Text mailfeild;
    public void addNewInfomationToMail(ClientData c, string header, string[] mailInfo) {
        MailVisualizer mailVis = null;
        Debug.Log("Update To Mail " + c.clientName);
        try {
            mailVis = mails[c.clientName];
        }
        catch (Exception e) {
            Debug.Log(e);
        }

        if (mailVis == null)
        {
            Debug.Log("Create mail for " + c.clientName);
            var obj = Instantiate(mailprefab, mailButtonContainer);
            var vis = obj.GetComponent<MailVisualizer>();
            mails.Add(c.clientName, vis);
            vis.addInfomation(header, mailInfo);
            vis.mailsys = this;
            vis.updateClientInfo(c);
        }
        else 
        {
            Debug.Log("Update mail for " + c.clientName);
            mailVis.addInfomation(header,mailInfo);
        }
    }

    public void addMail(string s) {
        mailfeild.text = s;

    }

    /* Thread[] Threads;

     // Start is called before the first frame update
     void Start()
     {
         Threads.Append(new Thread());
     }

     // Update is called once per frame
     void Update()
     {
         foreach (var thread in Threads)
         {
             Debug.Log(thread.name);
         }
     }*/
}

//strange monolithic code

/*public class Thread : MonoBehaviour
{
    public string Name = "Bob";
    public string[] Messages;
    void Start()
    {
        Messages.Append("Hey");
    }
}*/