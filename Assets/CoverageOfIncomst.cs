using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class CoverageOfIncomst : actionsDisplay
{
    [SerializeField] TMP_Text header;
    [SerializeField] TMP_Text describtion;
    [SerializeField] TMP_InputField answerFeild;
    [SerializeField] GameObject button;
    [SerializeField] TMP_Text wrongAnswer;
    [SerializeField]
    string[] tags = {
        "[Tjener]",
        "[Navn]",
        "[Gældsfaktor]",
        "[PengeTilUdbetaling]",
    };
    [SerializeField] float correctAnswer = 0;
    internal ClientInfo client;


    public override void fillOutDisplay()
    {
        float monthlyIncome = client.monthlyIncome;
        header.text = aName;
        string[] values = { client.monthlyIncome.ToString(), client.name };
        string des = originalAction.replaceString(description, tags, values);
        describtion.text = des;
        correctAnswer = ((100000 * 100) / ((monthlyIncome * 12 * 4) + 100000));
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
                isDone = true;
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
