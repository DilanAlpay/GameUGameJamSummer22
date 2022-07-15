using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    [SerializeField] ColorRange range;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] List<SpriteRenderer> others;
    // Start is called before the first frame update
    void Start()
    {       
        Color c = range.GetColor();
        sr.color = c;
        foreach(SpriteRenderer s in others)
        {
            s.color = c; 
        }   
        
    }

    
}
