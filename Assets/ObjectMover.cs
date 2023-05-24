using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public ObjectMover nxTile;
    public DEBUGrefinery nxBuilding;
    public GameManager gm;

    public List<Transform> obj;
    public List<float> timer;
    Vector3Int _position;

    public int rotation;
    public float speed;
    public bool moving;
    void Awake()
    {
        moving = false;

        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        obj = new List<Transform>();
        timer = new List<float>();
    }
    void Update()
    {
        if (nxTile != null)
            moving = nxTile.onTheMove();
        if (timer.Count > 0 && obj.Count > 0)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                if (nxTile != null && (nxTile.isEmpty() || moving))
                {
                    obj[i].position = Vector2.Lerp(transform.position, nxTile.transform.position, Time.realtimeSinceStartup - timer[i]);
                }
                else if(nxBuilding != null)
                {
                    moving = true;
                    obj[i].position = Vector2.Lerp(transform.position, nxBuilding.transform.position, Time.realtimeSinceStartup - timer[i]);
                }
                else
                {
                    timer[i] += Time.deltaTime;
                    //obj[i].position = Vector2.Lerp(transform.position, new Vector2(nextSpace().x + 0.5f, nextSpace().y + 0.5f), Time.realtimeSinceStartup - timer[i]);
                }
                if (Time.realtimeSinceStartup - timer[i] >= 1)
                {
                    if (nxTile != null)
                        nxTile.SetNewObj(obj[i]);
                    if (nxBuilding != null)
                        nxBuilding.SetNewObj(obj[i]);
                    obj.RemoveAt(i);
                    timer.RemoveAt(i);
                }
            }
        }
    }
    public void SetStartupData(int _rot, Vector3Int _pos) {
        rotation = _rot;
        _position = _pos;
        nextForwardSpace();
        CheckInDirection();
    }
    public void AddNextTileData(ObjectMover _nextTile) {
        nxTile = _nextTile;
    }
    public void SetNewObj(Transform _obj)
    {
        timer.Add(Time.realtimeSinceStartup);
        obj.Add(_obj);
    }
    void CheckInDirection()
    {
        Vector3Int _checkPos = new Vector3Int();
        Vector3Int _checkPos2 = new Vector3Int();
        Vector3Int _checkPos3 = new Vector3Int();

        switch (rotation)
        {
            case 0:
                _checkPos = _position - new Vector3Int(1, 0, 0);
                _checkPos2 = _position + new Vector3Int(0, 1, 0);
                _checkPos3 = _position - new Vector3Int(0, 1, 0);
                break;
            case 1:
                _checkPos = _position - new Vector3Int(0, 1, 0);
                _checkPos2 = _position + new Vector3Int(1, 0, 0);
                _checkPos3 = _position - new Vector3Int(1, 0, 0);
                break;
            case 2:
                _checkPos = _position + new Vector3Int(1, 0, 0);
                _checkPos2 = _position + new Vector3Int(0, 1, 0);
                _checkPos3 = _position - new Vector3Int(0, 1, 0);
                break;
            case 3:
                _checkPos = _position + new Vector3Int(0, 1, 0);
                _checkPos2 = _position + new Vector3Int(1, 0, 0);
                _checkPos3 = _position - new Vector3Int(1, 0, 0);
                break;
        }
        if (GameObject.Find(_checkPos.ToString()))
        {
            GameObject.Find(_checkPos.ToString()).GetComponent<ObjectMover>().nextForwardSpace();
        }
        if (GameObject.Find(_checkPos2.ToString()))
        {
            GameObject.Find(_checkPos2.ToString()).GetComponent<ObjectMover>().nextForwardSpace();
        }
        if (GameObject.Find(_checkPos3.ToString()))
        {
            GameObject.Find(_checkPos3.ToString()).GetComponent<ObjectMover>().nextForwardSpace();
        }
    }
    void nextForwardSpace(){
        Vector3Int _checkPos = new Vector3Int();
        _checkPos = nextSpace();

        if (GameObject.Find(_checkPos.ToString()))
        {
            if(GameObject.Find(_checkPos.ToString()).GetComponent<ObjectMover>())
                nxTile = GameObject.Find(_checkPos.ToString()).GetComponent<ObjectMover>();
            else if (GameObject.Find(_checkPos.ToString()).GetComponent<DEBUGrefinery>())
                nxBuilding = GameObject.Find(_checkPos.ToString()).GetComponent<DEBUGrefinery>();
        }
    }
    public bool isEmpty()
    {
        return (obj.Count > 0) ? false : true;
    }
    public bool onTheMove()
    {
        if (nxTile != null)
            return moving || nxTile.isEmpty();
        else
            return false;
    }
    Vector3Int nextSpace()
    {
        switch (rotation)
        {
            case 0:
                return _position + new Vector3Int(1, 0, 0);
            case 1:
                return _position + new Vector3Int(0, 1, 0);
            case 2:
                return _position - new Vector3Int(1, 0, 0);
            case 3:
                return _position - new Vector3Int(0, 1, 0);
            default:
                return Vector3Int.zero;
        }
    }
}
