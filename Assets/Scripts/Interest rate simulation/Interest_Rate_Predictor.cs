using System;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using XCharts.Runtime;
using Random = UnityEngine.Random;
/// <summary>
/// Using Hull-White model
/// </summary>
public class Interest_Rate_Predictor : MonoBehaviour
{
    [SerializeField] LineChart chart;
    [SerializeField] string seriesName = "Bond_James_bond";
    IRModel_HullWhite model;
    [SerializeField] int shownAmount = 10;
    [SerializeField][Tooltip("time interval increments, in years")] float dt = 1f / 12f;
    [SerializeField][Tooltip("Length of the calculated loan, in years")] float timeHorizon = 1f;
    [Tooltip("How unstable the bond is")] public double volatility;
    public double newVolatility;
    public double currentRate;
    public float addTimeHorizon = 1f;
    

    private void Awake()
    {
        model = new IRModel_HullWhite(currentRate, (double)Random.Range(0.01f, 0.05f), volatility);
        //chart = gameObject.GetComponent<LineChart>();
        
        // TODO: fix this.
        // when the graph is started, the last entry is always wildly different from the rest.
        // this doesn't seem to happend when StartGraph is called from the Start button.

        shownAmount = (int)(timeHorizon / dt);
        StartGraph();
    }

    public void StartGraph()
    {
        //chart.ClearSerieData();
        //chart.ClearData();
        var xAxis = chart.EnsureChartComponent<XAxis>();
        xAxis.splitNumber = 10;
        xAxis.boundaryGap = true;
        xAxis.type = Axis.AxisType.Category;

        var yAxis = chart.EnsureChartComponent<YAxis>();
        yAxis.type = Axis.AxisType.Value;
        chart.RemoveData();
        double[] predictData = model.predictIRforTimeInterval(dt, timeHorizon);
        chart.AddSerie<Line>(seriesName);
        chart.ClearData();
        chart.AddXAxisData("5");
        chart.AddData(0, 42);
        /*for (int i = 0; i < shownAmount; i++)
        {
            double d = predictData[i];
            chart.AddXAxisData("Month" + i.ToString());
            chart.AddData(0, d);
            
        }
        Debug.Log(predictData.Length);*/
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
            double d = predicData[i];
            chart.AddXAxisData(i.ToString());
            chart.AddData(0, d);
            Debug.Log(i);
            sum += d;
        }
        Debug.Log("average: " + sum / predicData.Length);
        shownAmount += (int)(addTimeHorizon / dt);
    }
}

