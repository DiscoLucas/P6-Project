using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Qustion_Convertion : chooseLoan_qustion
{
    public bool chooseThis = false;
    public string mailHeader = "Lånet blev konverteret", mailTypeLoan = "Type: ", mailAmount = "Lånemængden: ", mailDebt = "Skylder: ";
    public override void closeMeeting()
    {
        bool lastTerm = false;
        if (chooseThis)
        {
            string key = inputField.options[inputField.value].text;
            Debug.Log(key);
            LoanTypes l = loans[key];
            l.installment = installmentToggel.isOn;
            if (l.loanTime == 360)
            {
                LoanTypes nl = new LoanTypes();
                nl.volatility = l.volatility;
                nl.interssetRate = l.interssetRate;
                nl.longTermRate = l.longTermRate;
                nl.installment = l.installment;
                nl.name = l.name;
                _case.loan.convertLoan(GameManager.instance.monthNumber, (int)(UnityEngine.Random.Range(8, 16) * 12), nl.interssetRate,nl.volatility,l);
                _case.loan.fixedIR= true;
            }
            else
            {
                _case.loan = GameManager.instance.mm.convertLoan(client, client.Finance.neededLoan, _case.loan, l);
            }
            _case.nextImportenTurn = GameManager.instance.monthNumber + _case.loan.LoanTerm;
            if (GameManager.instance.monthNumber - _case.loan.initialMonth + _case.loan.LoanTerm >= 360) {
                Debug.LogError("LAst term: " + (GameManager.instance.monthNumber - _case.loan.initialMonth + _case.loan.LoanTerm));
                lastTerm = true;
            }

        }
        else {
            _case.loan = GameManager.instance.mm.convertLoan(client, client.Finance.neededLoan, _case.loan, _case.loan.loanTypes);
            
        }
        Debug.Log("Is this last period: " + _case.loan.lastPeriod);
        _case.loan.debtAmount = _case.loanAmount * Mathf.Pow((1 + (float)_case.loan.IRForTime[_case.loan.IRForTime.Count-1]), _case.loan.LoanTerm / 12);
        if (chooseThis)
            GameManager.instance.ms.addNewInfomationToMail(_case.client, mailHeader, new string[] { header_text, mailTypeLoan + _case.loan.loanTypes.name, mailAmount + _case.loanAmount, mailDebt + _case.loan.debtAmount });
        if (lastTerm) {
            Debug.LogError("last perIOD FOR THIS LOAN " + _case.loan.loanTypes.name);
            _case.loan.lastPeriod = lastTerm;
            _case.contiuneToNextTypeOfMeeting();
        }
    }
}
