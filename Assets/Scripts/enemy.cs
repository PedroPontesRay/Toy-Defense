using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private GameObject[] waypoint;

    private int currentWayPointIndex = 0;



    [Header("Values To change")]
    public int maxLife;
    public float currentSpeed;
    public int valueBricks;

    [Header("Objects to reference")]

    //Health bar
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Image _healthBarSprite;
    [SerializeField] private Transform meshEnemyRotation;
    private int currentLife;

    List<GameObject> listOfItensInScene = new List<GameObject>();

    //public float currentSpeed;
    private bool takenDamage;
    private float slowSpeed = 0.6f;

    private float timeInslow;


    public Interface_Manager interfaceManager;

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
                //verificação para deixa o inimigo lento após tomar dano
                if(takenDamage == true && timeInslow > 0) 
                {
                    Debug.Log("Reproduzinho Slow");
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, slowSpeed * Time.deltaTime);

                    timeInslow -= Time.deltaTime; 
                    //função para voltar a velocidade para normal depois de um tempo sem tomar dano
                    
                }
                else
                {
                    Debug.Log("Reproduzinho Speed " + timeInslow + takenDamage);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);
                    timeInslow = 1.0f;

                }


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
        takenDamage = true;
        currentLife -= damageInBulllet;
        UpdateHealthBar(maxLife, currentLife);

        if (currentLife <= 0)
        {
            Die();
        }
        Invoke("ReturnNormalSpeed", 1.0f);
    }

    private void ReturnNormalSpeed()
    {
        if (!takenDamage)
            return;
        timeInslow = 1.0f;
        takenDamage= false;
    }

    private void Die()
    {
        //ADD points
        Interface_Manager.instance.EnemyDied(valueBricks);

        Destroy(gameObject);


        //usar essa função quando o pooling de inimigos estiver pronto
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

    private void UpdateLookAt()
    {
        //Faz Objeto olhar para o próximo ponto
        meshEnemyRotation.LookAt(LookPoint());
    }

    private void UpdateHealthBar(int maxHealth,float currentHealth)
    {
        _healthBarSprite.fillAmount = currentHealth / maxHealth;
    }
}
