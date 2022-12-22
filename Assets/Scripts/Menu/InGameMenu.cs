using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    private GameController gameController;
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas mainGameCanvas;


    public void OnResumeClicked()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
    public void OnSaveClicked()
    {
        SaveManager.instance.SaveGame();
        DialogBox.instance.ShowDialogBox(SaveManager.instance.GetSaveID());
    }
    public void OnQuitClicked()
    {
        SaveManager.instance.SaveGame();
        mainMenuCanvas.gameObject.SetActive(true);
        mainGameCanvas.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
