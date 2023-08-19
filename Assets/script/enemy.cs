using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    private GameObject[] waypoint;

    private int currentWayPointIndex = 0;


    [Header("Life")]
    public int maxLife;
    public float currentSpeed;
    [SerializeField] private int currentLife;


    List<GameObject> listOfItensInScene = new List<GameObject>();

    //Health bar
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Image _healthBarSprite;
    private GameObject _cam;
    



    private void Start()
    {
        //vida atual chegar a vida maxima
        currentLife = maxLife;

        //Definição da camera e barra de vida
        _cam = GameObject.Find("Main Camera");



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
        while (currentWayPointIndex < waypoint.Length)
        {
            Vector3 targetPosition = waypoint[currentWayPointIndex].transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            while (distanceToTarget > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

                distanceToTarget = Vector3.Distance(transform.position, targetPosition);
                yield return null;
            }
            currentWayPointIndex++;

        }

        UpdateLookAt();

        Die();
    }

    public void TakeDamage(int damageInBulllet)
    {
        currentLife -= damageInBulllet;
        UpdateHealthBar(maxLife,currentLife);

        if (currentLife <= 0)
        {
            Die();
        }
    }

    private void Die()
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
        transform.LookAt(LookPoint());

        //Faz canvas olhar para camera
        _canvas.transform.LookAt(_cam.transform);
    }

    


    private void UpdateHealthBar(int maxHealth,float currentHealth)
    {
        _healthBarSprite.fillAmount = currentHealth / maxHealth;
    }
}
