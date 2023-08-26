using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{



    [Header("Values To change")]
    public int maxLife;
    public float currentSpeed;
    public int valueBricks;

    [Header("Objects to reference")]
    //Health bar
    [SerializeField] private Image _foreground;
    [SerializeField] private Transform meshEnemyRotation;
    private int currentLife;

    //Slow time 
    [SerializeField] private float slowSpeed;
    [SerializeField] private float slowTime;
    private float currentSlowTime;
    private float currentSlowSpeed;
    private bool InSlowTime = false;

    //Waypoints 
    private GameObject[] waypoint;
    private int currentWayPointIndex = 0;
    List<GameObject> listOfItensInScene = new List<GameObject>();

    private Interface_Manager interfaceManager;

    private void Start()
    {
        //vida atual chegar a vida maxima
        currentLife = maxLife;

        interfaceManager = GameObject.FindAnyObjectByType<Interface_Manager>();


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
        meshEnemyRotation.transform.LookAt(LookPoint());
        
    }

    private List<GameObject> OrderItemsByName(List<GameObject> unsorted)
    {
        List<GameObject> ordered = unsorted.OrderBy(item => item.name).ToList();
        return ordered;
    }

    private IEnumerator MoveToPoint()
    {
        while (currentWayPointIndex < waypoint.Length)
        {
            Vector3 targetPosition = waypoint[currentWayPointIndex].transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            while (distanceToTarget > 0.1f)
            {
                float speed = currentSpeed - currentSlowSpeed;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                distanceToTarget = Vector3.Distance(transform.position, targetPosition);


                yield return null;
            }
            currentWayPointIndex++;
            UpdateLookAt();

        }
        ReachThePoint();
    }

    public void TakeDamage(int damageInBulllet)
    {
        currentLife -= damageInBulllet;
        UpdateHealthBar(maxLife, currentLife);

        if (currentLife <= 0)
        {
            Die();
            return;
        }

        if(InSlowTime == false)
        {
            StartCoroutine(SlowState());
        }
    }

    private IEnumerator SlowState()
    {
        //estado quando leva dano
        InSlowTime = true;
        currentSlowSpeed = slowSpeed;
        currentSlowTime = slowTime;

        while (currentSlowTime > 0)
        {
            currentSlowTime -= Time.deltaTime;
            yield return null;
        }

        //estado quando acaba
        currentSlowSpeed = 0;
        InSlowTime = false;
    }


    private void Die()
    {
        //ADD points
        Interface_Manager.instance.EnemyDied(valueBricks);

        Destroy(gameObject);
        //usar essa fun��o quando o pooling de inimigos estiver pronto
        //gameObject.SetActive(false);
    }

    private void ReachThePoint()
    {
        Destroy(gameObject);
    }

    //retorna o transform que o inimigo tem que olhar
    private Transform LookPoint()
    {
        if (waypoint.Length != currentWayPointIndex)
        {
            return waypoint[currentWayPointIndex].transform;
        }
        return null;
    }

    //Faz Objeto olhar para o pr�ximo ponto
    private void UpdateLookAt()
    {
        meshEnemyRotation.LookAt(LookPoint());
    }

    private void UpdateHealthBar(int maxHealth,float currentHealth)
    {
        _foreground.fillAmount = currentHealth / maxHealth;
    }
}
