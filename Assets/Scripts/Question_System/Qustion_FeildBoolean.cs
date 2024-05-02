using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Qustion_FeildBoolean : Qustion
{
    [SerializeField]
    protected TMP_Dropdown answerFeild;
    [SerializeField]
    protected bool correctAnswer = false;


    public override void fillOutHeaderAndDescribtion()
    {
        base.fillOutHeaderAndDescribtion();
        answerFeild.ClearOptions();
        List<string> answers = new List<string>();
        answers.Add("Nej"); answers.Add("Ja");
        answerFeild.AddOptions(answers);

    }
    public override void setAnswer()
    {
        if (answerFeild.value == Convert.ToInt32(correctAnswer))
        {
            isCorrect = true;
        }

    }
}
