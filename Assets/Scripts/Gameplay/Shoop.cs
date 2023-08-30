using UnityEngine;

public class Shoop : MonoBehaviour
{
    BuildManager buildManager;



    private void Start()
    {
        buildManager= BuildManager.instance;
    }

    public void PurchaseTurrentOne()
    {
        Debug.Log("Torre 1 comprada");
        buildManager.SetTurrentToBuild(buildManager.standartTurrentPrefab);
    }

    public void PurchaseTurrentTwo()
    {
        /*Debug.Log("Torre 2 comprada");
        buildManager.SetTurrentToBuild(buildManager.misselTurrentPrefab);*/
    }
}
