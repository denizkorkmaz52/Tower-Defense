using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas saveSlotsMenu;
    [SerializeField] private Canvas mainGameCanvas;
    
    public void LoadClicked()
    {    
        saveSlotsMenu.GetComponent<SaveSlotsMenu>().ActivateMenu(true);
        this.gameObject.SetActive(false);
    }
    public void PlayClicked()
    {
        saveSlotsMenu.GetComponent<SaveSlotsMenu>().ActivateMenu(false);
        this.gameObject.SetActive(false);
    }

}
