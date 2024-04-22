using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assistant : MonoBehaviour
{
    public Animation walk;
    public int dialoguToSay;
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            assistantStartTalk();
        }

        if (Input.GetKeyDown("v"))
        {
            assistantNewLine();
        }
    }

    public void assistantStartTalk()
    {
        walk.Play();
        DialogueManager.instance.StartDia(dialoguToSay);
    }

    public void assistantNewLine()
    {
        DialogueManager.instance.nextSentince = dialoguToSay;
    }
}
