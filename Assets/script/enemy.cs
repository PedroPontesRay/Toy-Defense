using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class enemy : MonoBehaviour
{
    private GameObject[] waypoint;
    private int currentWayPointIndex = 0;
    [SerializeField]
    public float speed;

    [Header("Life")]
    public int maxLive;
    private int currentLife;
     List<GameObject> listOfItensInScene = new List<GameObject>();

    private void Start()
    {
        currentLife = maxLive;



        GameObject[] taggedItems = GameObject.FindGameObjectsWithTag("Waypoint");
        listOfItensInScene.AddRange(taggedItems);

        List<GameObject> listOfItensInSceneOrdered = OrderItemsByName(listOfItensInScene);

        waypoint = listOfItensInSceneOrdered.ToArray();

        
        // Debug para printar que a ordem esta correta
        /*for (int i = 0; i < waypoint.Length; i++)
        {
            Debug.Log("Item " + i + ": " + waypoint[i].name);
        }
        
        foreach(GameObject item in waypoint)
        {
            Debug.Log(item.name);
        }*/



        StartCoroutine(MoveToPoint());
        transform.LookAt(LookPoint());
    }

    private List<GameObject> OrderItemsByName(List<GameObject> unsorted)
    {
        List<GameObject> ordered = unsorted.OrderBy(item => item.name).ToList();
        return ordered;
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
            transform.LookAt(LookPoint());

        }

        Destroy(gameObject);
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StandartShoot"))
        {
            
        }
        else if(other.CompareTag("MisselShoot"))
        {
            TakeDamage(MainScript.damageAmountMissel);
            Debug.Log("Missel Acertado");
        }
    }*/

    public void TakeDamage(int damageInBulllet)
    {
        currentLife -= damageInBulllet;
        //Debug.Log("Vida inimigo: " + currentLife);
        if(currentLife <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }

    private Transform LookPoint()
    {
        if(waypoint.Length != currentWayPointIndex)
        {
            return waypoint[currentWayPointIndex].transform;
        }
        return null;
    }
}
