using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommentSystem : MonoBehaviour
{
    public GameObject commentsInfo;
    public Transform commentsParent;
    public string[] commentsPositive; //Array over positive kommentare
    public string[] commentsNegative; //Array over negative kommentare

    public void PosComment()
    {
        GameObject newComment = Instantiate(commentsInfo, commentsParent);

        TextMeshProUGUI commentText = newComment.GetComponentInChildren<TextMeshProUGUI>();

        commentText.text = commentsPositive[Random.Range(0,commentsPositive.Length)];
    }

    public void NegComment()
    {
        GameObject newComment = Instantiate(commentsInfo, commentsParent);

        TextMeshProUGUI commentText = newComment.GetComponentInChildren<TextMeshProUGUI>();

        commentText.text = commentsNegative[Random.Range(0, commentsNegative.Length)];
    }
}
