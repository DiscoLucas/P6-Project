using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qustion_Convertion : chooseLoan_qustion
{
    public bool chooseThis = false;
    public string mailHeader = "Lånet blev konverteret", mailTypeLoan = "Type: ", mailAmount = "Lånemængden: ", mailDebt = "Skylder: ";
    public override void closeMeeting()
    {
        if (chooseThis)
        {
            string key = inputField.options[inputField.value].text;
            Debug.Log(key);
            LoanTypes l = loans[key];
            l.installment = installmentToggel.isOn;
            _case.loan = GameManager.instance.mm.convertLoan(client, client.Finance.neededLoan, _case.loan, l);
            if (l.loanTime != 360) {
                _case.nextImportenTurn = GameManager.instance.monthNumber + _case.loan.LoanTerm;
            }
            else {
                _case.nextImportenTurn = GameManager.instance.monthNumber + (int)UnityEngine.Random.Range(1, 15) * 12;
            }
            Debug.Log("Updatede: " + _case.caseName + " to loantype " + l.name);
        }
        else {
            _case.loan = GameManager.instance.mm.convertLoan(client, client.Finance.neededLoan, _case.loan, _case.loan.loanTypes);
            
        }
        Debug.Log("Is this last period: " + _case.loan.lastPeriod);
        _case.loan.debtAmount = _case.loanAmount * Mathf.Pow((1 + (float)_case.loan.IRForTime[_case.loan.IRForTime.Count-1]), _case.loan.LoanTerm / 12);
        if (chooseThis)
            GameManager.instance.ms.addNewInfomationToMail(_case.client, mailHeader, new string[] { header_text, mailTypeLoan + _case.loan.loanTypes.name, mailAmount + _case.loanAmount, mailDebt + _case.loan.debtAmount });

        if (_case.loan.lastPeriod)
        {
            _case.contiuneToNextTypeOfMeeting();
        }
        else { 
            _case.sentincesIndex= 0;
        }
    }
}
