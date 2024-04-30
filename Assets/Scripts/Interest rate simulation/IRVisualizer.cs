using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;
using XCharts.Runtime;
public class IRVisualizer : MonoBehaviour
{
    [Header("References")]
    GameManager gameManager;
    public LineChart LineChartIR;
    public string headerIR,headerPrice;
    public string typeSubfiix = "år", loanFast = "Fast";
    public LineChart LineChartPrice;
    public Loan currentShowedLoan;
    string seriesName;
    string subfix = "s lån";
    string xAxis = "M ",secondXAxis = " D ";
    public GameObject graphArea, NoDataArea;
    // Start is called before the first frame update
    void Start()
    {
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
        LineChartIR.AddSerie<Line>(clientName + "s lån");
        LineChartIR.AddXAxisData("Måned 0");
        LineChartIR.AddData(0, interestRate);
    }

    public void setCurrentShownLoan(Loan loan) {
        currentShowedLoan = loan;
    }

    public void showGraph()
    {
        graphArea.SetActive(true);
        NoDataArea.SetActive(false);
    }
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
        
       /* LineChart.AddData(0, );

        // convert interest rate list to array
        double[] interestRates = gameManager.loanManager.GetIRHistory(clientName).ToArray();

        for (int i = 0; i < TimeStepsSinceLastTurn(clientName); i++)
        {
            LineChart.AddXAxisData(xAxis + (i + 1));
            LineChart.AddData(0, interestRates[i]);
        }*/
    }
    public void CreateLineChart(LineChart lineChart) {
        lineChart.RemoveData();
        lineChart.AddSerie<Line>(currentShowedLoan.clientData.clientName + subfix);
        lineChart.AddXAxisData(xAxis + 0);
        lineChart.AddData(0, currentShowedLoan.getFirstInterestRate());
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /*int TimeStepsSinceLastTurn(string clientName)
    {
        // Calculate the months since the loan was created
        int timeSteps = (sbyte)(gameManager.monthNumber - gameManager.loanManager.GetLoanProperty(clientName, typeof(int), "InitialMonth"));
        // return the average time steps between each interest rate update
        return timeSteps / gameManager.loanManager.GetIRHistory(clientName).Count;
    }*/

    int TimeStepsSinceLastTurn(Loan loan)
    {
        // Calculate the months since the loan was created
        int timeSteps = GameManager.instance.monthNumber - loan.initialMonth;
        // return the average time steps between each interest rate update
        return timeSteps / loan.getInterestRate().Count;
    }

}
