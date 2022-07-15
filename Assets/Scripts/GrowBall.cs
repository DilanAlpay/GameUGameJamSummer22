using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowBall : MonoBehaviour
{
    public Transform face;
    public CounterObj sizeCounter;
    public List<Transform> objParents;
    private int _current = 0;
    
    
    private float[] _sizes = new float[]
    {
        1f,
        1.75f,
        2.25f,
        3f,
        4f
    };

    public void IncreaseSize()
    {
        _current++;
        transform.localScale = Vector3.one * _sizes[_current];
        face.localScale = Vector3.one;
        sizeCounter.Add(1);
    }

    public void GetItem(Transform item, int i)
    {
        item.parent = objParents[i];
        item.localPosition = Vector3.zero;
        item.localRotation = Quaternion.identity;
        item.localScale = Vector3.one;
    }
}
