using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicEvent : MonoBehaviour
{
    [Header("Not required if using music area")]
    [SerializeField] AudioClip music;
    [SerializeField] SoundManager.MusicArea area;
    public void PlayMusic()
    {
        if (SoundManager.Instance)
            SoundManager.Instance.PlayMusic(music);
    }

    public void PlayMusicArea()
    {
        if(SoundManager.Instance)
            SoundManager.Instance.PlayMusicArea(area);
    }
}
