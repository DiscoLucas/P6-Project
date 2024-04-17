using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class Paper_work : MonoBehaviour
{
    private const string wrongAnswer = "Wrong answer";
    private const string correctAnswer = "Correct answer";
    private float answerValueFloat;
    private float annualIncome;
    private float debtFactor;
    private float debtFactorLoan;
    private float downPayment = 0.05f;
    private float maxLoan;
    private float clientSavings;
    private float currentAnswer;

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

    private TMP_Text GetWorldText()
    {
        return worldText;
    }

    //Game relatet code

    private void Update()
    {
        currentAnswer = annualIncome;
    }
    public void solutionChecker()
    {
        answerValueFloat = float.Parse(worldText.text);
        if (currentAnswer == answerValueFloat) 
        {
            canvasText.text = correctAnswer;
        }
        else
        {
            canvasText.text = wrongAnswer;
        }

        Debug.Log(answerValueFloat);
    }

    public void Checker()
    {
        float answerValueFloat = float.Parse(worldText.text);
        Debug.Log(answerValueFloat);
        Debug.Log(worldText.text);
    }
    
}
