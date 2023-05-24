using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public List<Tile> tiles;
    public Tilemap map;

    public Vector3Int mouseTilePosition()
    {
        Vector3 _pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return map.LocalToCell(_pos);
    }
    public Vector3 mouseTilePositionCentred(float _depth)
    {
        Vector3 _pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 _newPos = map.LocalToCell(_pos);
        return new Vector3(_newPos.x + 0.5f, _newPos.y + 0.5f, _depth);
    }
    public Vector3Int positionToTilePosition(Vector2 _pos)
    {
        return map.LocalToCell(_pos);
    }
}
