using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    private Slider slider;

    public TextMeshProUGUI scoreText;

    public float score = 0f;

    public float FillSpeed = 0.5f;

    public float targetScore = 10f;

    public float addedScore = 1f;

    private void Awake()
    {
        Instance = this;

        slider = gameObject.GetComponent<Slider>();
    }

    public void AddPoint()
    {
        score += addedScore;
        ProgressWorks(FillSpeed);
    }
    public void MinusPoint()
    {
        score -= addedScore;
        Backtrack(FillSpeed);
    }

    //Add Score to the bar
    public void ProgressWorks(float newProgress)
    {
        targetScore += newProgress;
        slider.value = targetScore;
    }

    public void Backtrack(float newProgress)
    {
        targetScore -= newProgress;
        slider.value = targetScore;
    }
}
