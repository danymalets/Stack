using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLeftTileMovement : TileMovement
{
    protected override Vector3 _startDirection => Vector3.right;

    protected override float GetScale() => _tower.NextTileScale.x;
    
    protected override Vector3 GetMainTileScale(Transform tile) =>
        new Vector3(_tower.NextTileScale.x - GetDistance(tile), _tower.NextTileScale.y, _tower.NextTileScale.z);
    
    protected override Vector3 GetFallingTilePosition(Transform tile) =>
        Vector3.LerpUnclamped(_tower.NextTilePosition, tile.position, 1 + _tower.NextTileScale.x / (2 * GetDistance(tile)));
    
    protected override Vector3 GetFallingTileScale(Transform tile) =>
        new Vector3(GetDistance(tile), _tower.NextTileScale.y, _tower.NextTileScale.z);
}
