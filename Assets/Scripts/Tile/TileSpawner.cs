using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    
    [SerializeField] private Transform _tilePrefab;

    [SerializeField] private TileMover _tileMover;
    
    private ColorGenerator _colorGenerator;
    
    private Vector3 _nextTilePosition;
    private Vector3 _nextTileScale;
    private float _tileHeight => _tilePrefab.localScale.y;
    private void OnEnable()
    {
        _tower.TileInstalled += OnTileInstalled;
    }

    private void OnDisable()
    {
        _tower.TileInstalled -= OnTileInstalled;
    }

    public void StartGame()
    {
        _colorGenerator = new ColorGenerator();
        _tower.Destroy();
        
        _nextTilePosition = new Vector3(0f,_tileHeight / 2, 0f);
        _nextTileScale = new Vector3(1f, _tileHeight, 1f);
        CreateTile();
    }
    
    private void OnTileInstalled(Transform tile)
    {
        _nextTilePosition = tile.position + Vector3.up * _tileHeight;
        _nextTileScale = tile.localScale;
        CreateTile();
    }
    private void CreateTile()
    {
        Transform tile = Instantiate(_tilePrefab, _nextTilePosition, Quaternion.identity);
        tile.GetComponent<Renderer>().material.color = _colorGenerator.GetColor(_tower.Score);
        tile.localScale = _nextTileScale;
        StartCoroutine(_tileMover.Move(tile, _tower.Score % 2 == 0 ? new Vector3(-1, 0, 0) : new Vector3(0, 0, -1)));
    }
}