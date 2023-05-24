using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGconyer : MonoBehaviour
{
    public ObjectMover tile;
    public bool put;

    void Update()
    {
        if (put)
        {
            put = false;
            tile.SetNewObj(gameObject.transform);
        }
    }
}
