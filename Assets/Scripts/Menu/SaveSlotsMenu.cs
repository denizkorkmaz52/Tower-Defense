using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotsMenu : MonoBehaviour
{
    private SaveSlot[] saveSlots;
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas mainGameCanvas;

    private bool isLoadingGame = false;
    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }
    public void ActivateMenu(bool isLoadingGame)
    {
        this.gameObject.SetActive(true);
        Dictionary<string, SaveData> saves = SaveManager.instance.GetAllSaves();

        this.isLoadingGame = isLoadingGame;
        foreach (SaveSlot saveSlot in saveSlots)
        {
            SaveData saveData = null;
            saves.TryGetValue(saveSlot.GetSaveID(), out saveData);
            saveSlot.SetData(saveData);

            if (saveData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
            }
        }
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        mainMenuCanvas.gameObject.SetActive(false);
        mainGameCanvas.gameObject.SetActive(true);
        SaveManager.instance.ChangeSelectedSaveID(saveSlot.GetSaveID());
        if (!isLoadingGame)
        {
            SaveManager.instance.NewGame();
        }
        else
        {
            SaveManager.instance.LoadGame();
        }
        
    }
    public void OnBackClicked()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
