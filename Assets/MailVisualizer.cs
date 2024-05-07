using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class MailVisualizer : MonoBehaviour
{
    public TMP_Text text;
    public List<MailText> mailInfomatio;
    public MailSystem mailsys;
    public string styleH, styleN,from = "Fra:\n";
    public void addInfomation(string header, string[] text) {
        MailText mt = new MailText();
        mt.header = header;
        mt.text = text;
        mailInfomatio.Add(mt);
    }
    public void updateClientInfo(ClientData client) {
        text.text = from + client.clientName;
    }
    public void MailText(MailText mt) {
        mailInfomatio.Add(mt);
    }

    public void onClick() {
        string info = "";
        foreach (MailText m in mailInfomatio) {
            info += "\n" + styleH + m.header;
            for (int i = 0; i < m.text.Length; i++) {
                info += "\n" + styleN + m.text[i];
            }
        }

        mailsys.addMail(info);
    }
}
[Serializable]
public struct MailText {
    public string header;
    public string[] text;
}
