using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speedProjectile;
    [SerializeField] private float timeToDestroy;

    private void OnEnable()
    {
        MoveProject();
        Invoke("Deactivate",timeToDestroy);
    }

    public void Update()
    {
        MoveProject();
    }

    private void MoveProject()
    {
        transform.position += transform.forward * (Time.deltaTime * speedProjectile);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy enemyIns = other.GetComponent<enemy>();
            if (enemyIns != null)
            {
                enemyIns.TakeDamage(MainScript.damageAmountMissel);
                //Debug.Log("Dano dado: "+MainScript.damageAmountStandarBullet);
                //Debug.Log("Bala acertada");
            }
            Deactivate();
        }

    }

}

