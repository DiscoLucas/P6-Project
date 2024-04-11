using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMe : MonoBehaviour
{
    public void Play(string name)
    {
        AudioManager.instance.Play(name);
    }

    public void PlayVoice(string name)
    {
        AudioManager.instance.PlayVoice(name);
    }

    public void PlayMusic(string name)
    {
        AudioManager.instance.PlayMusic(name);
    }

    public void DisableAudioSource(string audioSourceName)
    {
        AudioManager.instance.DisableAudioSource(audioSourceName);
    }
}
