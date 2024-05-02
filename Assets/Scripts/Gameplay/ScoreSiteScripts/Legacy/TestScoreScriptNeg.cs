using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScoreScriptNeg : MonoBehaviour
{
    public void TestButton2()
    {
        ScoreManager.Instance.MinusPoint();
    }
}
