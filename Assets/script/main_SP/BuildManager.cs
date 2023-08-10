using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Mais de um Build Manager");
            return;
        }
        instance = this;
    }

    public GameObject standartTurrentPrefab;
    public GameObject misselTurrentPrefab;

    private GameObject turrentToBuild;

    public GameObject GetTurrentToBuild()
    {
        return turrentToBuild;
    }

    public void SetTurrentToBuild(GameObject turrent)
    {
        turrentToBuild = turrent;
    }

    /*public GameObject SelectedObject()
    {
        return ;
    }*/
}
