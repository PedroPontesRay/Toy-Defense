using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private float speedProjectile = 20f;
    private int damageAmoutInbullet;
    // Start is called before the first frame update
    void Start()
    {
        damageAmoutInbullet = MainScript.damageAmount;       
        
        Destroy(gameObject,0.5f);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speedProjectile * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
