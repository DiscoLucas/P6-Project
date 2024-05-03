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
        _case.nextImportenTurn = GameManager.instance.monthNumber + _case.loan.LoanTerm;
        _case.loan.debtAmount = _case.loanAmount * Mathf.Pow((1 + (float)_case.loan.IRForTime[_case.loan.IRForTime.Count-1]), _case.loan.LoanTerm / 12);
        if (_case.loan.lastPeriod)
        {
            _case.contiuneToNextTypeOfMeeting();
        }
        else { 
            _case.sentincesIndex= 0;
        }
    }
}
