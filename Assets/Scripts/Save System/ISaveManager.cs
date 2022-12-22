using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager
{
    void LoadData(SaveData saveData);
    void SaveDataFunc(ref SaveData saveData);
}
