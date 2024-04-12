using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class IncomeActionDisplay : actionsDisplay
{
    [SerializeField]
    TMP_Text header;
    [SerializeField]
    TMP_Text describtion;
    [SerializeField]
    TMP_InputField answerFeild;
    [SerializeField]
    GameObject button;
    [SerializeField]
    TMP_Text wrongAnswer;
    [SerializeField]
    string[] tags = {
        "[Tjener]",
        "[Navn]",
    };
    [SerializeField]
    float correctAnswer = 0;

    public override void fillOutDisplay()
    {
        float monthlyIncome = yeEvent.client.monthlyIncome;
        header.text = yeEvent.aName;
        string[] values = { yeEvent.client.monthlyIncome.ToString(), yeEvent.client.name };
        string des = yeEvent.originalAction.replaceString(yeEvent.description, tags, values);
        describtion.text = des;
        correctAnswer = (monthlyIncome * 12);
    }
    public void typeAnswer()
    {
        if (answerFeild.text != null)
        {
            if (answerFeild.text.Length > 0)
                button.SetActive(true);
            else
                button.SetActive(false);
        }
        else
        {
            button.SetActive(false);
        }
    }
    public void giveAnswer()
    {
        wrongAnswer.gameObject.SetActive(false);
        if (answerFeild.text != null)
        {
            float answer = float.Parse(answerFeild.text);
            Debug.Log("This to check " + answer);
            if (answer == correctAnswer)
            {
                yeEvent.isDone = true;
                updateActions();
            }
            else
            {
                wrongAnswer.gameObject.SetActive(true);
            }
        }
    }
    public float procent(float procent, float value)
    {
        return value * (procent / 100);
    }
}
