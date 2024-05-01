using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Qustion_FeildInput : Qustion
{
    [SerializeField]
    protected TMP_Dropdown answerFeild;
    [SerializeField]
    protected string[] wrongAnsers;
    [SerializeField]
    protected string correctAnswer;



    public override void fillOutHeaderAndDescribtion()
    {
        base.fillOutHeaderAndDescribtion();
        answerFeild.ClearOptions();
        List<string> answers = new List<string>(wrongAnsers);
        answers.Add(correctAnswer);
        for (int i = 0; i<answers.Count; i++) { 
            string value = answers[i];
            int index = UnityEngine.Random.Range(0, answers.Count);
            answers[i] = answers[index];
            answers[index] = value;
        }

        answerFeild.AddOptions(answers);
        
    }
    public override void setAnswer()
    {
        if (answerFeild.options[answerFeild.value].text == correctAnswer)
        {
            isCorrect = true;
        }
        else {
            isCorrect= false;
        }

    }
}
