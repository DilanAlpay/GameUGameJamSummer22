using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Dialog/Create New Speaker")]
public class Speaker : ScriptableObject
{
    public string speakerName;
    public Color nameBoxColor = Color.white;

    public Sprite sprite;
}
