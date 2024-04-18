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

    public ClientMeeting manager;
    [SerializeField]
    public bool isCorrect = false;
    public bool checkAnswer() {
        return isCorrect;
    }

    public virtual void fillOutHeaderAndDescribtion() { 
        header.text = header_text;
        describtion.text = describtion_text;
    }
    private void Awake()
    {
        fillOutHeaderAndDescribtion();
        calcCorrectAnswer();
    }
    public virtual void calcCorrectAnswer() { 
    
    }
    public virtual void setAnswer() {
    
    }
}
