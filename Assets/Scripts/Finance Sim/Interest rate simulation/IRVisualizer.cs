using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;
using XCharts.Runtime;
using TMPro;
using System;
/// <summary>
/// The class that controlls the graphs of the different loans
/// </summary>
public class IRVisualizer : MonoBehaviour
{
    [Header("Managers")]
    GameManager gameManager;
    [Header("Loan")]
    public Loan currentShowedLoan;
    public bool showIR;
    [Header("Headers/other visual elements strings")]
    public string headerIR,headerPrice;
    public string typeSubfiix = "�r", loanFast = "Fast";
    string seriesName;
    string subfix = "s l�n";
    string btn_showIR = "Rente", btn_showIRP = "Kurs", btn_flavor = "Vis";
    string xAxis = "M ", secondXAxis = " D ";
    string noDataHeader = "Ingen Data Valgt";
    [Header("UI elements")]
    public LineChart LineChartIR;
    public GameObject graphArea, NoDataArea;
    public TMP_Text buttonText, firstNumber,LastNumber;

    private void OnEnable()
    {
        LineChartIR.RemoveData();
        var title = LineChartIR.EnsureChartComponent<Title>();
        string loanTerm = noDataHeader;
    }

    /// <summary>
    /// Create the initial chart with the current interest rate.
    /// </summary>
    /// <param name="clientName"></param>
    /// <param name="interestRate">Use the initial interest rate <see cref="Loan.interestRate"/> from the <see cref="LoanManager"/></param>
    /// <returns></returns>
    public void CreateLineChart(string clientName, double interestRate)
    {
        LineChartIR.RemoveData();
        LineChartIR.AddSerie<Line>(clientName + "s l�n");
    }

    void addIRData(double interestRate) {
        float data = (float)(interestRate * 100);
        LineChartIR.AddData(0, data);
    }

    /// <summary>
    /// Update the current loans
    /// </summary>
    /// <param name="loan"></param>
    public void setCurrentShownLoan(Loan loan) {
        currentShowedLoan = loan;
    }

    /// <summary>
    /// set the correct graph active
    /// </summary>
    public void showGraph()
    {
        graphArea.SetActive(true);
        NoDataArea.SetActive(false);
    }

    public void changeWhatsIsOnGraph() {
        buttonText.text = (showIR)? btn_showIR: btn_showIRP;
        buttonText.text = btn_flavor+" " + buttonText.text;
        UpdateRateLineChart(!showIR);
    }

    /// <summary>
    /// Update the line chart infomation and the bool controls if it  which graph should be used
    /// </summary>
    /// <param name="isIR"></param>
    public void UpdateRateLineChart(bool isIR)
    {
        showIR = isIR;
        LineChart lineChart = LineChartIR;
        double[] data;
        string whatIsShowen = (showIR) ? btn_showIR : btn_showIRP;
        if (isIR) {
            data = currentShowedLoan.getInterestRate().ToArray();
            if (data.Length > 0) {
                firstNumber.text = (data[0] * 100).ToString("N2");
                LastNumber.text = (data[data.Length - 1] * 100).ToString("N2");
            }
        }
        else {
            data = currentShowedLoan.IRPForTime.ToArray();
            if (data.Length > 0)
            {
                firstNumber.text = (data[0]).ToString("N2");
                LastNumber.text = (data[data.Length - 1]).ToString("N2");
            }
        }

        CreateLineChart(lineChart);
        var title = lineChart.EnsureChartComponent<Title>();
        string loanTerm = currentShowedLoan.LoanTerm/12 + typeSubfiix;
        if (currentShowedLoan.LoanTerm == 360)
            loanTerm = loanFast;
        title.text = currentShowedLoan.clientData.clientName + " - " + loanTerm + whatIsShowen;
        
        for (int i = 0; i < data.Length; i++)
        {
            lineChart.AddXAxisData(xAxis + GameManager.instance.monthNumber + secondXAxis + (i+1));
            if(isIR)
                addIRData(data[i]);
            else
                lineChart.AddData(0, data[i]);
        }
    }
    public void CreateLineChart(LineChart lineChart) {
        lineChart.RemoveData();
        lineChart.RemoveAllSerie();
        lineChart.AddSerie<Line>(currentShowedLoan.clientData.clientName + subfix);      
    }
    private void OnApplicationQuit()
    {
        LineChartIR.RemoveData();
        LineChartIR.RemoveAllSerie();
        
    }

}
