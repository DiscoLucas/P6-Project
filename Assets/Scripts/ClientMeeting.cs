using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMeeting : MonoBehaviour
{
    public ClientData currentClient;
    public Transform qutionsParrent;
    public List<Qustion> qustions= new List<Qustion>();
    public bool allneedTobecorrect = false;
    public void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        currentClient = GameManager.instance.setCurrentClientMeeting(this);
        foreach (Transform child in qutionsParrent)
        {
            Qustion q = child.GetComponent<Qustion>();
            if (q != null) {
                q.manager = this;
                q.client = currentClient;
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
        }
        GameManager.instance.closeMeeting();
    }
}
