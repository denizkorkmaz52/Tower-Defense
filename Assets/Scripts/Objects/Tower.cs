using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, ISaveManager
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float damageDeflection;
    public ObjectParameters parameters;
    GameObject target;
    private bool hasTarget = false;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(FireRoutine());
    }

    // Update is called once per frame
    public void SetParamaters(int id, int type, int spawnPoint)
    {
        parameters.id = id;
        parameters.type = type;
        parameters.points = spawnPoint;
        parameters.position = transform.position;
    }
    private IEnumerator FireRoutine()
    {
        while (true)
        {
            float targetDistance = 999;
            if (target == null)
            {
                hasTarget = false; 
            }
            
            if (!hasTarget)
            {
                GameObject[] targets = GameObject.FindGameObjectsWithTag("Monster");
                foreach (var item in targets)
                {
                    float itemDistance = Mathf.Abs(Vector2.Distance(transform.position, item.transform.position));
                    if (target == null && (itemDistance < range))
                    {
                        target = item;
                        targetDistance = itemDistance;
                        hasTarget = true;
                    }
                    if (target != null && (targetDistance > itemDistance) && itemDistance < range)
                    {
                        target = item;
                        targetDistance = itemDistance;
                        hasTarget = true;
                    }
                }

            }
            else if (hasTarget)
            {
                targetDistance = Mathf.Abs(Vector2.Distance(transform.position, target.transform.position));
                if (targetDistance > range)
                {
                    hasTarget = false;
                }
                else
                {
                    Fire(target);
                    yield return new WaitForSeconds(fireRate);
                }  
            }
            
            yield return null;
        }
    }
    private void Fire(GameObject target)
    {
        if (target != null)
        {
            Bullet b = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Bullet>();
            //Debug.Log(target.name);
            b.SetParameters(damage, damageDeflection, target);
            b.StartMoving();
        }
        
    }

    public void LoadData(SaveData saveData)
    {
        if (saveData != null)
        {
            foreach (ObjectParameters tower in saveData.towers)
            {
                if (tower.id == this.parameters.id)
                {
                    this.transform.position = tower.position;
                    break;
                }
            }
        }
        
    }

    public void SaveDataFunc(ref SaveData saveData)
    {
        Debug.Log("tower eklendi");
        saveData.towers.Add(parameters);
    }
}
