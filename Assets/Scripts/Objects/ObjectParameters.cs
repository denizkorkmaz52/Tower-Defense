using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectParameters
{
    public int id;
    public int type;
    public Vector3 position;
    public int points;

    public ObjectParameters(int id, int type, Vector3 position, int points)
    {
        this.id = id;
        this.type = type;
        this.position = position;
        this.points = points;
    }
}
