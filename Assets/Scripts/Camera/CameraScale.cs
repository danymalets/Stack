using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraScale : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private TileCutter _tileCutter;
    [SerializeField] private Transform _lowerTargetPoint;
    [SerializeField] private float _scalingTime = 0.5f;
    
    private Camera _camera;
    private float _defaultSize;
    
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _defaultSize = _camera.orthographicSize;
    }

    private void OnEnable()
    {
        _gameState.GameInit += OnGameInit;
        _tileCutter.GameOver += OnGameOver;
    }

    private void OnDisable()
    {        
        _gameState.GameInit -= OnGameInit;
        _tileCutter.GameOver -= OnGameOver;
    }

    private void OnGameInit()
    {
        _camera.DOOrthoSize(_defaultSize, _scalingTime);
    }
    
    private void OnGameOver()
    {
        float screenLowerY = _camera.WorldToScreenPoint(_lowerTargetPoint.position).y;
        float scale = (_camera.pixelHeight / 2f - screenLowerY) / (_camera.pixelHeight / 2f);
        float targetSize = _defaultSize * Mathf.Max(1f, scale);
        _camera.DOOrthoSize(targetSize, _scalingTime);
    }
}
