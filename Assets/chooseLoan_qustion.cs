using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseLoan_qustion : Qustion_FeildInput
{
    public List<LoanTemplate> loanTypes;
    public override void calcCorrectAnswer()
    {
        //v�lg rigigte l�n type

        //s�t i rigtigt svar

        //tag de fokerte l�ntyper og put i liste med forkerte svar

    }

    public override void setAnswer()
    {
        base.setAnswer();
        //s�t clienten til at have dette l�n
    }
}
