using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube
{
    private const float Epsilon = 0.001f;
    public Vector3 Position { get; }
    public Vector3 Scale { get; }

    private Vector3 _min { get; }
    private Vector3 _max { get; }

    public Cube(Vector3 position, Vector3 scale)
    {
        Position = position;
        Scale = scale;
        _min = Position - Scale / 2;
        _max = Position + Scale / 2;
    }

    public bool IsEmpty()
    {
        return (_max.x - _min.x < Epsilon) || (_max.y - _min.y < Epsilon) || (_max.z - _min.z < Epsilon);
    }
    
    private static Cube FromMinMax(Vector3 min, Vector3 max)
    {
        return new Cube(Vector3.Lerp(min, max, 0.5f), max - min);
    }
    
    public static Cube operator*(Cube a, Cube b)
    {
        return FromMinMax(Vector3.Max(a._min,b._min), Vector3.Min(a._max,b._max));
    }

    public static Cube operator-(Cube a, Cube b)
    {
        if (Mathf.Abs(a.Position.x - b.Position.x) > Mathf.Abs(a.Position.z - b.Position.z))
        {
            if (a._min.x > b._min.x)
            {
                return FromMinMax(new Vector3(Mathf.Max(b._max.x, a._min.x), b._min.y, b._min.z), a._max);
            }
            else
            {
                return FromMinMax(a._min, new Vector3(Mathf.Min(b._min.x, a._max.x), b._max.y, b._max.z));
            }
        }
        else
        {
            if (a._min.z > b._min.z)
            {
                return FromMinMax(new Vector3(b._min.x, b._min.y, Mathf.Max(b._max.z, a._min.z)), a._max);
            }
            else
            {
                return FromMinMax(a._min, new Vector3(b._max.x, b._max.y, Mathf.Min(b._min.z, a._max.z)));
            }
        }
    }
}
