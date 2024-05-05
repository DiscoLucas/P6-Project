using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Qustion_RedeemLoan : Qustion
{
    public TMP_Dropdown dropdown;
    public List<string> answers;
    public GameObject convertLoanWindow;
    public override void calcCorrectAnswer()
    {
        isCorrect= true;
        dropdown.AddOptions(answers);
    }

    public override void setAnswer()
    {
        convertLoanWindow.SetActive((dropdown.value > 0));
    }

    public override void closeMeeting()
    {
        if((dropdown.value == 0))
            GameManager.instance.csm.getCurrentCase().closeCase = true;
    }
}
