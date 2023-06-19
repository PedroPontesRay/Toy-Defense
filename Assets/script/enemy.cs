using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private GameObject[] waypoint;
    private int currentWayPointIndex = 0;
    [SerializeField]
    public float speed;

    [Header("Life")]
    public int maxLive;
    private int currentLife;

    private void Start()
    {
        currentLife = maxLive;


        List<GameObject> listOfItensInScene = new List<GameObject>();

        GameObject[] taggedItems = GameObject.FindGameObjectsWithTag("Waypoint");
        listOfItensInScene.AddRange(taggedItems);

        waypoint = listOfItensInScene.ToArray();

        /*
        // Debug para printar que a ordem esta correta
        for (int i = 0; i < waypoint.Length; i++)
        {
            Debug.Log("Item " + i + ": " + waypoint[i].name);
        }*/



        StartCoroutine(MoveToPoint());
    }

    
    private IEnumerator MoveToPoint()
    {
        while(currentWayPointIndex < waypoint.Length)
        {
            Vector3 targetPosition = waypoint[currentWayPointIndex].transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            while(distanceToTarget > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position,targetPosition,speed * Time.deltaTime);

                distanceToTarget = Vector3.Distance(transform.position, targetPosition);
                yield return null;
            }
            currentWayPointIndex++; 

        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shoot"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentLife -= MainScript.damageAmount;
        if(currentLife <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
