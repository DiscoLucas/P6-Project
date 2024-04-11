using UnityEngine.Audio;
using UnityEngine;

public class AudioData
{
	public string name;

	public AudioClip clip;

	[Range(0f, 10f)]
	public float volume = 1f;
	[Range(-3f, 3f)]
	public float pitch = 1f;

	[Range(0f, 1)]
	public float spatialBlend = 1f;



	public bool spatialize;


	public bool loop;

	[HideInInspector]
	public float doblerEffect = 0f;

	[HideInInspector]
	public AudioSource source;

	public AudioMixerGroup mixerGroup;
	public AudioMixer mixer;
}
