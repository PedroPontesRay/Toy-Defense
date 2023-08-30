using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region Variables
    [Header("Values To change")]
    public int maxLife;
    public float currentSpeed;
    public int valueBricks;

    public int damageInCastle;

    [Header("Objetos a referenciar")]
    [SerializeField] private Image _foreground;
    private int currentLife;
    //Mesh
    [SerializeField] private Transform meshEnemyRotation;
    public GameObject objectMesh;
    [NonSerialized]public Mesh currentMesh;
    

    //Slow time 
    private float slowSpeed = 0;
    private float slowTime = 0;
    private float currentSlowTime;
    private float currentSlowSpeed;
    private bool InSlowTime = false;

    //Waypoints 
    private GameObject[] waypoint;
    private int currentWayPointIndex = 0;
    List<GameObject> listOfItensInScene = new List<GameObject>();

    [NonSerialized]private Interface_Manager interfaceManager;
    [NonSerialized]private Spawn spawn;
    [NonSerialized]private Castle castle;

    #endregion

    #region Methods

    private void Awake()
    {
        //Add scripts nas variaveis
        interfaceManager = GameObject.FindAnyObjectByType<Interface_Manager>();
        spawn = GameObject.FindAnyObjectByType<Spawn>();
        castle = GameObject.FindAnyObjectByType<Castle>();
    }

    private void Start()
    {
        //add uma mesh ao inimigo
        objectMesh.GetComponent<MeshFilter>().sharedMesh = currentMesh;
        

        //vida atual chegar a vida maxima
        currentLife = maxLife;
        damageInCastle = 10;


        //Waypoints
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
        interfaceManager.EnemyDied(valueBricks);
        spawn.enemyInGame--;

        Destroy(gameObject);
        //usar essa função quando o pooling de inimigos estiver pronto
        //gameObject.SetActive(false);
    }

    private void ReachThePoint()
    {
        //Debug.Log("");
        spawn.enemyInGame--;

        //
        castle.Damage(10);

        //

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

    //Faz Objeto olhar para o próximo ponto
    private void UpdateLookAt()
    {
        meshEnemyRotation.LookAt(LookPoint());
    }

    private void UpdateHealthBar(int maxHealth,float currentHealth)
    {
        _foreground.fillAmount = currentHealth / maxHealth;
    }

    #endregion
}
