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
        MISSEL
    }
    
    [SerializeField] private float speedProjectile;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private shootType currentTypeShoot;
    [NonSerialized] public Transform target;
    [NonSerialized] public int currentDamage;

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
            Deactivate();
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
            }
        }

    }

}

