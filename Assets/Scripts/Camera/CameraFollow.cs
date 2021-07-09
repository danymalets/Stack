using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;


[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private Tower _tower;
    
    [SerializeField] private float _liftingTime = 0.4f;
    [SerializeField] private float _returnTime = 0.5f;
    
    private Transform _transform;
    private Vector3 _defaultPosition;
    
    private void Awake()
    {
        _transform = transform;
        _defaultPosition = _transform.position;
    }

    private void OnEnable()
    {
        _gameState.GameInit += OnGameInit;
        _tower.TileInstalled += OnTileInstalled;
    }

    private void OnDisable()
    {
        _gameState.GameInit -= OnGameInit;
        _tower.TileInstalled -= OnTileInstalled;
    }

    private void OnGameInit()
    {
        _transform.DOMove(_defaultPosition, _returnTime);
    }
    
    private void OnTileInstalled(Transform tile)
    {
        _transform.DOMove(_transform.position + Vector3.up * tile.localScale.y, _liftingTime);
    }
}
