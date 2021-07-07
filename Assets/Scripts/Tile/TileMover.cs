using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TileMover: MonoBehaviour
{
    [SerializeField] private TileCutter _tileCutter;
    [SerializeField] private float _distanceToCenter = 1f;
    [SerializeField] private float _speed = 1f;

    public IEnumerator Move(Transform tile, Vector3 startDirection)
    {
        Vector3 center = tile.position;
        Vector3 source = center - startDirection * _distanceToCenter;
        Vector3 target = center + startDirection * _distanceToCenter;
        
        for (float distanceTraveled = 0f; !Input.GetMouseButtonDown(0); distanceTraveled += Time.deltaTime * _speed)
        {
            tile.position = Vector3.Lerp(source, target, Mathf.PingPong(distanceTraveled , 1));
            yield return null;
        }

        yield return null;

        _tileCutter.Cut(tile, center);
    }
}   
