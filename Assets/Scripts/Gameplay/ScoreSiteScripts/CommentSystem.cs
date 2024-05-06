using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommentSystem : MonoBehaviour
{
    public TextMeshProUGUI commentText;
    public TextMeshProUGUI scoreText;
    public string[] commentsPositive; //Array over positive kommentare
    public string[] commentsNegative; //Array over negative kommentare
    public int score;
    public int bestPossibleScore;

    public void PosComment()
    {
        score += 2;
        bestPossibleScore += 2;
        commentText.text = commentsPositive[Random.Range(0, commentsPositive.Length)];
        scoreText.text = "Score: " + score;
    }

    public void NegComment()
    {
        score -= 1;
        bestPossibleScore += 2;
        commentText.text = commentsNegative[Random.Range(0, commentsNegative.Length)];
        scoreText.text = "Score: " + score;
    }
}
