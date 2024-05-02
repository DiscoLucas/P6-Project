using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMeeting : MonoBehaviour
{
    CommentSystem commentSystem;
    public ClientData currentClient;
    public Case currentCase;
    public Transform qutionsParrent;
    public List<Qustion> qustions= new List<Qustion>();
    public bool allneedTobecorrect = false;
    public bool canProcede= true;
    // Start is called before the first frame update
    void Start()
    {
        commentSystem = GameManager.instance.cm;
        currentCase = GameManager.instance.setCurrentCase(this);
        currentClient = currentCase.client;
        foreach (Transform child in qutionsParrent)
        {
            Qustion q = child.GetComponent<Qustion>();
            Debug.Log("Checking For Qustion in: " + child.name + " Qustion exits: " + (q != null));
            if (q != null) {
                q.manager = this;
                q.client = currentClient;
                q._case = currentCase;
                q.init();
                qustions.Add(q);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void checkAllAnswers() {
        bool allRight = false;
        float correct = 0;
        foreach (Qustion q in qustions) {
            if (q.checkAnswer() )
            {
                correct++;
            }
            else {
                correct--;
            }
        }

        allRight = (correct == qustions.Count);
        //Debug.ClearDeveloperConsole();
        Debug.Log("Check all answers \n" + "AllRight: " + allRight + "\nallneedTobecorrect: " + allneedTobecorrect);

        if (allneedTobecorrect && allRight)
        {
            Debug.LogError("videre");
            close();
        }
        else if (!allneedTobecorrect) {
            Debug.LogError("Hurray");
            close();
        }
        else {
            Debug.LogError("NOT ALL ANSWERES ARE CORRECT");
        }
    }
    public virtual void close() {
        foreach (Qustion q in qustions) { 
            q.closeMeeting();
            if (q.isCorrect) {
                commentSystem.PosComment();
            }
            else
            {
                commentSystem.NegComment();
            }
        }
        points /= qustions.Count;
        qustions[0]._case.canMoveToNext = canProcede;
        GameManager.instance.points = points;
        GameManager.instance.closeMeeting();
    }
}
