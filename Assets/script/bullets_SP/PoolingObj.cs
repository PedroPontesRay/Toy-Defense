using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolingObj : MonoBehaviour
{
    [Header("Qual prefab sera instanciado")]
    [SerializeField] private GameObject prefab;
    
    [Header("Quantidade de objetos criados")]
    [SerializeField] private int amountToPool;

    private List<GameObject> pools = new();

    private void Start()
    {
        for(int index = 0;index< amountToPool; index++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pools.Add(obj);
        }
    }

    
    public GameObject GetPoolingObject()
    {
        for(int index = 0; index<pools.Count; index++)
        {
            if (!pools[index].activeInHierarchy)
            {
                return pools[index];
            }
        }
        return null;
    }

    public void CreatingObject()
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        pools.Add(obj);
        Debug.Log("Creating OBJ");
    }
}
