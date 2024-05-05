using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Creats the sound array
    public Sound[] soundsArray;
    public Voice[] voiceArray;
    public Music[] musicArray;


    //Singleton pattern
    public static AudioManager instance;

    void Awake()
    {
        //Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in soundsArray)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
            s.source.spatialize = s.spatialize;
            
            //Only here to disable dopplerLevel
            s.source.dopplerLevel = s.doblerEffect;

            //s.source.outputAudioMixerGroup = s.mixerGroup;
        }

        foreach (Voice v in voiceArray)
        {
            v.source = gameObject.AddComponent<AudioSource>();
            v.source.clip = v.clip;
            v.source.spatialBlend = v.spatialBlend;
            v.source.volume = v.volume;
            v.source.pitch = v.pitch;
            v.source.spatialize = v.spatialize;

            //Only here to disable dopplerLevel
            v.source.dopplerLevel = v.doblerEffect;

            //v.source.outputAudioMixerGroup = v.mixerGroup;
        }

        foreach (Music m in musicArray)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;

            m.source.volume = m.volume;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;
            m.source.spatialBlend = m.spatialBlend;

            //Only here to disable dopplerLevel
            m.source.dopplerLevel = m.doblerEffect;

            //m.source.outputAudioMixerGroup = m.mixerGroup;
        }
    }

    void Start()
    {
        Play("Electic");
        PlayMusic("Music");
    }

    //To call:
    // AudioManager.instance.Play("clip_name");
    /// <summary>
    /// This function is for selecting a specific audio clip in the Sounds catagory of the AudioManager. Input the name of a given Element.
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        Sound s = Array.Find(soundsArray, soundsArray => soundsArray.name == name);

        if (s == null)
        {
            Debug.LogWarning(name + " kinda cringe, check spelling you dylexic ass");
            return;
        }
        s.source.Play();
    }

    // AudioManager.instance.PlayVoice("clip_name");
    /// <summary>
    /// This function is for selecting a specific audio clip in the Voice catagory of the AudioManager. Input the name of a given Element.
    /// </summary>
    /// <param name="name"></param>
    public void PlayVoice(string name)
    {
        Voice v = Array.Find(voiceArray, voiceArray => voiceArray.name == name);

        if (v == null)
        {
            Debug.LogWarning("Voice " + name + " kinda cringe, check spelling you dylexic ass");
            return;
        }
        v.source.Play();
    }


    // AudioManager.instance.PlayMusic("clip_name");
    /// <summary>
    /// This function is for selecting a specific audio clip in the Music catagory of the AudioManager. Input the name of a given Element.
    /// </summary>
    /// <param name="name"></param>
    public void PlayMusic(string name)
    {
        Music m = Array.Find(musicArray, musicArray => musicArray.name == name);

        if (m == null)
        {
            Debug.LogWarning("Track" + name + "is not working, thats kinda cringe, check spelling you dylexic ass");
            return;
        }
        m.source.Play();
    }

    // AudioManager.instance.DisableAudioSource("clip_name");
    /// <summary>
    /// This funktion takes a name and disables that spicific Element if it is playing
    /// </summary>
    /// <param name="audioSourceName"></param>
    public void DisableAudioSource(string audioSourceName)
    {
        // Find the sound with the specified name
        Sound s = Array.Find(soundsArray, sound => sound.name == audioSourceName);

        // Find the voice with the specified name
        Voice v = Array.Find(voiceArray, voice => voice.name == audioSourceName);

        // Find the given music track to disable
        Music m = Array.Find(musicArray, music => music.name == audioSourceName);

        if (s != null)
        {
            if (s.source.isPlaying)
            {
                s.source.Stop();
            }
        }
        else if (v != null)
        {
            if (v.source.isPlaying)
            {
                v.source.Stop();
            }
        }
        else if (m != null)
        {
            if (m.source.isPlaying)
            {
                m.source.Stop();
            }
        }
    }

    public Voice GetVoiceClip(string name)
    {
        foreach (Voice v in voiceArray)
        {
            if(name == v.name)
            {
                return v;
            }
        }
        Debug.LogError("Clip not found");
        return null;
    }

    public Sound GetSoundClip(string name)
    {
        foreach (Sound s in soundsArray)
        {
            if (name == s.name)
            {
                return s;
            }
        }
        Debug.LogError("Clip not found");
        return null;
    }

    public Music GetMusicTrack(string name)
    {
        foreach (Music m in musicArray)
        {
            if (name == m.name)
            {
                return m;
            }
        }
        Debug.LogError("Clip not found");
        return null;
    }

    // Play a sound with random pitch within a specified range
    public void playSoundRandomPitch(string sClip, float min, float max)
    {
        float randomPitch = UnityEngine.Random.Range(min, max);
        Sound s = GetSoundClip(sClip);
        s.source.pitch = randomPitch;
        Play(sClip);
    }

    // Check if a sound clip is currently playing
    public bool isPlayingBool(string sClipPlay)
    {
        Sound s = GetSoundClip(sClipPlay);
        if (s == null)
        {
            return false;

        }
        else if (s.source.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Get a random sound clip from an array of sound clip names
    public string randomClipFromSoundArray(string[] soundClips)
    {
        string oneSoundClip = soundClips[UnityEngine.Random.Range(0, soundClips.Length)];
        Debug.LogWarning(oneSoundClip + "can not be found");
        return oneSoundClip;
    }
}
