using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using System.Globalization;
using System.Linq;

public class Paper_work : ClientMeeting
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
        answerValueFloat = stringToNumber(worldText.text);
        Debug.Log("The given answer: " +answerValueFloat + " \nThe Correct answer: " + currentAnswer);
        if (currentAnswer == answerValueFloat) 
        {
            canvasText.text = correctAnswer;
        }
        else
        {
            canvasText.text = wrongAnswer;
        }

        
    }

    public float stringToNumber(string givenAnswer)
    {
        Debug.Log(worldText.text);

        float answerValueFloat;
        givenAnswer = GetNumbers(givenAnswer);
        // Check if the string contains a ","
        if (worldText.text.Contains(","))
        {
            if (!float.TryParse(givenAnswer, NumberStyles.Float, CultureInfo.InvariantCulture, out answerValueFloat))
            {
                string normalizedText = givenAnswer.Replace(',', '.');
                if (!float.TryParse(normalizedText, NumberStyles.Float, CultureInfo.InvariantCulture, out answerValueFloat))
                {
                    Debug.LogError("Failed to parse float value from input text: " + worldText.text);
                    return -111111;
                }
            }
        }
        else
        {
            if (!float.TryParse(givenAnswer, NumberStyles.Float, CultureInfo.InvariantCulture, out answerValueFloat))
            {
                int intValue;
                if (!int.TryParse(GetNumbers(givenAnswer), out intValue))
                {
                    Debug.LogError("Failed to parse float value from input text: " + worldText.text);
                    return -111111;
                }
                answerValueFloat = (float)intValue;
            }
        }

        return answerValueFloat;
    }

    private string GetNumbers(string input)
    {
        return new string(input.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
    }





}
