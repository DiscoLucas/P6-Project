using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qustion_Convertion : chooseLoan_qustion
{
    public override void closeMeeting()
    {
        LoanTypes l = loans[inputField.options[inputField.value].text];
        l.installment = installmentToggel.isOn;
        _case.loan = GameManager.instance.mm.convertLoan(client, client.Finance.neededLoan,_case.loan ,l);
    }
}
