using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PointScreen : MonoBehaviour
{
    public TMP_Text header;
    public string headerText = "Du fik", sufix = "points";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null) {
            string head = headerText + " " + GameManager.instance.pointsCol.x + "/" + GameManager.instance.pointsCol.y + sufix;
            Debug.Log("The new header should be: " + head);
            header.text = head;
            GameManager.instance.destoryAllManagers();
        }
    }

    public void goBackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
