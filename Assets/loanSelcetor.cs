using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Class on the button that tells when the main virsualizer should update and the infotion
/// </summary>
public class LoanSelcetor : MonoBehaviour
{
    [Header("The Loan that is visualized")]
    /// <summary>
    /// The loan they the button is showing
    /// </summary>
    public Loan loan;
    [Header("UI elements")]
    public TMP_Text text;
    public IRVisualizer iRVisualizer;
    [Header("The sufix used in UI ")]
    public string sufix = "ÅR";
    public string sufixFast = "Fast";

    /// <summary>
    /// Fillout the ui elements on the button
    /// </summary>
    public void fillout() {
        Debug.Log(loan + " " + loan.clientData);
        string type = (loan.LoanTerm/12) + sufix;
        if (loan.LoanTerm == 360)
            type = sufixFast;
        text.text = loan.clientData.clientName + " - " + type;
    }

    /// <summary>
    /// This function called when button clicked and it update the graph
    /// </summary>
    public void onclick() {
        iRVisualizer.currentShowedLoan = loan;
        iRVisualizer.showGraph();
        iRVisualizer.UpdateRateLineChart(true);
    }
}
