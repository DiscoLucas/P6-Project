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
        if (l.loanTime == 360)
        {
            _case.sentincesIndex = 0;
            _case.meetingIndex = _case.meetings.Length-1;
        }
        else {
            _case.contiuneToNextTypeOfMeeting();
        }
        
    }

    public override void setAnswer()
    {
        
    }


}
