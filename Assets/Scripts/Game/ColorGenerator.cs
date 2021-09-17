using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorGenerator
{
    private const float Spread = 0.05f;
    
    private float _startHue;
    private float _brightnessSeed;
    private int _sign;
    
    public ColorGenerator()
    {
        _startHue = Random.Range(0f, 1f);
        _brightnessSeed = Random.Range(1f, 1000f);
        _sign = Random.Range(0, 2) * 2 - 1; // -1 or 1
    }
    
    public Color GetColor(int level)
    {
        float hue = ((_startHue + level * Spread * _sign) % 1f+ 1f) % 1f;
        float brightness = Mathf.PerlinNoise(_brightnessSeed, level / 4f) * 0.4f + 0.6f;
        return Color.HSVToRGB(hue, 1f, brightness);
    }
}