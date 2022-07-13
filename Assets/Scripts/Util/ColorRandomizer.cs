using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    [SerializeField] ColorRange range;
    [SerializeField] SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
       
        sr.color = range.GetColor();
        
        
    }

    
}
