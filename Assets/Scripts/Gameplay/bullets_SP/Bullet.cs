using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum shootType
    {
        GUMBAL,
        MISSEL,
        ICESHOOT
    }
    
    [SerializeField] private shootType currentTypeShoot;
    private float speedProjectile;
    private float timeToDestroy;
    [NonSerialized] public Transform target;
    [NonSerialized] public int currentDamage;

    //Slow
    private float currentSlowSpeed;
    private float currentSpeedTime;

    private void Awake()
    {
        if (currentTypeShoot == shootType.ICESHOOT)
        {
            speedProjectile = 30f;
            timeToDestroy = 0.6f;
            currentSlowSpeed = 1.0f;
            currentSpeedTime = 3.0f;
        }
        else if (currentTypeShoot == shootType.MISSEL)
        {
            speedProjectile = 2f;
            timeToDestroy = 5f;
        }
        else if (currentTypeShoot == shootType.GUMBAL)
        {
            speedProjectile = 30f;
            timeToDestroy = 0.6f;
        }
    }

    private void OnEnable()
    {
        Invoke("Deactivate",timeToDestroy);
    }

    public void Update()
    {
        MoveProject(currentTypeShoot);
    }

    private void MoveProject(shootType currentType)
    {
        if (target == null)
        {
            gameObject.SetActive(false);
            return;
        }
        if (currentType == shootType.GUMBAL)
        {
            transform.position += transform.forward * (Time.deltaTime * speedProjectile);
        }
        else if (currentType == shootType.MISSEL) 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speedProjectile * Time.deltaTime);
            transform.LookAt(target);
        }
        else if (currentType == shootType.ICESHOOT)
        {
            transform.position += transform.forward * (Time.deltaTime * speedProjectile);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemyIns = other.GetComponent<Enemy>();

            enemyIns.TakeDamage(currentDamage);

            if(currentTypeShoot == shootType.ICESHOOT)
            {

                GameObject[] enemyINGAME = GameObject.FindGameObjectsWithTag("Enemy"); 

                for(int i = 0; i < enemyINGAME.Length;i++)
                {
                    enemyINGAME[i].GetComponent<Enemy>().SlowStateFunc(currentSpeedTime, currentSlowSpeed);
                }

            }

            Deactivate();
        }

    }

}

