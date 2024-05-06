using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Qustion : MonoBehaviour
{
    [SerializeField]
    [TextArea(3, 10)]
    string header_text = " ";
    [SerializeField]
    [TextArea(3, 10)]
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
    public Case _case;

    public bool checkAnswer() {
        return isCorrect;
    }

    public virtual void fillOutHeaderAndDescribtion() { 
        header.text = DialogueRegistry.instance.replaceString(header_text,_case);
        describtion.text = DialogueRegistry.instance.replaceString(describtion_text, _case);
    }
    public void init()
    {
        calcCorrectAnswer();
        fillOutHeaderAndDescribtion();
    }
    public virtual void calcCorrectAnswer() { 
    
    }
    public virtual void setAnswer() {
    
    }

    public virtual void closeMeeting() {
        
    }
}
