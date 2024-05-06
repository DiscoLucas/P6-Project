using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qustion_Convertion : chooseLoan_qustion
{
    public bool chooseThis = false;
    public override void closeMeeting()
    {
        if (chooseThis)
        {
            string key = inputField.options[inputField.value].text;
            Debug.Log(key);
            LoanTypes l = loans[key];
            l.installment = installmentToggel.isOn;
            _case.loan = GameManager.instance.mm.convertLoan(client, client.Finance.neededLoan, _case.loan, l);
            Debug.Log("Updatede: " + _case.caseName + " to loantype " + l.name);
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
