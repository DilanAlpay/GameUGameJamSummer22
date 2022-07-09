using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialog",menuName = "Dialog/Create New Dialog")]
public class Dialog : ScriptableObject
{
    public List<Line> lines;


}
