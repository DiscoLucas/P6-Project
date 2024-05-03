using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuFunctions : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    //Root Menu
    public void Play()
    {
        SceneManager.LoadScene("Scenes/GameManager");
    }

    public void Exit()
    {
        Application.Quit();
    }

    //Options Menu
    public void Volume(Slider slider)
    {
        Debug.Log(slider.value.ToString());
    }
}
