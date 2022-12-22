using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int score;
    public int money;
    public List<ObjectParameters> monsters;
    public List<ObjectParameters> towers;

    public SaveData()
    {
        score = 0;
        money = 50;
        monsters = new List<ObjectParameters>();
        towers = new List<ObjectParameters>();
    }
}
