using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public string[] VoiceClip;
    [Range(0.01f, 0.1f)]
    public float TypeSpeed = 0.04f;

    [Range(-3f, 3f)]
    [SerializeField] public float minPitch = -0.5f;
    [Range(-3f, 3f)]
    [SerializeField] public float maxPitch = 1.5f;

    [TextArea(3, 10)]
    public string[] sentensis;

    public bool hasRun = false;


}
