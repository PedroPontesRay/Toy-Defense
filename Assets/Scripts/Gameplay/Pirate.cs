using System;
using Unity.VisualScripting;
using UnityEngine;

public class Pirate : MonoBehaviour
{

    [SerializeField] private float rangeFire;
    private float damage;

    public event Action<Enemy> OnContinuousAttack = delegate { };

    private void Start()
    {
        InvokeRepeating("CheckForEnemies", 0f, 1f);
    }


    private void CheckForEnemies()
    {
        
        Collider[] enemyGonDamage = Physics.OverlapSphere(transform.position, rangeFire);
        foreach(Collider collider in enemyGonDamage)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    EnemyTakingDamage(enemy);
                }
            }
        }
    }




    private void EnemyTakingDamage(Enemy enemy)
    {
        OnContinuousAttack.Invoke(enemy);
    }
    public void StopEnemyTakingDamage()
    {
        OnContinuousAttack.Invoke(null);
    }



    


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeFire);
    }
}
