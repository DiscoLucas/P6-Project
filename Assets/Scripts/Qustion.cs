using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Qustion : MonoBehaviour
{
    [SerializeField]
    string header_text = " ";
    [SerializeField]
    string describtion_text = " ";
    [SerializeField]
    TMP_Text header;
    [SerializeField]
    TMP_Text describtion;
    [SerializeField]
    public ClientData client;
    public ClientMeeting manager;
    [SerializeField]
    public bool isCorrect = false;
    public bool checkAnswer() {
        return isCorrect;
    }

    public virtual void fillOutHeaderAndDescribtion() { 
        header.text = DialogueRegistry.instance.replaceString(header_text,client);
        describtion.text = DialogueRegistry.instance.replaceString(describtion_text, client);
    }
    public void init()
    {
        fillOutHeaderAndDescribtion();
        calcCorrectAnswer();
    }
    public virtual void calcCorrectAnswer() { 
    
    }
    public virtual void setAnswer() {
    
    }
}
