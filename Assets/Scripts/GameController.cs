using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour, ISaveManager
{
    [SerializeField] private List<GameObject> checkPoints;
    [SerializeField] private List<GameObject> towerSpawnPoints;
    [SerializeField] private List<GameObject> monsters;
    [SerializeField] private List<GameObject> towers;
    [SerializeField] private GameObject spawnPosition;
    [SerializeField] private GameObject monstersParent;
    [SerializeField] private GameObject towersParent;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Canvas inGameMenu;
    private float monsterSpawnTime = 3.5f;
    private float speedUpTime = 30f;
    private float previousTime;
    private int money = 50;
    private int cost = 10;
    private int score = 0;
    private int idForMonsters = 0;
    private int idForTowers = 0;
    private bool gameInitiated = false;
    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 0;
        previousTime = Time.time;
        UpdateTexts();
        StartCoroutine(SpawnMonster());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            inGameMenu.gameObject.SetActive(true);
        }
    }
    private IEnumerator SpawnMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(monsterSpawnTime);
            float spentTime = Time.time - previousTime;
            if (spentTime >= speedUpTime && monsterSpawnTime > 0.5f)
            {
                previousTime = Time.time;
                monsterSpawnTime -= 0.5f;
            }
            int random = Random.Range(0, monsters.Count);
            CreateMonster(idForMonsters, random, spawnPosition.transform.position, 0);
            idForMonsters++;
            
        }
    }
    private void CreateMonster(int monsterID, int monsterType, Vector3 spawnPosition, int checkPoint)
    {
        GameObject created = Instantiate(monsters[monsterType], spawnPosition, Quaternion.identity);
        created.GetComponent<Monster>().SetParameters(monsterID, monsterType, checkPoint);
        created.transform.parent = monstersParent.transform;
    }

    public void SpawnTower()
    {
        if (money >= cost && towerSpawnPoints.Count > 0)
        {
            int spawnPoint = Random.Range(0, towerSpawnPoints.Count);
            GameObject spawnP = towerSpawnPoints[spawnPoint];
            towerSpawnPoints.Remove(spawnP);
            int towerIndex = Random.Range(0, towers.Count);
            CreateTower(idForTowers, towerIndex, spawnP.transform.position, spawnPoint);
            idForTowers++;
            money -= cost;
            UpdateTexts();
        }
        
    }
    private void CreateTower(int towerID, int towerType, Vector3 spawnPosition, int spawnPoint)
    {
        GameObject created = Instantiate(towers[towerType], spawnPosition, Quaternion.identity);
        created.GetComponent<Tower>().SetParamaters(towerID, towerType, spawnPoint);
        created.transform.parent = towersParent.transform;
    }

    public void MonsterKilled(int score)
    {
        this.score += score;
        money += score;
        UpdateTexts();
    }
    public List<GameObject> GetCheckPoints() {
        return checkPoints;
    }
    public void GameLost()
    {
        Time.timeScale = 0;
    }
    public void GameWon()
    {
        Time.timeScale = 0;
    }

    public void LoadData(SaveData saveData)
    {
        if (!gameInitiated)
        {
            this.money = saveData.money;
            this.score = saveData.score;
            gameInitiated = true;
            foreach (ObjectParameters monster in saveData.monsters)
            {
                Debug.Log("Monster þekli");
                Vector3 position = new Vector3(monster.position.x, monster.position.y, monster.position.z);
                CreateMonster(monster.id, monster.type, position, monster.points);
                idForMonsters++;
            }
            foreach (ObjectParameters tower in saveData.towers)
            {
                Debug.Log("Tower þekli");
                Vector3 position = new Vector3(tower.position.x, tower.position.y, tower.position.z);
                CreateTower(tower.id, tower.type, position, tower.points);
                towerSpawnPoints.Remove(towerSpawnPoints[tower.points]);
                idForTowers++;
            }
            UpdateTexts();
            Time.timeScale = 1;
        }  
    }
    public void SaveDataFunc(ref SaveData saveData)
    {
        saveData.money = this.money;
        saveData.score = this.score;
    }
    private void UpdateTexts()
    {
        scoreText.text = "Score :" + this.score;
        moneyText.text = money.ToString();
    }
}
