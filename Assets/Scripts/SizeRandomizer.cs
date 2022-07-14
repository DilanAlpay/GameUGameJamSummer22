using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeRandomizer : MonoBehaviour
{
    [SerializeField] float range;

    public void RandomizeSize()
    {
        float newScale = Random.Range(-range, range);
        transform.localScale *= newScale;
    }
    
}
