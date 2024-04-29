using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    Animation an;
    [SerializeField]
    string intro = "cameraswooper", outro = "cameraSwooperOut";

    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animation>();
    }

    // Update is called once per frame
    public void zoomIn()
    {
        an.Play(intro);
    }

    public void zoomOut()
    {
        an.Play(outro);
    }
}
