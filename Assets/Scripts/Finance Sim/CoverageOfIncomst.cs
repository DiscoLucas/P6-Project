using UnityEngine;
using TMPro;

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


    public override void fillOutDisplay()
    {
        float monthlyIncome = actionClient.Finance.monthlyIncome;
        header.text = aName;
        string[] values = { actionClient.Finance.monthlyIncome.ToString(), actionClient.clientName };
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
                AudioManager.instance.Play("Correct");
                isDone = true;
                updateActions();
            }
            else
            {
                AudioManager.instance.Play("Wrong");
                wrongAnswer.gameObject.SetActive(true);
            }
        }
    }
    public float procent(float procent, float value)
    {
        return value * (procent / 100);
    }
}
