using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Variables
    public static BuildManager instance;

    [NonSerialized]public GameObject turrentToBuild;
    #endregion

    #region Methods
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Mais de um Build Manager");
            return;
        }
        instance = this;
    }
    public GameObject GetTurrentToBuild()
    {
        return turrentToBuild;
    }

    public void SetTurrentToBuild(GameObject currentSelectTurrent)
    {
        turrentToBuild = currentSelectTurrent;
    }
    #endregion
}
