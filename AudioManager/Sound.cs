using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound: ISound
{

	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = .75f;
	[Range(0f, 1f)]
	public float volumeVariance = .1f;

	[Range(.1f, 3f)]
	public float pitch = 1f;
	[Range(0f, 1f)]
	public float pitchVariance = .1f;

	public bool loop = false;

	//public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source;

	public void SetSource(AudioSource source)
	{
		this.source = source;

		source.clip = clip;
		source.loop = loop;

		//source.outputAudioMixerGroup = mixerGroup;
	}

	public void Play()
	{
		source.volume = volume * (1f + Random.Range(-volumeVariance / 2f, volumeVariance / 2f));
		source.pitch = pitch * (1f + Random.Range(-pitchVariance / 2f, pitchVariance / 2f));

		source.Play();
	}

	public float GetLength() {
		return clip.length;
	}

}
