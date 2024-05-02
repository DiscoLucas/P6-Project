using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qustion_konvertion : Qustion_FeildBoolean
{
    public override void calcCorrectAnswer()
    {
        correctAnswer = (_case.loan.IRPForTime[0] > _case.loan.IRPForTime[_case.loan.IRPForTime.Count - 1]);
    }
}
