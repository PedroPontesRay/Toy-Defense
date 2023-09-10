using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sloot : MonoBehaviour
{
    BuildManager buildManager;
    public Image imagePlaceHolder;

    private GameObject currentTurrent;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SetSlootSet(Sprite towerSprite, GameObject towerPrefab)
    {
        imagePlaceHolder.sprite = towerSprite;
        currentTurrent = towerPrefab;
    }

    public void PurchaseTurrent()
    {
        buildManager.SetTurrentToBuild(currentTurrent);
    }
}
