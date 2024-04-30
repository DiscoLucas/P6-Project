using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class loanSelcetor : MonoBehaviour
{
    public Loan loan;
    public TMP_Text text;
    public string sufix = "ÅR";
    public string sufixFast = "Fast";
    public IRVisualizer iRVisualizer;
    public void fillout() {
        Debug.Log(loan + " " + loan.clientData);
        string type = (loan.LoanTerm/12) + sufix;
        if (loan.LoanTerm == 360)
            type = sufixFast;
        text.text = loan.clientData.clientName + " - " + type;
    }
    public void onclick() {
        iRVisualizer.currentShowedLoan = loan;
        iRVisualizer.showGraph();
        iRVisualizer.UpdateRateLineChart(true);
    }
}
