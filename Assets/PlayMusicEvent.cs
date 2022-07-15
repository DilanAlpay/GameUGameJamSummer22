using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicEvent : MonoBehaviour
{
    [SerializeField] AudioClip music;

    public void PlayMusic()
    {
        SoundManager.Instance.PlayMusic(music);
    }
}
