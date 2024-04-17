using UnityEngine;
using XCharts.Runtime;
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
    [Tooltip("e")] public float Volatility;
    

    private void Awake()
    {
        model = new IRModel_HullWhite((double)Random.Range(1f, 100f), (double)Random.Range(0.01f, 0.05f), (double)Random.Range(0.01f, 0.1f));
        if (chart == null)
        {
            Debug.LogError("The Chart is not been added to the componet");
            chart = gameObject.GetComponent<LineChart>();
        }

        updateGraph();
    }

    public void updateGraph()
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
}

