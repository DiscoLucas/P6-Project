using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Qustion_konvertion : Qustion
{

    [SerializeField]
    protected TMP_Dropdown answerFeild;
    [SerializeField]
    protected bool shouldDownConvert = false;
    public GameObject downConvertionWindow,upConvertionWindow;
    public Qustion_Convertion downCS, upCS;
    public List<string> answers = new List<string>();

    public override void fillOutHeaderAndDescribtion()
    {
        base.fillOutHeaderAndDescribtion();
        answerFeild.ClearOptions();
        Debug.Log(" DOES ANSWERFEILD ANWERS" + answerFeild.options.Count + " ANSERS COUNT: " + answers.Count);
        answerFeild.AddOptions(answers);

    }
    public bool checkAnswer(int index) {
        return (answerFeild.options[answerFeild.value].text == answers[index]);
    }

    public override void setAnswer()
    {

        if (checkAnswer(0)) { 
        
        }else if (checkAnswer(1))
        {
            Debug.Log("downConvert");
            downConvertionWindow.SetActive(true);
            upConvertionWindow.SetActive(false);
            downCS.chooseThis = true;
            upCS.chooseThis = false;
            isCorrect = shouldDownConvert;

        }
        else if (checkAnswer(2))
        {
            Debug.Log("upConvert");
            downConvertionWindow.SetActive(false);
            upConvertionWindow.SetActive(true);
            downCS.chooseThis = false;
            upCS.chooseThis = true;
            isCorrect = !shouldDownConvert;
        }
        else {
            Debug.Log("behold");
            downConvertionWindow.SetActive(false);
            upConvertionWindow.SetActive(false);
            downCS.chooseThis = false;
            upCS.chooseThis = false;
            isCorrect = !shouldDownConvert;
        }


        bool startCon = (answerFeild.value == 1);
        Debug.Log("open convert" + startCon.ToString());

    }
    public override void calcCorrectAnswer()
    {
        shouldDownConvert = (_case.loan.IRForTime[0] > _case.loan.IRPForTime[_case.loan.IRForTime.Count - 1]);
    }

    public override void closeMeeting()
    {
        base.closeMeeting();
    }

}
