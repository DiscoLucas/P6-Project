using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuFunctions : MonoBehaviour
{
    //Root Menu
    public void Play()
    {
        //SceneManager.LoadScene("#DEN SCENE VI SKAL LOADE#");
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
