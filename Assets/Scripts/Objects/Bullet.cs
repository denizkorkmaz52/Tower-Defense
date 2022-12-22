using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public float damage;
    private GameObject target;

    public void SetParameters(float baseDamage, float damageDeflection, GameObject target)
    {
        this.target = target;
        float deflection = Random.Range(-damageDeflection, damageDeflection);
        damage = baseDamage + deflection;
    }
    public void StartMoving()
    {
        StartCoroutine(MovingToTarget());
    }
    private IEnumerator MovingToTarget()
    {
        var elapsedTime = 0f;
        float moveTime = 0.4f;
        var startPos = transform.position;
        while (elapsedTime < moveTime)
        {
            if (target != null)
            {
                transform.position = Vector3.Lerp(startPos, target.transform.position, elapsedTime / moveTime);
                elapsedTime += Time.deltaTime;
                //transform.position = Vector3.MoveTowards(transform.position, moveToPosition.position, 1f * Time.deltaTime);
                //Debug.Log("ssss222");
            }
            else
                Destroy(gameObject);

            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("dafaf");
        if (collision.CompareTag("Monster"))
        {
            DOTween.Kill(gameObject);
            collision.GetComponent<Monster>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
