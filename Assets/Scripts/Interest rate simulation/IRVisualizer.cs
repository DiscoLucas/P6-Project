using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;
using XCharts.Runtime;
public class IRVisualizer : MonoBehaviour
{
    [Header("References")]
    GameManager gameManager;
    public LineChart LineChart { get {  return LineChart; } }

    string chartTitle;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Create the initial chart with the current interest rate.
    /// </summary>
    /// <param name="clientName"></param>
    /// <param name="interestRate">Use the initial interest rate <see cref="Loan.interestRate"/> from the <see cref="LoanManager"/></param>
    /// <returns></returns>
    public LineChart CreateLineChart(string clientName, double interestRate)
    {
        LineChart.RemoveData();
        LineChart.AddSerie<Line>(clientName + "s lån");
        LineChart.AddXAxisData("Måned 0");
        LineChart.AddData(0, interestRate);
        
        return LineChart;
    }

    public void UpdateLineChart(string clientName, double interestRate)
    {
        LineChart.AddXAxisData("Måned " + gameManager.monthNumber);
        LineChart.AddData(0, interestRate);

        // convert interest rate list to array
        double[] interestRates = gameManager.loanManager.GetIRHistory(clientName).ToArray();

        for (int i = 0; i < interestRates.Length; i++)
        {
            LineChart.AddXAxisData("Måned " + (i + 1));
            LineChart.AddData(0, interestRates[i]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    int TimeStepsSinceLastTurn(string clientName)
    {
        int timeSteps = (sbyte)(gameManager.monthNumber - gameManager.loanManager.GetLoanProperty(clientName, typeof(int), "InitialMonth"));
        return timeSteps;
    }
    
}
