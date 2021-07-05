using System;
using System.Collections;
using UnityEngine;

public abstract class TileMovement: MonoBehaviour
{
    [SerializeField] protected Tower _tower;
    [SerializeField] private Transform _tilePrefab;
    [SerializeField] private float _distanceToCenter = 1f;
    [SerializeField] private float _speed = 1f;

    public event Action<Transform> Installed;
    protected abstract Vector3 _startDirection { get; }
    public void Awake()
    {
        _tower = GetComponent<Tower>();
    }

    public void CreateTile()
    {
        Transform tile = Instantiate(_tilePrefab, _tower.NextTilePosition - _startDirection * _distanceToCenter, Quaternion.identity);
        tile.localScale = _tower.NextTileScale;
        StartCoroutine(Move(tile));
    }
    
    private IEnumerator Move(Transform tile)
    {
        Vector3 source = _tower.NextTilePosition - _startDirection * _distanceToCenter;
        Vector3 target = _tower.NextTilePosition + _startDirection * _distanceToCenter;

        for (float distanceTraveled = 0f; !Input.GetMouseButtonDown(0); distanceTraveled += Time.deltaTime * _speed)
        {
            transform.position = Vector3.Lerp(source, target, Mathf.PingPong(distanceTraveled , 2 * _distanceToCenter));
            yield return null;
        }
        
        LetGo(tile);
    }

    private void LetGo(Transform tile)
    {
        float distance = GetDistance(tile);
        
        if (distance < 0.05f)
        {
            tile.position = _tower.NextTilePosition;
            _tower.AppendTile(tile);
        }
        else if (distance > GetScale() - 0.001f)
        {
            Transform fallingTile = Instantiate(tile);
            Rigidbody rigidBody = fallingTile.GetComponent<Rigidbody>();
            rigidBody.isKinematic = false;
            Destroy(tile);
        }
        else
        {
            Transform mainTile = Instantiate(tile, Vector3.Lerp(_tower.NextTilePosition, tile.position, 0.5f), Quaternion.identity);
            mainTile.localScale = GetMainTileScale(tile);
            
            _tower.AppendTile(mainTile);
            
            Transform fallingTile = Instantiate(tile, GetFallingTilePosition(tile), Quaternion.identity);
            fallingTile.localScale = GetFallingTileScale(tile);
            
            Rigidbody rigidBody = fallingTile.GetComponent<Rigidbody>();
            rigidBody.isKinematic = false;

            Destroy(tile);
        }
    }
    
    protected abstract float GetScale();
    protected abstract Vector3 GetMainTileScale(Transform tile);
    protected abstract Vector3 GetFallingTilePosition(Transform tile);
    protected abstract Vector3 GetFallingTileScale(Transform tile);
    protected float GetDistance(Transform tile) => Vector3.Distance(_tower.NextTilePosition, tile.position);
}   
