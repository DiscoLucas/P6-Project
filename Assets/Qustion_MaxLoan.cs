using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qustion_MaxLoan : Qustion_NumberInput
{
    public override void calcCorrectAnswer()
    {
        float incomY = client.Finance.monthlyIncome*12;
        correctAnswer = incomY* client.Finance.debtFactor;
    }
}
