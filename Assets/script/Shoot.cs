using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float speedProjectile = 20f;
    [SerializeField] private string typeShoot;

    private float timeToDestroy;
    Turrent turrentInstance;


    void Start()
    {
        turrentInstance = Turrent.TurrentInstance;

        Destroy(gameObject, timeToDestroy);
    }

    public void Update()
    {
        timeToDestroy = 0.5f;
        transform.Translate(Vector3.forward * speedProjectile * Time.deltaTime);
    }
}
