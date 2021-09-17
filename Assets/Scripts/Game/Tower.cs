using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    private List<Transform> _tiles = new List<Transform>();
    public int Score { get; private set; }
    public event Action<Transform> TileInstalled;
    
    public void Append(Transform tile)
    {
        _tiles.Add(tile);
        Score++;
        TileInstalled?.Invoke(tile);
    }

    public void AppendFree(Transform tile)
    {
        _tiles.Add(tile);
    }

    public void Destroy()
    {
        foreach (Transform tile in _tiles) 
            Destroy(tile.gameObject);
        _tiles = new List<Transform>();

        Score = 0;
    }
}
