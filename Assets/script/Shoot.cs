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
        Type();

        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        Type();
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void Type()
    {
        if(typeShoot == "missel")
        {
            timeToDestroy = 3f;
            transform.LookAt(turrentInstance.target);
            transform.Translate(Vector3.forward * speedProjectile * Time.deltaTime);
            
        }
        else if(typeShoot == "bullet")
        {
            timeToDestroy = 0.5f;
            transform.Translate(Vector3.forward * speedProjectile * Time.deltaTime);
        }
    }

}
