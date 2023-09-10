using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shoop : MonoBehaviour
{
    //1Gumbal,2missel,3pirate,4soldier
    public GameObject[] turrentsPrefab;
    public Sprite[] turrentsSprites;

    //imagens est�o na ordem junto com os bot�es
    public Sloot[] SlootsTurrents;


    public int[] towerInfo;

    public void Awake()
    {
      
        for(int i = 0;i< SlootsTurrents.Length;i++)
        {
            if (SlootsTurrents[i] != null)
            {
                SlootsTurrents[i].SetSlootSet(turrentsSprites[towerInfo[i]], turrentsPrefab[towerInfo[i]]);
            }
        }       

    }
}