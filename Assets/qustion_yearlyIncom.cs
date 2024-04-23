using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qustion_yearlyIncom : Qustion_NumberInput
{
    public override void fillOutHeaderAndDescribtion()
    {
        base.fillOutHeaderAndDescribtion();
    }

    public override void calcCorrectAnswer()
    {
        float incom = client.Finance.monthlyIncome;
        correctAnswer = incom * 12;
        client.Finance.yearlyIncome = correctAnswer;
    }
}
