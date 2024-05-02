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

    public override void calcCorrectAnswer()
    {
        
        isCorrect = true;
        LoanTypes[] lt= GameManager.instance.mm.loanTypes;
        inputField.ClearOptions();
        List<string> list= new List<string>();
        foreach (LoanTypes loant in lt) {
            string ir = string.Format("{0:N2}", (loant.interssetRate*100).ToString());
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
        _case.loan = GameManager.instance.mm.createLoan(client, client.Finance.neededLoan, l);
        Debug.Log("Next mounth somthing happends: " + (GameManager.instance.monthNumber + _case.loan.LoanTerm));
        _case.nextImportenTurn = GameManager.instance.monthNumber + _case.loan.LoanTerm;
    }

    public override void setAnswer()
    {
        
    }


}
