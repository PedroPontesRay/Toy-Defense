using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using TMPro;
using System;

public class Spawn : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject firstTrainPrefab;
    [SerializeField] private GameObject secondTrainPrefab;
    [SerializeField] private GameObject thirdTrainPrefab;

    [Header("Objetos de cena")]
    [SerializeField] private GameObject spawnPosition;

    [Header("WaveTime Atributos")]
    [SerializeField] float tempoEntreWaves;
    [SerializeField] int contagemInimigos;
    [SerializeField] int aumentoPorOnda;
    [SerializeField] float inimigoAumentoVelocidade;
    [SerializeField] int inimigoAumentoVida;

    [SerializeField] private int aumentoValorBricks;

    [Header("EnemySpawn Atributos Não alterar")]
    [SerializeField] private int atualNumeroInimigos;
    [SerializeField] private float atualVelocidadeInimigo;
    [SerializeField] private int atualVidaInimigo;

    [SerializeField] private int atualValorBricks;

    private int currentWave;
    private int enemyCurrent;

    private Interface_Manager interface_functions;

    private void Start()
    {
        interface_functions = GetComponent<Interface_Manager>();



        currentWave = 1;
        interface_functions.UpdateWave(currentWave);
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            //Debug.Log("Onda: " + currentWave);

            yield return new WaitForSeconds(tempoEntreWaves);

            atualNumeroInimigos = contagemInimigos + (currentWave - 1) * aumentoPorOnda;
            enemyCurrent =  atualNumeroInimigos;

            atualVelocidadeInimigo = (TrainChoose().GetComponent<Enemy>().currentSpeed + currentWave) * inimigoAumentoVelocidade;
            atualVidaInimigo = (TrainChoose().GetComponent<Enemy>().maxLife + currentWave) * inimigoAumentoVida;
            atualValorBricks = (TrainChoose().GetComponent<Enemy>().valueBricks + currentWave) * aumentoValorBricks;
            


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

            interface_functions.UpdateWave(currentWave);
        }

    }

    private void SpawnEnemy()
    {
        GameObject enemyWhoGonnaSpawn = Instantiate(TrainChoose(), spawnPosition.transform.position, Quaternion.identity);
        //Definindo variaveis da Onda Atual nos inimigos

        enemyWhoGonnaSpawn.GetComponent<Enemy>().currentSpeed = atualVelocidadeInimigo;
        enemyWhoGonnaSpawn.GetComponent<Enemy>().maxLife = atualVidaInimigo;
        enemyWhoGonnaSpawn.GetComponent<Enemy>().valueBricks = atualValorBricks;

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
