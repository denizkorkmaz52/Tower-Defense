using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SaveManager : MonoBehaviour
{

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    [SerializeField] private Canvas saveSlotsMenu;
    private GameController gameController;
    private SaveData saveData;
    private List<ISaveManager> saveManagerObjects;
    private string selectedSaveID = "test";
    private FileManager dataManager;
    public static SaveManager instance { get; private set; }

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (instance != null)
        {
            Debug.LogError("Found more than one Save Manager script");
        }
        instance = this;
    }
    private void Start()
    {
        this.dataManager = new FileManager(Application.persistentDataPath, fileName, useEncryption);
        this.saveManagerObjects = FindAllSaveManagerObjects();
    }
    public void NewGame()
    {
        saveSlotsMenu.gameObject.SetActive(false);
        this.saveData = new SaveData();
        Time.timeScale = 1;
    }

    public void LoadGame()
    {
        saveSlotsMenu.gameObject.SetActive(false);
        this.saveData = dataManager.Load(selectedSaveID);
        if (this.saveData == null)
        {
            Debug.Log("No Saved Data found");
            NewGame();
        }
        else
        {
            gameController.LoadData(saveData);
            foreach (ISaveManager item in saveManagerObjects)
            {
                item.LoadData(saveData);
            }
        }

        
    }

    public void SaveGame()
    {
        saveManagerObjects.Clear();
        this.saveData = new SaveData();
        this.saveManagerObjects = FindAllSaveManagerObjects();
        foreach (ISaveManager item in saveManagerObjects)
        {
            item.SaveDataFunc(ref saveData);
        }
        dataManager.Save(saveData, selectedSaveID);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<ISaveManager> FindAllSaveManagerObjects()
    {
        IEnumerable<ISaveManager> saveManagerObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagerObjects);
    }

    public Dictionary<string, SaveData> GetAllSaves()
    {
        return dataManager.LoadAllSaves();
    }

    public void ChangeSelectedSaveID(string newSaveID)
    {
        selectedSaveID = newSaveID;
        // Load the game, which will use that save, updating game data accordingly 
    }
    public string GetSaveID()
    {
        return selectedSaveID;
    }
}
