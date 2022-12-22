using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] private string saveID = "";
    [SerializeField] private GameObject empty;
    [SerializeField] private GameObject hasData;

    public void SetData(SaveData data)
    {
        if (data != null)
        {
            empty.SetActive(false);
            hasData.SetActive(true);
            
        }
        else
        {
            empty.SetActive(true);
            hasData.SetActive(false);
        }
    }
    public string GetSaveID()
    {
        return saveID;
    }

    public void SetInteractable(bool set)
    {
        this.gameObject.GetComponent<Button>().interactable = set;
    }

}
