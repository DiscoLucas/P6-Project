using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qustion_konvertion : Qustion_FeildBoolean
{
    public GameObject convertionWindow;
    public override void calcCorrectAnswer()
    {
        correctAnswer = (_case.loan.IRPForTime[0] > _case.loan.IRPForTime[_case.loan.IRPForTime.Count - 1]);
    }

    public override void setAnswer()
    {
        base.setAnswer();
        bool startCon = (answerFeild.value == 1);
        Debug.Log("open convert" +startCon.ToString());
        convertionWindow.SetActive(startCon);
        
    }
}
