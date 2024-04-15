using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HowMuchCanItCostAD : actionsDisplay
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
    float loanPayment = 100000;
    [SerializeField]
    float loan = 1344000;

    [SerializeField]
    float wronganswerprocent = 5;

    [SerializeField]
    string[] tags = {
        "[Tjener]",
        "[Navn]",
        "[loan]",
        "[Loan Payment]"
    };
    [SerializeField]
    float correctAnswer = 0;

    public override void fillOutDisplay()
    {
        header.text = aName;
        string[] values = { client.monthlyIncome.ToString(), client.name, loan.ToString(),loanPayment.ToString() };
        string des = originalAction.replaceString(description, tags, values);
        describtion.text = des;
        correctAnswer = loan + loanPayment;
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
            if (answer == correctAnswer ||
                    (
                        answer > correctAnswer - procent(wronganswerprocent, correctAnswer)
                    &&
                        answer < correctAnswer + procent(wronganswerprocent, correctAnswer)
                    )
                )
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
