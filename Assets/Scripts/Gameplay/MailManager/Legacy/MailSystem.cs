using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MailSystem : MonoBehaviour
{
    Thread[] Threads;

    // Start is called before the first frame update
    void Start()
    {
        Threads.Append(new Thread());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var thread in Threads)
        {
            Debug.Log(thread.name);
        }
    }
}

//strange monolithic code

public class Thread : MonoBehaviour
{
    public string Name = "Bob";
    public string[] Messages;
    void Start()
    {
        Messages.Append("Hey");
    }
}