using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class chooseLoan_qustion : Qustion
{
    public TMP_Dropdown inputField;
    public Toggle installmentToggel;
    public Dictionary<string, LoanTypes> loans = new Dictionary<string, LoanTypes>();
    public string sufix = "%",ir_Name = "Rente";
    public bool doNotTakeAll = false;
    public bool ir_lower = false;
    public string headerMail = "Oprettede lån",mailTypeLoan1 = "Type: ", mailAmount2 = "Lånemængden: ", mailDebt4 = "Skylder: ";
    public bool chooseOwnLoan = false;
    public override void calcCorrectAnswer()
    {
        
        isCorrect = true;
        LoanTypes[] lt= GameManager.instance.mm.loanTypes;
        inputField.ClearOptions();
        List<string> list= new List<string>();
        foreach (LoanTypes loant in lt) {
            if (doNotTakeAll) {

                if (ir_lower)
                {
                    if (_case.loan.IRForTime[_case.loan.IRForTime.Count - 1] < loant.interssetRate) {
                        continue;
                    }
                }
                else {
                    if (_case.loan.IRForTime[_case.loan.IRForTime.Count - 1] > loant.interssetRate)
                    {
                        continue;
                    }
                }
            }
            if (_case.loan != null) {
                if (!chooseOwnLoan && _case.loan.loanTypes.name == loant.name) {
                    continue;
                }
            }
            string ir = string.Format("{0:N2}", (loant.interssetRate*100).ToString("N2"));
            string key = loant.name + "-" + ir_Name + ":" + ir + sufix;
            loans.Add(key, loant);
            list.Add(key);

        }
        inputField.AddOptions(list);
    }



    public override void closeMeeting()
    {
 
        LoanTypes l = loans[inputField.options[inputField.value].text];
        l.installment = installmentToggel.isOn;
        _case.loan = GameManager.instance.mm.createLoan(client, _case.loanAmount, l);
        Debug.Log("Next mounth somthing happends: " + (GameManager.instance.monthNumber + _case.loan.LoanTerm) + " " + _case.loan.loanTypes.name);
        _case.nextImportenTurn = GameManager.instance.monthNumber + _case.loan.LoanTerm;
        _case.loan.debtAmount = _case.loanAmount * Mathf.Pow((1 + (float)_case.loan.interestRate), _case.loan.LoanTerm / 12);
        GameManager.instance.ms.addNewInfomationToMail(_case.client, headerMail, new string[] { mailTypeLoan1 + l.name, mailAmount2 + _case.loanAmount, mailDebt4 + _case.loan.debtAmount });
        if (l.loanTime == 360)
        {
            _case.loan.fixedIR = true;
            _case.sentincesIndex = 0;
            _case.contiuneToNextTypeOfMeeting();
            _case.nextImportenTurn = GameManager.instance.monthNumber + (int)UnityEngine.Random.Range(1, 5)*12;
        }
        else {
            _case.contiuneToNextTypeOfMeeting();
            _case.loan.fixedIR = false;
        }
        
    }

    public override void setAnswer()
    {
        
    }


}
