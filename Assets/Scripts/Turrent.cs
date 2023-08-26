using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Turrent : MonoBehaviour
{
    public static Turrent TurrentInstance;
    private PoolingObj shootPooling;

    public Transform target;
    private string enemytag = "Enemy";


    [Header("Rotate-Mesh")]
    public GameObject partRotate;
    public Transform meshTurrentTrans;

    [Header("Fire Atributes")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float RangeFire;
    [SerializeField] private float fireRate;
    [SerializeField] private float updateTargetTime;
    private float fireCountDown = 0f;
    public int damageInBullet;



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
        if(nearestEnemy != null && shortest <= RangeFire)
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
            fireCountDown = 1f / fireRate;
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
        shoot.transform.position = firePoint.position;
        shoot.transform.rotation = firePoint.rotation; 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeFire);
    }
}
