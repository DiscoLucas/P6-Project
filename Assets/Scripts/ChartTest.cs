using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;

public class ChartTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LineChart chart = GetComponent<LineChart>();
        if (chart == null)
        {
            chart = gameObject.AddComponent<LineChart>();
            chart.Init();
        }

        for (int i = 0; i < 10; i++)
        {
            chart.AddXAxisData(i.ToString());
            chart.AddData(i, Random.Range(0, 100));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
