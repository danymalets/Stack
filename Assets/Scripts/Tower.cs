using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tower : MonoBehaviour
{
    private List<Transform> _tiles = new List<Transform>();
    private List<Transform> _freeTiles = new List<Transform>();
    public int Score => _tiles.Count;
    public event Action<Transform> TileInstalled;
    
    public void Append(Transform tile)
    {
        _tiles.Add(tile);
        TileInstalled?.Invoke(tile);
    }

    public void AppendFree(Transform tile)
    {
        _freeTiles.Add(tile);
    }
    
    public void Destroy()
    {
        foreach (Transform tile in _tiles) 
            Destroy(tile.gameObject);
        _tiles = new List<Transform>();
        
        foreach (Transform tile in _freeTiles) 
            Destroy(tile.gameObject);
        _freeTiles = new List<Transform>();
    }
}
