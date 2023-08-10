using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speedProjectile;
    [SerializeField] private float timeToDestroy;

    //Turrent turrentInstance;
    public Rigidbody m_rigidbody;

    private void OnEnable()
    {
        MoveProject();
        Invoke("Deactivate",timeToDestroy);
    }

    void Start()
    {
        //turrentInstance = Turrent.TurrentInstance;
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        MoveProject();
    }

    private void MoveProject()
    {
        //transform.position += transform.forward * (Time.deltaTime * speedProjectile);
        m_rigidbody.velocity = transform.forward* speedProjectile;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}

