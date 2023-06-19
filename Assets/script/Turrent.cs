using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Turrent : MonoBehaviour
{


    [Header("Fire Atributes")]
    private Transform target;
    private string enemytag = "Enemy";
    public GameObject partRotate;

    [Header("Fire Atributes")]
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float attackRange;
    public float attackInterval;
    private float fireCountDown = 0f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    //update com menos atualizações
    void UpdateTarget()
    {
        GameObject[] enemies= GameObject.FindGameObjectsWithTag(enemytag);
        float shortest = Mathf.Infinity;
        GameObject nearestEnemy= null;

        foreach(GameObject enemy in enemies)
        {
            float distanceEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(shortest > distanceEnemy)
            {
                shortest = distanceEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortest <= attackRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    
    private void Update()
    {
        
        if (target== null) 
            return;

        partRotate.transform.LookAt(target);

        if(fireCountDown <= 0f)
        {
            Firefunc();
            fireCountDown = 1f / attackInterval;
        }

        fireCountDown -= Time.deltaTime;
    }

    

    private void Firefunc()
    {    
        Instantiate(projectilePrefab,firePoint.transform.position,firePoint.transform.rotation);
        //Debug.Log("Fire");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
