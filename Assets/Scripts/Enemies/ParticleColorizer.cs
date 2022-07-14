using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColorizer : MonoBehaviour
{
    ParticleSystem particle;
    [SerializeField] ColorRange _range;
    // Start is called before the first frame update
    void Start()
    {
        SetParticleColor(_range);
    }

    public void SetParticleColor(ColorRange range)
    {
        ParticleSystem.MainModule settings = GetComponent<ParticleSystem>().main;
        settings.startColor = new ParticleSystem.MinMaxGradient(range.Color1, range.Color2);
    }
}
