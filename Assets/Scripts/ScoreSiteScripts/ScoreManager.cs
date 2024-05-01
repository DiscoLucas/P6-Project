using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

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

    // Start is called before the first frame update
    void Start()
    {
        //To test if the ProgressWorks works:
        //ProgressWorks(0.75f);

        scoreText.text = score.ToString() + "Percents";
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < targetScore)
        {
            slider.value += FillSpeed * Time.deltaTime;
        }
            
    }

    public void AddPoint()
    {
        score += addedScore;
        scoreText.text = score.ToString() + "Percents";
        ProgressWorks(FillSpeed);
    }

    //Add Score to the bar
    public void ProgressWorks(float newProgress)
    {
        targetScore += newProgress;
        slider.value = targetScore;
    }
}
