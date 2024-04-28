using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseLoan_qustion : Qustion_FeildInput
{
    public List<LoanTemplate> loanTypes;
    public override void calcCorrectAnswer()
    {
        //vælg rigigte lån type

        //sæt i rigtigt svar

        //tag de fokerte låntyper og put i liste med forkerte svar

    }

    public override void setAnswer()
    {
        base.setAnswer();
        //sæt clienten til at have dette lån
    }
}
