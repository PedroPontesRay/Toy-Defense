using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speedProjectile;
    [SerializeField] private float timeToDestroy;

    private int currentDamage;

    private void OnEnable()
    {
        currentDamage = Turrent.TurrentInstance.damageInBullet;
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
        //transform.position = Vector3.MoveTowards(transform.position,currentTarget.position, speedProjectile * Time.deltaTime);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other != null)
            {
                Enemy enemyIns = other.GetComponent<Enemy>();
                enemyIns.TakeDamage(currentDamage);
                Deactivate();
                //enemyIns.Deactivate();

                //Debug.Log("Dano dado: "+MainScript.damageAmountStandarBullet);
                //Debug.Log("Bala acertada");
            }
        }

    }

}
