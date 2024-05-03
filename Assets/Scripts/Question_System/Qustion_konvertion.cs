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
    public List<string> answers = new List<string>();

    public override void fillOutHeaderAndDescribtion()
    {
        answerFeild.ClearOptions();
        Debug.Log(" DOES ANSWERFEILD ANWERS" + answerFeild.options.Count + " ANSERS COUNT: " + answers.Count);
        answerFeild.AddOptions(answers);

    }
    public override void setAnswer()
    {


        if (answerFeild.value == 0)
        {
            Debug.Log("downConvert");
            downConvertionWindow.SetActive(true);
            upConvertionWindow.SetActive(false);
            isCorrect = shouldDownConvert;

        }
        else if (answerFeild.value == 1)
        {
            Debug.Log("upConvert");
            downConvertionWindow.SetActive(false);
            upConvertionWindow.SetActive(true);
            isCorrect = !shouldDownConvert;
        }
        else {
            Debug.Log("behold");
            downConvertionWindow.SetActive(false);
            upConvertionWindow.SetActive(false);
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
