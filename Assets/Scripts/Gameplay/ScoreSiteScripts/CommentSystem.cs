using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommentSystem : MonoBehaviour
{
    public GameObject commentsInfo;
    public Transform commentsParent;

    public void PosComment()
    {
        GameObject newComment = Instantiate(commentsInfo, commentsParent);

        TextMeshProUGUI commentText = newComment.GetComponentInChildren<TextMeshProUGUI>();

        commentText.text = "Fuck du er dygtig";

        //Debug.Log("Fuck du er dygtig");
    }

    public void NegComment()
    {
        GameObject newComment = Instantiate(commentsInfo, commentsParent);

        TextMeshProUGUI commentText = newComment.GetComponentInChildren<TextMeshProUGUI>();

        commentText.text = "Idioten er ikke god til at regne kursen ud";

        //Debug.Log("Idioten er ikke god til at regne kursen ud");
    }
}
