using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorManager
{
    private float _rand;
    private int _sign;
    
    public ColorManager()
    {
        _rand = Random.Range(0f, 1f);
        _sign = Random.Range(0, 2) * 2 - 1; // -1 or 1
    }
    public Color GetColorByHeight(float height)
    {
        float val = (_rand + height / 30 * _sign) % 1f;
        if (val < 0f) val += 1f;
        return Color.HSVToRGB(val, 1f, 1f);
    }
}