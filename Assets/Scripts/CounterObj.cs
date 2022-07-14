using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Counter")]
public class CounterObj : ScriptableObject
{
    [SerializeField]
    private int _count;
    public int Count => _count;

    public void SetTo(int i)
    {
        _count = i;
    }

    public void Add(int i)
    {
        _count += i;
    }
}
