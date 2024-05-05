using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using TMPro;
public class Qustion_NumberInput : Qustion
{
    [SerializeField]
    TMP_InputField answerFeild;
    [SerializeField]
    float answerPrecents = 5;
    [SerializeField]
    protected float correctAnswer = 1;
    protected float answer;
    float calcProcent() {
        return correctAnswer * (correctAnswer / 100);
    }
    public override void setAnswer()
    {
        answer = stringToNumber(answerFeild.text);
        float minAnswer = correctAnswer - calcProcent();
        float maxAnswer = correctAnswer + calcProcent();
        Debug.Log("given answer: " + answer + " Min answer: " + minAnswer + " max answer: " + maxAnswer);
        if (minAnswer <= answer && maxAnswer >= answer)
        {
            isCorrect = true;
            Debug.Log("IT is correct");
        }
        else {
            isCorrect = false;
            Debug.Log("WRONG!!");
        }
        
    }

    public float stringToNumber(string givenAnswer)
    {

        float answerValueFloat;
        givenAnswer = GetNumbers(givenAnswer);
        // Check if the string contains a ","
        if (givenAnswer.Contains(",") || givenAnswer.Contains("."))
        {
            if (!float.TryParse(givenAnswer, NumberStyles.Float, CultureInfo.InvariantCulture, out answerValueFloat))
            {
                string normalizedText = givenAnswer.Replace(',', '.');
                if (!float.TryParse(normalizedText, NumberStyles.Float, CultureInfo.InvariantCulture, out answerValueFloat))
                {
                    Debug.LogError(gameObject.name +  "Failed to parse float value from input text: " + givenAnswer);
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
                    Debug.LogError(gameObject.name + " Failed to parse float value from input text: " + givenAnswer);
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
