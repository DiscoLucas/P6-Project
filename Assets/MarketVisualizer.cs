using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;

public class MarketVisualizer : MonoBehaviour
{
    public LineChart lineChart;

    public void createChart(LoanTypes[] loanTypes) {
        lineChart.RemoveData();
        foreach (LoanTypes loan in loanTypes) {
            lineChart.AddSerie<Line>(loan.name);
        }
    }

    public void updateIr(LoanTypes[] loanTypes) {
        Debug.Log("Updating market graph");
        foreach (LoanTypes loan in loanTypes)
        {
            lineChart.AddXAxisData("m" + GameManager.instance.monthNumber);
            lineChart.AddData(loan.name, loan.interssetRate * 100);
        }
    }

    private void OnApplicationQuit()
    {
        lineChart.RemoveAllSerie();
        lineChart.RemoveData();
    }
}
