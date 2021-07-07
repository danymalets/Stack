using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private Tower _tower;
    [SerializeField] private float _liftingSpeed = 0.5f;
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
        StartCoroutine(Move(
            _transform.position, 
            _defaultPosition, 
            _returnTime));
    }
    
    private void OnTileInstalled(Transform tile)
    {
        StartCoroutine(Move(
            _transform.position, 
            _transform.position + Vector3.up * tile.localScale.y, 
            tile.localScale.y / _liftingSpeed));
    }

    private IEnumerator Move(Vector3 source, Vector3 target, float time)
    {
        for (float elapsedTime = 0f; elapsedTime < time; elapsedTime += Time.deltaTime)
        {
            _transform.position = Vector3.Lerp(source, target, elapsedTime / time);
            yield return null;
        }
        _transform.position = target;
    }
}
