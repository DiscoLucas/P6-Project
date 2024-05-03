using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qustion_Convertion : chooseLoan_qustion
{
    public override void closeMeeting()
    {
        if (gameObject.active)
        {
            LoanTypes l = loans[inputField.options[inputField.value].text];
            l.installment = installmentToggel.isOn;
            _case.loan = GameManager.instance.mm.convertLoan(client, client.Finance.neededLoan, _case.loan, l);
        }
        else {
            _case.loan = GameManager.instance.mm.convertLoan(client, client.Finance.neededLoan, _case.loan, _case.loan.loanTypes);
            
        }
        Debug.Log("Is this last period: " + _case.loan.lastPeriod);
        if (_case.loan.lastPeriod)
        {

            _case.canMoveToNext = true;
        }
        else {
            _case.canMoveToNext = false;
        }
        _case.nextImportenTurn = GameManager.instance.monthNumber + _case.loan.LoanTerm;
    }
}
