using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Dialog/Create New Speaker")]
public class Speaker : ScriptableObject
{
    public string speakerName;
    public Color nameBoxColor = Color.white;

    public Sprite sprite;

    //Don't do this at home kids... It's halfway between a dictionary and something else.. I don't feel like creating resources and building a dictionary for each character
    public SpeakerEmotionData defaultEmotion;
    public SpeakerEmotionData happyEmotion;
    public SpeakerEmotionData sadEmotion;
    public SpeakerEmotionData angryEmotion;

    public List<AudioClip> audioClips = new List<AudioClip>();
    public bool HasAudio => audioClips.Count > 0;
 

    public Sprite GetSprite(SpeakerEmotion emotion = SpeakerEmotion.Default)
    {
        if(emotion == SpeakerEmotion.Angry && angryEmotion != null)
        {
            return angryEmotion.sprite;
        }
        else if(emotion == SpeakerEmotion.Happy && happyEmotion != null)
        {
            return happyEmotion.sprite;
        }
        else if(emotion == SpeakerEmotion.Sad && sadEmotion != null)
        {
            return sadEmotion.sprite;
        }
        else if(emotion == SpeakerEmotion.Default && defaultEmotion != null)
        {
            return defaultEmotion.sprite;
        }
        else
        {
            return null;
        }

    }
    public AudioClip GetAudioClip()
    {
        if (!HasAudio)
        {
            return null;
        }
        return audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
    }
}


public enum SpeakerEmotion
{
    Default,
    Happy,
    Sad,
    Angry
}
