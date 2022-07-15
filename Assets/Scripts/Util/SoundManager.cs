using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

	public AudioMixer mixer;
	public AudioSource EffectsSource;
	public AudioSource MusicSource;
	
	public static SoundManager Instance = null;

	[Tooltip("0. Home 1. Forest 2. Cliffs 3. Desolate 4. Willows, 5. Bosses")]
	public List<AudioClip> areaMusic;

	public enum MusicArea
    {
		Home,
		Forest,
		Cliffs,
		Desolate,
		Willows,
		Boss
    }
	private void Awake()
	{
		
		if (Instance == null)
		{
			Instance = this;
		}
		
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	
	public void Play(AudioClip clip)
	{
		EffectsSource.clip = clip;
		EffectsSource.Play();
	}
	
	public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();
	}

	public void SetMasterVolume(float value)
	{
		mixer.SetFloat("masterVolume", value);
	}

	public void SetMusicVolume(float value)
    {
		mixer.SetFloat("musicVolume", value);
    }

	public void SetSFXVolume(float value)
	{
		mixer.SetFloat("sfxVolume", value);
	}

	public void PlayMusicArea(MusicArea area)
    {
		if(areaMusic.Count < (int)area)
        {
			AudioClip clip = areaMusic[(int)area];
			if(clip)
				PlayMusic(clip);
            else
            {
				Debug.LogWarning($"No music found for {area}");
            }
        }

    }
}