using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemyPrefab,spawnPosition;

    [Header("WaveTime Atributos")]
    [SerializeField] float tempoEntreWaves = 5f;
    [SerializeField] int contagemInimigos = 5;
    [SerializeField] int aumentoPorOnda = 2;
    [SerializeField] float inimigoAumentoVelocidade = 0.1f;
    [SerializeField] int inimigoAumentoVida = 5;

    [Header("EnemySpawn Atributos")]
    private int atualOnda = 1;
    [SerializeField] private int atualNumeroInimigos;
    [SerializeField] private float atualVelocidadeInimigo;
    [SerializeField] private int atualVidaInimigo;


    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }


    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Debug.Log("Onda: " + atualOnda);
            yield return new WaitForSeconds(tempoEntreWaves);
            atualNumeroInimigos = contagemInimigos + (atualOnda - 1) * aumentoPorOnda;
            atualVelocidadeInimigo = enemyPrefab.GetComponent<enemy>().speed + (atualOnda - 1) * inimigoAumentoVelocidade;
            atualVidaInimigo = enemyPrefab.GetComponent<enemy>().maxLive + (atualOnda - 1) * inimigoAumentoVida;


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
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition.transform.position, Quaternion.identity);
        enemy.GetComponent<enemy>().speed = atualVelocidadeInimigo;
        enemy.GetComponent<enemy>().maxLive = atualVidaInimigo;
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
}
