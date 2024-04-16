using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Paper_work : MonoBehaviour
{
    private float annualIncome;
    private float debtFactor;
    private float debtFactorLoan;
    private float downPayment = 0.05f;
    private float maxLoan;
    private float clientSavings;
    private float currentAswer;

    [SerializeField]
    public TMP_Text canvasText;
    public TMP_Text worldText;
    public float clientIncome = 0;

    //Variables for calculating the answers
    public void clientAnnualIncome()
    {
        //Hvor meget client tjæner om året.
        //ClientIncome er en værdi som skal hives fra client data.
        annualIncome = 12 * clientIncome;
    }

    public void clientDebtFactor()
    {
        debtFactorLoan = annualIncome * debtFactor;
    }

    public void maxDownpayment()
    {
        maxLoan = debtFactorLoan + clientSavings;
    }

    //Game relatet code
    private void solutionChecker()
    {
        float wtNumber = float.Parse(worldText.text);
        if (currentAswer == wtNumber) 
        { 
            
        }
        else
        {

        }
    }

    private void currentQuestion()
    {
        currentAswer = annualIncome;
    }
}
