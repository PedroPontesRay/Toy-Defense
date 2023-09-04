using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Turrent : MonoBehaviour
{
    private enum typeTurrent
    {
        NONE,
        GUMBALL,
        MISSEL,
        SOLDIER,
        CROCO,
        PIRATE
    }

    private PoolingObj shootPooling;
    private GameObject target;
    private string enemytag = "Enemy";



    [Header("Fire Atributes")]
    [SerializeField] private typeTurrent currentTypeTurrent;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float RangeFire;
    [SerializeField] private float fireRate;
    [SerializeField] private float updateTargetTime;
    private float fireCountDown = 0f;
    public int damageInBullet;

    [Header("Rotate-Mesh")]
    public GameObject partRotate;
    public Transform meshTurrentTrans;


    void OnEnable()
    {
        shootPooling = GetComponent<PoolingObj>();
        switch(currentTypeTurrent)
        {
            case typeTurrent.NONE:
                Debug.LogError("TIPO NÃO SETADO");
                break; 
            case typeTurrent.GUMBALL:
                InvokeRepeating("UpdateTargetByDistance", 0f, updateTargetTime);
                break;
            case typeTurrent.MISSEL:
                InvokeRepeating("UpdateTargetByLifeEnemy", 0f, updateTargetTime);
                break;
            case typeTurrent.SOLDIER:
                InvokeRepeating("UpdateTargetByDistance", 0f, updateTargetTime);
                break;
            case typeTurrent.CROCO:
                break;
            case typeTurrent.PIRATE:
                break;
        }
        
    }

    //update com menos atualizações
    void UpdateTargetByDistance()
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
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
        
    }

    void UpdateTargetByLifeEnemy()
    {
        
        //add todos os inimigos da area em um array
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);

        int highLife = int.MaxValue;

        GameObject enemyWithHighHealth = null;
        
        //verifica a posição dos mesmos
        foreach (GameObject enemy in enemies)
        {
            int enemylife = enemy.GetComponent<Enemy>().currentLife;

            if (enemylife < highLife)
            {
                highLife = enemylife;
                enemyWithHighHealth = enemy;
            }
        }

        //define qual o mais perto
        if (enemyWithHighHealth != null)
        {
            target = enemyWithHighHealth;
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

        //setar rotação da torre para olhar para o alvo
        partRotate.transform.LookAt(target.transform);
        Vector3 rotationMesh = partRotate.transform.eulerAngles;
        meshTurrentTrans.transform.rotation = Quaternion.Euler(0, rotationMesh.y, 0);

        if(fireCountDown <= 0f)
        {
            Firefunc(firePoint);
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    private void Firefunc(Transform whereFirePoints)
    {   
        GameObject shoot = shootPooling.GetPoolingObject();
        

        if(shoot == null)
        {
            shootPooling.CreatingObject();
            return;
        }

        //ativar o tiro colocando e rotacionando ele na posição correta
        shoot.SetActive(true);
        shoot.GetComponent<Bullet>().target = target.transform;
        shoot.GetComponent<Bullet>().currentDamage = damageInBullet;
        shoot.transform.position = whereFirePoints.position;
        shoot.transform.rotation = whereFirePoints.rotation; 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeFire);
    }
}
