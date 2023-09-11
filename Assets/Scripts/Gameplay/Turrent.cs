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
    }

    private PoolingObj shootPooling;
    private GameObject target;
    private string enemytag = "Enemy";

    [Header("Fire Atributes")]
    [SerializeField] private typeTurrent currentTypeTurrent;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float rangeFire;
    [SerializeField] private float fireRate;
    [SerializeField] private float updateTargetTime;
    private float fireCountDown = 0f;
    public float damageInBullet;

    [Header("Rotate-Mesh")]
    public GameObject partRotate;
    public Transform meshTurrentTrans;


    void OnEnable()
    {
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
        }
        if (GetComponent<PoolingObj>() == null)
            return;
        shootPooling = GetComponent<PoolingObj>();
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
        if(nearestEnemy != null && shortest <= rangeFire)
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

        float highLife = float.MaxValue;

        GameObject enemyWithHighHealth = null;
        float enemyDistance = 0;

        //verifica a posição dos mesmos
        foreach (GameObject enemy in enemies)
        {
            float enemylife = enemy.GetComponent<Enemy>().currentLife;

            if (enemylife < highLife)
            {
                highLife = enemylife;
                enemyWithHighHealth = enemy;
                enemyDistance = Vector3.Distance(transform.position, enemyWithHighHealth.transform.position);
            }
        }

        

        //define qual o mais perto
        if (enemyWithHighHealth != null && enemyDistance <= rangeFire)
        {
            target = enemyWithHighHealth;
        }
        else
        {
            target = null;
        }
    }
    void DamageByPirate()
    {
        // Encontre todos os inimigos no alcance da torre
        Collider[] colliders = Physics.OverlapSphere(transform.position, rangeFire);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // Aplique dano ao inimigo
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageInBullet);
                }
            }
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
            fireCountDown = 1f / fireRate;

            Firefunc(firePoint);

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
        Gizmos.DrawWireSphere(transform.position, rangeFire);
    }
}
