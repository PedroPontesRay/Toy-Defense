using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Turrent : MonoBehaviour
{
    public static Turrent TurrentInstance;

    private Transform target;
    private string enemytag = "Enemy";
    [Header("Rotate-Mesh")]
    public GameObject partRotate;
    public Transform meshTurrentTrans;

    [Header("Fire Atributes")]
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float attackRange;
    public float attackInterval;
    private float fireCountDown = 0f;
    [SerializeField] private float updateTargetTime;

    private PoolingObj shootPooling;


    void Start()
    {
        shootPooling = GetComponent<PoolingObj>();

        InvokeRepeating("UpdateTarget", 0f, updateTargetTime);

        if(TurrentInstance != null)
        {
            return;
        }
        TurrentInstance = this;
    }

    //update com menos atualizações
    void UpdateTarget()
    {
        //add todos os inimigos da area em um array
        GameObject[] enemies= GameObject.FindGameObjectsWithTag(enemytag);
        float shortest = Mathf.Infinity;
        GameObject nearestEnemy= null;

        //verifica a posição dos mesmos
        foreach(GameObject enemy in enemies)
        {
            float distanceEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(shortest > distanceEnemy)
            {
                shortest = distanceEnemy;
                nearestEnemy = enemy;
            }
        }

        //define qual o mais perto
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
        Vector3 rotationMesh = partRotate.transform.eulerAngles;
        meshTurrentTrans.transform.rotation = Quaternion.Euler(0, rotationMesh.y, 0);

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

        //ativar o tiro colocando e rotacionando ele na posição correta
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
