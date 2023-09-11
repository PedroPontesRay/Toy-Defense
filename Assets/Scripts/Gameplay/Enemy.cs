using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    [SerializeField] public float currentLife;
    //Mesh
    [SerializeField] private Transform meshEnemyRotation;
    public GameObject objectMesh;
    [NonSerialized] public Mesh currentMesh;
    

    //Slow time 

    private float currentSlowTime;
    private float currentSlowSpeed;
    private bool InSlowTime = false;

    //Waypoints 
    private int currentWayPointIndex = 0;
    GameObject[] wayPoints;

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
        wayPoints = GameObject.FindGameObjectsWithTag("Waypoint");

        List<GameObject> listOfItensInSceneOrdered = wayPoints.OrderBy(waypoint => waypoint.name).ToList();

        wayPoints = listOfItensInSceneOrdered.ToArray();

        /*
        // Debug para printar que a ordem esta correta
        for (int i = 0; i < wayPoints.Length; i++)
        {
            Debug.Log("Item " + i + ": " + wayPoints[i].name);
        }*/


        StartCoroutine(MoveToPoint());
        meshEnemyRotation.transform.LookAt(LookPoint());

    }

    private IEnumerator MoveToPoint()
    {
        while (currentWayPointIndex < wayPoints.Length)
        {
            Vector3 targetPosition = wayPoints[currentWayPointIndex].transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            while (distanceToTarget > 0.1f)
            {
                float speed = currentSpeed - currentSlowSpeed;
                if(speed < 0)
                {
                    speed = 0.1f;
                }
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                distanceToTarget = Vector3.Distance(transform.position, targetPosition);


                yield return null;
            }
            currentWayPointIndex++;

     
            meshEnemyRotation.LookAt(LookPoint());//Faz Objeto olhar para o próximo ponto

        }
        ReachThePoint();
    }
    public bool PassOnPoint()
    {
        if (currentWayPointIndex >= 1)
        {
            return true;
        }
        return false;
    }
    public void TakeDamage(float damageInBulllet)
    {
        currentLife -= damageInBulllet;
        UpdateHealthBar(maxLife, currentLife);


        if (currentLife <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        //ADD points
        interfaceManager.EnemyDied(valueBricks);


        gameObject.SetActive(false);
    }
    public void SlowStateFunc(float time, float speed)
    {
        if (InSlowTime == false)
        {
            currentSlowSpeed = speed;
            currentSlowTime = time;
            StartCoroutine(SlowState());
        }
    }
    private IEnumerator SlowState()
    {
        //estado quando leva dano
        InSlowTime = true;
        
        while (currentSlowTime > 0)
        {
            currentSlowTime -= Time.deltaTime;
            yield return null;
        }

        //estado quando acaba
        currentSlowSpeed = 0;
        InSlowTime = false;
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
    private Transform LookPoint()
    {
        if (wayPoints.Length != currentWayPointIndex)
        {
            return wayPoints[currentWayPointIndex].transform;
        }
        return null;
    }
    private void UpdateHealthBar(int maxHealth,float currentHealth)
    {
        _foreground.fillAmount = currentHealth / maxHealth;
    }
    #endregion
}
