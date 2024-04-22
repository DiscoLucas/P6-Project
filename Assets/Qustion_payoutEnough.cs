using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qustion_payoutEnough : Qustion_FeildBoolean
{
    public float percentesEnough = 5;
    public override void calcCorrectAnswer()
    {
        float loan = client.Finance.monthlyIncome * 12 * client.Finance.debtFactor + client.Finance.totalSavings;
        float perc = client.Finance.totalSavings * 100 / loan;
        correctAnswer = (percentesEnough <= perc);
    }
}
