using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interface_Manager : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI waveTxt;
    [SerializeField] private TextMeshProUGUI brickNumberTXT;
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject gameplayMenu;

    public static Interface_Manager instanceInterface;

    private void Awake()
    {
        instanceInterface = this; 
        pauseMenu.SetActive(false);


        Screen.SetResolution(480, 680, true);
    }

    //Funcao para atualizar a wave
    public void UpdateWave(int waveCurrent)
    {
        waveTxt.text = "Onda " + waveCurrent.ToString();
    }

    public void EnemyDied(int valueBrick)
    {
        MainScript.brickQnt += (valueBrick / 2);
        brickNumberTXT.text = MainScript.brickQnt.ToString();
    }

    public void PauseOn()
    {
        pauseMenu.SetActive(true);
        gameplayMenu.SetActive(false);
        Time.timeScale = 0.0f;
    }



}
