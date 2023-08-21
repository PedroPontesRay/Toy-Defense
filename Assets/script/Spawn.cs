using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

using System;

public class Spawn : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject firstTrainPrefab, secondTrainPrefab, thirdTrainPrefab;


    [Header("Objetos de cena")]
    [SerializeField] private GameObject spawnPosition;

    [Header("WaveTime Atributos")]
    [SerializeField] float tempoEntreWaves;
    [SerializeField] int contagemInimigos;
    [SerializeField] int aumentoPorOnda;
    [SerializeField] float inimigoAumentoVelocidade;
    [SerializeField] int inimigoAumentoVida;
    [SerializeField] private float aumentoValorBricks;

    [Header("EnemySpawn Atributos Não alterar")]
    [SerializeField] private int atualNumeroInimigos;
    [SerializeField] private float atualVelocidadeInimigo;
    [SerializeField] private int atualVidaInimigo;
    [SerializeField] private float atualValorBricks;


    private int currentWave;
    private int enemyCurrent;

    private void Start()
    {
        currentWave = 1;
        StartCoroutine(SpawnEnemies());
    }


    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            //Debug.Log("Onda: " + currentWave);
            waveTxt.text = currentWave.ToString();
            yield return new WaitForSeconds(tempoEntreWaves);

            atualNumeroInimigos = contagemInimigos + (currentWave - 1) * aumentoPorOnda;
            enemyCurrent =  atualNumeroInimigos;

            atualVelocidadeInimigo = (TrainChoose().GetComponent<enemy>().currentSpeed + currentWave) * inimigoAumentoVelocidade;
            atualVidaInimigo = (TrainChoose().GetComponent<enemy>().maxLife + currentWave) * inimigoAumentoVida;
            atualValorBricks = (TrainChoose().GetComponent<enemy>().valueBricks + currentWave) * aumentoValorBricks;
            


            for (int i = 0;i < atualNumeroInimigos;i++)
            {
                yield return new WaitForSeconds(0.5f);        
                SpawnEnemy();
            }

            while(HasEnemys())
            {
                //Pensar numa solução melhor de verificação de inimigos na cena
                yield return null;
            }


            currentWave++;
            waveTxt.text = currentWave.ToString();
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyWhoGonnaSpawn = Instantiate(TrainChoose(), spawnPosition.transform.position, Quaternion.identity);
        //Definindo variaveis da Onda Atual nos inimigos
        enemyWhoGonnaSpawn.GetComponent<enemy>().currentSpeed = atualVelocidadeInimigo;
        enemyWhoGonnaSpawn.GetComponent<enemy>().maxLife = atualVidaInimigo;
        enemyWhoGonnaSpawn.GetComponent<enemy>().valueBricks = atualValorBricks;
        enemyCurrent--;
    }


    private bool HasEnemys()
    {
        GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemiesInScene)
        {
            if(enemy != null)
            {
                return true;
            }
        }
        return false;
    }

    
    public GameObject TrainChoose()
    {
        if (enemyCurrent == atualNumeroInimigos)
        {
            return firstTrainPrefab;
        }
        else if (enemyCurrent == 1)
        {
            return thirdTrainPrefab;
        }
        else if(enemyCurrent < atualNumeroInimigos)
        {
            return secondTrainPrefab;
        }
        return null;
    }



}
