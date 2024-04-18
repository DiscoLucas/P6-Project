using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMeeting : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void close() {
        GameManager.instance.nextMounth();
    }
}
