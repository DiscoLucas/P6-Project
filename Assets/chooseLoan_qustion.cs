using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class chooseLoan_qustion : Qustion_FeildInput
{
    public List<LoanTemplate> loanTypes;
    public override void calcCorrectAnswer()
    {
        //vælg rigigte lån type

        //sæt i rigtigt svar
        LoanTypes[] lt = GameManager.instance.mm.loanTypes;
        wrongAnsers = Array.ConvertAll(lt, x => x.name);
        correctAnswer = wrongAnsers[0];
        //tag de fokerte låntyper og put i liste med forkerte svar

    }

    public override void setAnswer()
    {
        base.setAnswer();
    }

    public override void closeMeeting()
    {
        //sæt clienten til at have dette lån
        LoanTypes lt = null;
        foreach (LoanTypes loanT in GameManager.instance.mm.loanTypes)
        {
            if (loanT.name == answerFeild.options[answerFeild.value].text)
            {
                lt = loanT;
                Debug.Log("Loan: " + loanT.name + " Was created");
                break;
            }
        }
        GameManager.instance.mm.createLoan(client, client.Finance.neededLoan, lt);
    }
}
