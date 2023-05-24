using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGrefinery : MonoBehaviour
{
    public int objects;
    public Vector3Int _position;
    public void SetNewObj(Transform _obj)
    {
        // GET OBJ DATA
        objects++;
        Destroy(_obj.gameObject);
    }
}
