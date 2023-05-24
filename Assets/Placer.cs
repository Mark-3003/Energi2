using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Placer : MonoBehaviour
{
    public GameManager gm;
    public Vector2Int size;
    public Transform debugObj;
    public List<GameObject> baseRenderObj;

    int tileListOffset;
    int rotation;

    GameObject _newTile;
    Vector3Int previousTilePosition;
    Texture2D tx;
    void Awake()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        tx = new Texture2D(size.x, size.y);
        rotation = 0;
        tileListOffset = 0;
    }

    
    void Update() 
    {
        if(!Vector3.Equals(previousTilePosition, gm.mouseTilePosition())) {
            previousTilePosition = gm.mouseTilePosition();
            UpdateCurrentPositionTile();
        }

        if (Input.GetKeyDown(KeyCode.R))
            Rotate();
        if (Input.GetMouseButtonDown(0) && !gm.map.GetTile(gm.mouseTilePosition()))
            Place();
        if (Input.GetMouseButtonDown(1) && gm.map.GetTile(gm.mouseTilePosition()))
            DeleteTile();
        if (Input.GetKeyDown(KeyCode.Alpha1))
            OffsetListValue(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            OffsetListValue(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            OffsetListValue(2);
        Debug.Log(gm.mouseTilePosition());
        debugObj.position = gm.mouseTilePositionCentred(0);
    }
    void OffsetListValue(int _key)
    {
        tileListOffset = _key * 4;
        debugObj.GetComponent<SpriteRenderer>().sprite = gm.tiles[tileListOffset + rotation].sprite;
    }
    void Rotate()
    {
        rotation++;
        if (rotation > 3)
            rotation = 0;
        debugObj.GetComponent<SpriteRenderer>().sprite = gm.tiles[tileListOffset + rotation].sprite;
    }
    void Place()
    {
        gm.map.SetTile(gm.mouseTilePosition(), gm.tiles[tileListOffset + rotation]);

        baseRenderSelector(tileListOffset + rotation);

        UpdateCurrentPositionTile();
    }
    void DeleteTile()
    {
        gm.map.SetTile(gm.mouseTilePosition(), null);

        if (GameObject.Find(gm.mouseTilePosition().ToString()))
        {
            Destroy(GameObject.Find(gm.mouseTilePosition().ToString()));
        }
        UpdateCurrentPositionTile();
    }
    void UpdateCurrentPositionTile()
    {
        if (gm.map.GetTile(previousTilePosition) != null)
        {
            debugObj.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            debugObj.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    void baseRenderSelector(int _num)
    {
        if(_num >= 0 && _num <= 3)
        {
            _newTile = baseRenderObj[0];
            _newTile.GetComponent<SpriteRenderer>().sprite = gm.tiles[tileListOffset + rotation].sprite;
            _newTile = Instantiate(_newTile, gm.mouseTilePositionCentred(0), Quaternion.Euler(0, 0, 0));
            _newTile.GetComponent<ObjectMover>().SetStartupData(rotation, gm.mouseTilePosition());
        }
        else if(_num >= 4 && _num <= 7)
        {
            _newTile = baseRenderObj[1];
            _newTile.GetComponent<SpriteRenderer>().sprite = gm.tiles[tileListOffset + rotation].sprite;
            _newTile = Instantiate(_newTile, gm.mouseTilePositionCentred(0), Quaternion.Euler(0, 0, 0));
            _newTile.GetComponent<DEBUGmine>().SetStartupData(rotation, gm.mouseTilePosition());
        }
        else if(_num >= 8 && _num <= 11)
        {
            _newTile = baseRenderObj[2];
        }
        _newTile.name = gm.mouseTilePosition().ToString();
    }
}
