using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interface_Manager : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI waveTxt;
    [SerializeField] private TextMeshProUGUI brickNumberTXT;

    public Spawn spawnSP;

    public static Interface_Manager instance;

    private void Awake()
    {
        instance = this; 
    }
    private void Start()
    {
        spawnSP = GameObject.FindAnyObjectByType<Spawn>();    
    }
    //Funcao para atualizar a wave



    public void UpdateWave(int waveCurrent)
    {
        waveTxt.text = waveCurrent.ToString();
    }

    public void EnemyDied(int valueBrick)
    {
        MainScript.brickQnt += valueBrick;
        brickNumberTXT.text = MainScript.brickQnt.ToString();
    }
}
