using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGmine : MonoBehaviour
{
    public ObjectMover nxTile;
    public DEBUGrefinery nxBuilding;
    
    public GameObject obj;

    public Vector3Int _position;
    public int rotation;
    public float timer;
    void Awake()
    {
        nextForwardSpace();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.5f)
        {
            timer -= 0.5f;
            SpawnNewObj();
        }
    }
    public void SetStartupData(int _rot, Vector3Int _pos)
    {
        rotation = _rot;
        _position = _pos;
    }
    void SpawnNewObj()
    {

    }
    void nextForwardSpace()
    {
        Vector3Int _checkPos = new Vector3Int();
        _checkPos = nextSpace();

        if (GameObject.Find(_checkPos.ToString()))
        {
            if (GameObject.Find(_checkPos.ToString()).GetComponent<ObjectMover>())
                nxTile = GameObject.Find(_checkPos.ToString()).GetComponent<ObjectMover>();
            else if (GameObject.Find(_checkPos.ToString()).GetComponent<DEBUGrefinery>())
                nxBuilding = GameObject.Find(_checkPos.ToString()).GetComponent<DEBUGrefinery>();
        }
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
