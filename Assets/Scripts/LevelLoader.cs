using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;

    public Slider slider;

    public void LoadLevel(int Index1)
    {
        StartCoroutine(LoadAsynchronously(Index1));
    }

    IEnumerator LoadAsynchronously(int Index1)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Index1);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            //Debug.Log(progress);

            slider.value = progress;

            yield return null;
        }
    }
}
