using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tower : MonoBehaviour
{
    [FormerlySerializedAs("NextTileCenter")] public Vector3 NextTilePosition;
    public Vector3 NextTileScale;

    public void AppendTile(Transform tile)
    {
        
    }
}
