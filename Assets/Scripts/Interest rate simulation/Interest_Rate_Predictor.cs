using System;
using UnityEngine;
using XCharts.Runtime;
using Random = UnityEngine.Random;
/// <summary>
/// Using Hull-White model
/// </summary>
public class Interest_Rate_Predictor : MonoBehaviour
{
    [SerializeField] BaseChart chart;
    [SerializeField] string seriesName = "Bond_James_bond";
    IRModel_HullWhite model;
    [SerializeField] int shownAmount = 10;
    [SerializeField][Tooltip("time interval increments, in years")] float dt = 1f / 12f;
    [SerializeField][Tooltip("Length of the calculated loan, in years")] float timeHorizon = 1f;
    [Tooltip("e")] public double volatility;
    public double newVolatility;
    public float addTimeHorizon = 1f;
    

    private void Awake()
    {
        model = new IRModel_HullWhite((double)Random.Range(1f, 100f), (double)Random.Range(0.01f, 0.05f), volatility);
        if (chart == null)
        {
            Debug.LogError("The Chart is not been added to the componet");
            chart = gameObject.GetComponent<LineChart>();
        }
        // TODO: fix this.
        // when the graph is started, the last entry is always wildly different from the rest.
        // this doesn't seem to happend when StartGraph is called from the Start button.
        StartGraph();
    }

    public void StartGraph()
    {
        //chart.ClearSerieData();
        //chart.ClearData();
        chart.RemoveData();
        double[] predicData = model.predictIRforTimeInterval(dt, timeHorizon);
        chart.AddSerie<Line>(seriesName);
        for (int i = 0; i < shownAmount; i++)
        {
            double d = predicData[i];
            chart.AddXAxisData(i.ToString());
            chart.AddData(0, d);
            Debug.Log(i);
        }
    }

    public void UpdateGraph() 
        // TODO: fix the graph update
        // Curently the update is not working as expected
        // When the graph is updated, the first entry is very different.
        // but the following the entrys look stable like it's following the previous volatility value.
    {
        model.UpdateVolatility(newVolatility);

        timeHorizon += addTimeHorizon; //add more time to the time horizon
        double sum = 0;
        double[] predicData = model.predictIRforTimeInterval(dt, timeHorizon);
        for (int i = shownAmount; i < shownAmount + (int)(addTimeHorizon / dt); i++)
        {
            double d = predicData[i - shownAmount];
            chart.AddXAxisData(i.ToString());
            chart.AddData(0, d);
            Debug.Log(i);
            sum += d;
        }
        Debug.Log("average: " + sum / predicData.Length);
        shownAmount += (int)(addTimeHorizon / dt);
    }
}

