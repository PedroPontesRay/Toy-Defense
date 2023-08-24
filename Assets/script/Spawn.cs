using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
<<<<<<< Updated upstream
=======
using TMPro;
using System;
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
=======
    [SerializeField] private int aumentoValorBricks;
>>>>>>> Stashed changes

    [Header("EnemySpawn Atributos Não alterar")]
    [SerializeField] private int atualNumeroInimigos;
    [SerializeField] private float atualVelocidadeInimigo;
    [SerializeField] private int atualVidaInimigo;
<<<<<<< Updated upstream
=======
    [SerializeField] private int atualValorBricks;
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
=======
            

>>>>>>> Stashed changes
            yield return new WaitForSeconds(tempoEntreWaves);

            atualNumeroInimigos = contagemInimigos + (currentWave - 1) * aumentoPorOnda;
            enemyCurrent =  atualNumeroInimigos;

<<<<<<< Updated upstream
            atualVelocidadeInimigo = (TrainChoose().GetComponent<enemy>().currentSpeed + currentWave) * inimigoAumentoVelocidade;
            atualVidaInimigo = (TrainChoose().GetComponent<enemy>().maxLife + currentWave) * inimigoAumentoVida;

=======
            atualVelocidadeInimigo = (TrainChoose().GetComponent<Enemy>().currentSpeed + currentWave) * inimigoAumentoVelocidade;
            atualVidaInimigo = (TrainChoose().GetComponent<Enemy>().maxLife + currentWave) * inimigoAumentoVida;
            atualValorBricks = (TrainChoose().GetComponent<Enemy>().valueBricks + currentWave) * aumentoValorBricks;
>>>>>>> Stashed changes
            


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
<<<<<<< Updated upstream
=======
            interface_functions.UpdateWave(currentWave);
>>>>>>> Stashed changes
        }

    }

    private void SpawnEnemy()
    {
        GameObject enemyWhoGonnaSpawn = Instantiate(TrainChoose(), spawnPosition.transform.position, Quaternion.identity);
        //Definindo variaveis da Onda Atual nos inimigos
<<<<<<< Updated upstream
        enemyWhoGonnaSpawn.GetComponent<enemy>().currentSpeed = atualVelocidadeInimigo;
        enemyWhoGonnaSpawn.GetComponent<enemy>().maxLife = atualVidaInimigo;
=======
        enemyWhoGonnaSpawn.GetComponent<Enemy>().currentSpeed = atualVelocidadeInimigo;
        enemyWhoGonnaSpawn.GetComponent<Enemy>().maxLife = atualVidaInimigo;
        enemyWhoGonnaSpawn.GetComponent<Enemy>().valueBricks = atualValorBricks;
>>>>>>> Stashed changes
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
