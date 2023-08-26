using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money_Bricks : MonoBehaviour
{
    public int bricksPlayer;

    public void DropBricks(int enemyValueDrop)
    {
        bricksPlayer =+ enemyValueDrop;
    } 
}
