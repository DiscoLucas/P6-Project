using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qustion_MaxLoan : Qustion_NumberInput
{
    public GameObject nextButton;
    public override void calcCorrectAnswer()
    {
        float incomY = client.Finance.monthlyIncome*12;
        correctAnswer = incomY* client.Finance.debtFactor;
    }

    public override void setAnswer()
    {
        base.setAnswer();
        if (answer > 0)
        {
            nextButton.SetActive(true);
        }
        else {
            nextButton.SetActive(false);
        }
    }

    public override void closeMeeting()
    {
        base.closeMeeting();
        _case.loanAmount = answer;
    }
}
