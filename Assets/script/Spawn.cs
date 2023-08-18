using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("Prefabs e objetos de cena")]
    [SerializeField] private GameObject firstTrainPrefab;
    [SerializeField] private GameObject secondTrainPrefab;
    [SerializeField] private GameObject thirdTrainPrefab;
    [SerializeField] private GameObject spawnPosition;

    [Header("WaveTime Atributos")]
    [SerializeField] float tempoEntreWaves = 5f;
    [SerializeField] int contagemInimigos = 5;
    [SerializeField] int aumentoPorOnda = 2;
    [SerializeField] float inimigoAumentoVelocidade = 0.1f;
    [SerializeField] int inimigoAumentoVida = 5;

    [Header("EnemySpawn Atributos Não alterar")]
    [SerializeField] private int atualNumeroInimigos;
    [SerializeField] private float atualVelocidadeInimigo;
    [SerializeField] private int atualVidaInimigo;

    private int atualOnda = 1;
    private int enemyCurrent;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }


    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            //Debug.Log("Onda: " + atualOnda);
            yield return new WaitForSeconds(tempoEntreWaves);


            atualNumeroInimigos = contagemInimigos + (atualOnda - 1) * aumentoPorOnda;
            enemyCurrent =  atualNumeroInimigos;
            atualVelocidadeInimigo = TrainChoose().GetComponent<enemy>().speed + (atualOnda - 1) * inimigoAumentoVelocidade;
            atualVidaInimigo = TrainChoose().GetComponent<enemy>().maxLive + (atualOnda - 1) * inimigoAumentoVida;

            


            for (int i = 0;i < atualNumeroInimigos;i++)
            {
                yield return new WaitForSeconds(0.5f);
                //Debug.Log("Spawnado");
                
                SpawnEnemy();
            }

            while(HaInimigos())
            {
                yield return null;
            }

            atualOnda++;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(TrainChoose(), spawnPosition.transform.position, Quaternion.identity);
        enemy.GetComponent<enemy>().speed = atualVelocidadeInimigo;
        enemy.GetComponent<enemy>().maxLive = atualVidaInimigo;
        enemyCurrent--;
        //Debug.Log(enemyCurrent);
    }


    private bool HaInimigos()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if(enemy != null)
            {
                return true;
            }
        }
        return false;
    }


    private GameObject TrainChoose()
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
