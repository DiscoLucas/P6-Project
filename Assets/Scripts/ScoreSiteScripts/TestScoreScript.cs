using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScoreScript : MonoBehaviour
{
   //To test if the slider score thing works
   public void TestButton()
    {
       ScoreManager.Instance.AddPoint();
    }
}
