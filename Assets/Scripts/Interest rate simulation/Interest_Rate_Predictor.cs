using UnityEngine;
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
    [SerializeField][Tooltip("time interval increments, in months")] float dt = 1f;
    [SerializeField][Tooltip("Length of the calculated loan, in months")] float timeHorizon = 12f;
    [Tooltip("How unstable the bond is")] public double volatility;
    public double newVolatility;
    public double currentRate;
    public float addTimeHorizon = 12f;
    

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

    /// <summary>
    /// Initializes the graph with the current interest rate and the predicted interest rates for the time horizon.
    /// </summary>
    public void StartGraph()
    {
        chart.RemoveData();
        double[] predictData = model.PredictIRforTimeInterval(dt, timeHorizon);
        chart.AddSerie<Line>(seriesName);
        for (int i = 0; i < shownAmount; i++)
        {
            double d = predictData[i];
            chart.AddXAxisData("Month" + i.ToString());
            chart.AddData(0, d);
            
        }
        
    }
    
    /// <summary>
    /// Continues the simulation and sets new input values for the model.
    /// </summary>
    public void UpdateGraph() 
        // TODO: Make this method acceept input values as parameters.
    {
        model.SetVolatility(newVolatility);

        timeHorizon += addTimeHorizon; //add more time to the time horizon
        double sum = 0;
        double[] predicData = model.PredictIRforTimeInterval(dt, timeHorizon);
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

