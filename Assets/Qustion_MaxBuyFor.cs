using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qustion_MaxBuyFor : Qustion_NumberInput
{
    public override void calcCorrectAnswer()
    {
        /////
        float loanMax = client.Finance.monthlyIncome * 12 * client.Finance.debtFactor;
        correctAnswer = loanMax+client.Finance.totalSavings;
    }
}
