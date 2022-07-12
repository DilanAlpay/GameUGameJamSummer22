using System;
using UnityEngine;

[Serializable]
public class Line
{
    [TextArea]
    public string text;
    public Speaker speaker;
    public SpeakerEmotion emotion;
    public int speakerNum = 0;
}
