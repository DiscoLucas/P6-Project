using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;
using XCharts.Runtime;
/// <summary>
/// The class that controlls the graphs of the different loans
/// </summary>
public class IRVisualizer : MonoBehaviour
{
    [Header("Managers")]
    GameManager gameManager;
    [Header("Loan")]
    public Loan currentShowedLoan;
    [Header("Headers/other visual elements strings")]
    public string headerIR,headerPrice;
    public string typeSubfiix = "år", loanFast = "Fast";
    string seriesName;
    string subfix = "s lån";
    string xAxis = "M ", secondXAxis = " D ";
    [Header("UI elements")]
    public LineChart LineChartPrice;
    public LineChart LineChartIR;
    public GameObject graphArea, NoDataArea;

    /// <summary>
    /// Create the initial chart with the current interest rate.
    /// </summary>
    /// <param name="clientName"></param>
    /// <param name="interestRate">Use the initial interest rate <see cref="Loan.interestRate"/> from the <see cref="LoanManager"/></param>
    /// <returns></returns>
    public void CreateLineChart(string clientName, double interestRate)
    {
        LineChartIR.RemoveData();
        LineChartIR.AddSerie<Line>(clientName + "s lån");
        LineChartIR.AddXAxisData("Måned 0");
        LineChartIR.AddData(0, interestRate);
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

    /// <summary>
    /// Update the line chart infomation and the bool controls if it  which graph should be used
    /// </summary>
    /// <param name="isIR"></param>
    public void UpdateRateLineChart(bool isIR)
    {
        LineChart lineChart;
        double[] data = currentShowedLoan.getInterestRate().ToArray();
        if (isIR) {
            lineChart = LineChartIR;
        }
        else {
            lineChart = LineChartPrice;
        }
        CreateLineChart(lineChart);
        var title = lineChart.EnsureChartComponent<Title>();
        string loanTerm = currentShowedLoan.LoanTerm/12 + typeSubfiix;
        if (currentShowedLoan.LoanTerm == 360)
            loanTerm = loanFast;
        title.text = currentShowedLoan.clientData.clientName + " - " + loanTerm;
        for (int i = 0; i < data.Length; i++)
        {
            lineChart.AddXAxisData(xAxis + GameManager.instance.monthNumber + secondXAxis + (i+1));
            lineChart.AddData(0, data[i]);
        }
    }
    public void CreateLineChart(LineChart lineChart) {
        lineChart.RemoveData();
        lineChart.AddSerie<Line>(currentShowedLoan.clientData.clientName + subfix);
        lineChart.AddXAxisData(xAxis + 0);
        lineChart.AddData(0, currentShowedLoan.getFirstInterestRate());
    }

}
