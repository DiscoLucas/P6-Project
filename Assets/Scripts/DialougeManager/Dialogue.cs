using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //Thiss class is for storing all the dialogue properties.
    [Tooltip("Here you write the charectors name and set voice clips")]
    //public string name;
    public string[] VoiceClip;
    //[Range(0.01f, 0.1f)]
    //public float TypeSpeed = 0.04f;
    [Tooltip("minPitch and maxPitch is for setting a charectors voice pitch.")]
    [Range(-3f, 3f)]
    [SerializeField] public float minPitch = -0.5f;
    [Range(-3f, 3f)]
    [SerializeField] public float maxPitch = 1.5f;
    [Tooltip("This is where to write the sentinces a charector is saying")]
    [TextArea(3, 10)]
    public string[] sentencis;

    public bool hasRun = false;
}
