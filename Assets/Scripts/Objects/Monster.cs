using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class Monster : MonoBehaviour, ISaveManager
{
    [SerializeField] private string type;
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float missChance;
    [SerializeField] private int score;
    public ObjectParameters parameters;
    List<GameObject> checkPoints;
    private int passedPoints;
    GameController controller;
    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        checkPoints = controller.GetCheckPoints();        
    }
    private void Update()
    {
        parameters.position = transform.position;
    }
    public void SetParameters(int id, int type, int passedPoints)
    {
        parameters.id = id;
        parameters.type = type;
        this.passedPoints = passedPoints;
        MoveToNextCheckPoint();
    }
    private void MoveToNextCheckPoint()
    {
        int startPoint = passedPoints;
        Debug.Log(passedPoints);
        var sequence = DOTween.Sequence();
        for (int i = startPoint; i < checkPoints.Count; i++)
        {
            sequence.Append(transform.DOMove(checkPoints[i].transform.position, speed).SetEase(Ease.Linear).OnComplete(() => {
                passedPoints++;
                parameters.points = passedPoints;
            }));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameLost();
        }
    }
    public void TakeDamage(float damage)
    {
        //Debug.Log("damage = " + damage);
        int chance = Random.Range(1, 100);
        if (chance < missChance)
        {
            //Debug.Log("Missed");
        }
        else
        {
           health -= damage;
        }

        if (health <= 0)
        {
            List<Tween> tweens = DOTween.TweensByTarget(gameObject);
            if (tweens != null)
            {
                foreach (var tween in tweens)
                {
                    if (tween.IsActive())
                    {
                        DOTween.Kill(tween);
                    }
                }
            }
            controller.MonsterKilled(score);
            Destroy(gameObject);
        }
    }

    public void LoadData(SaveData saveData)
    {
        if (saveData != null)
        {
            foreach (ObjectParameters monster in saveData.monsters)
            {
                if (monster.id == this.parameters.id)
                {
                    this.transform.position = monster.position;
                    break;
                }
            }
        }
      
    }

    public void SaveDataFunc(ref SaveData saveData)
    {
        Debug.Log("Monster eklendi");
        saveData.monsters.Add(parameters);       
    }
}
