using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Turrent : MonoBehaviour
{
    public static Turrent TurrentInstance;

    [Header("Fire Atributes")]
    public Transform target;
    private string enemytag = "Enemy";
    public GameObject partRotate;

    [Header("Fire Atributes")]
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float attackRange;
    public float attackInterval;
    private float fireCountDown = 0f;

    private PoolingObj shootPooling;


    void Start()
    {
        shootPooling = GetComponent<PoolingObj>();

        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        if(TurrentInstance != null)
        {
            return;
        }
        TurrentInstance = this;
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
        
        if (target == null) 
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
        GameObject shoot = shootPooling.GetPoolingObject();
        if(shoot == null)
        {
            shootPooling.CreatingObject();
            return;
        }
        shoot.SetActive(true);
        shoot.transform.rotation = firePoint.rotation; 
        shoot.transform.position = firePoint.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
