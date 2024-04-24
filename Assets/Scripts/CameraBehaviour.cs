using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    Animation an;

    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animation>();
    }

    // Update is called once per frame
    public void zoomIn()
    {
        an.Play("cameraswooper");
    }

    public void zoomOut()
    {
        an.Play("cameraswooper");
    }
}
