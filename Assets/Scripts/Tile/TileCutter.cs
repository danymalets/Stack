using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TileCutter : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private float _allowableError = 0.05f;
    
    public event Action GameOver;
    
    public void Cut(Transform tile, Vector3 center)
    {
        if (Vector3.Distance(tile.position, center) < _allowableError)
        {
            tile.position = center;
            _tower.Append(tile);
        }
        else
        {
            Cube realCube = new Cube(tile.position, tile.localScale);
            Cube centerCube = new Cube(center, tile.localScale);

            Cube remainingCube = realCube * centerCube;
            Cube fallingCube = realCube - centerCube;

            Transform fallingTile = Instantiate(tile, fallingCube.Position, Quaternion.identity);
            fallingTile.localScale = fallingCube.Scale;
            fallingTile.GetComponent<Rigidbody>().isKinematic = false;
            _tower.AppendFree(fallingTile);
            
            if (remainingCube.IsEmpty())
            {
                Destroy(tile.gameObject);
                GameOver?.Invoke();
            }
            else
            {
                tile.position = remainingCube.Position;
                tile.localScale = remainingCube.Scale;
                _tower.Append(tile);
            }
        }
    }
}
