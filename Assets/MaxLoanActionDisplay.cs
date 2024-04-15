using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class MaxLoanActionDisplay : actionsDisplay
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
    MaxLoanAction infoamtion;
    [SerializeField]
    string[] tags = {
        "[Tjener]",
        "[Navn]",
        "[Gældsfactor]"
    };
    [SerializeField]
    float correctAnswer = 0;

    public override void fillOutDisplay()
    {
        infoamtion = (MaxLoanAction)originalAction;
        header.text = aName;
        string[] values = { client.monthlyIncome.ToString(), client.name, infoamtion.debtFactor.ToString() };
        string des =originalAction.replaceString(description, tags, values);
        describtion.text = des;
        correctAnswer = infoamtion.debtFactor * (12 * client.monthlyIncome);
    }
    public void typeAnswer() {
        if (answerFeild.text != null)
        {
            if(answerFeild.text.Length > 0)
                button.SetActive(true);
            else
                button.SetActive(false);
        }
        else {
            button.SetActive(false);
        }
    }
    public void giveAnswer() {
        wrongAnswer.gameObject.SetActive(false);
        if (answerFeild.text != null ) {
            float answer = float.Parse(answerFeild.text);
            if (answer == correctAnswer ||
                    (
                        answer > correctAnswer - procent(infoamtion.wronganswerprocent, correctAnswer)
                    &&
                        answer < correctAnswer + procent(infoamtion.wronganswerprocent, correctAnswer)
                    )
                )
            {
                isDone = true;
                updateActions();
            }
            else {
                wrongAnswer.gameObject.SetActive(true);
            }
        }
    }
    public float procent(float procent, float value) {
        return value * (procent / 100);
    }
}
