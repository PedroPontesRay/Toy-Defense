using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class Spawn : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Mesh firstTrainMesh;
    [SerializeField] private Mesh secondTrainMesh;
    [SerializeField] private Mesh thirdTrainMesh;
    [SerializeField] private GameObject prefabEnemy;

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
    private int enemyCurrentMesh;
    [NonSerialized]public int enemyInGame;

    public Action<bool> passOnWaypoint;



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
            enemyCurrentMesh =  atualNumeroInimigos + 1;

            
            atualVelocidadeInimigo = (prefabEnemy.GetComponent<Enemy>().currentSpeed + currentWave) * inimigoAumentoVelocidade;
            atualVidaInimigo = (prefabEnemy.GetComponent<Enemy>().maxLife + currentWave) * inimigoAumentoVida;
            atualValorBricks = (prefabEnemy.GetComponent<Enemy>().valueBricks + currentWave) * aumentoValorBricks;
            
            for (int i = 0;i < atualNumeroInimigos;i++)
            {
                enemyCurrentMesh--;
                EnemySpawn();
                
                yield return new WaitUntil(EnemySpawn().GetComponent<Enemy>().PassOnPoint);
            }

            //TO DO: MELHORAR A VERIFICAÇÃO
            while(HasEnemy())
            {
                yield return null;
            }

            //yield return new WaitUntil(HasEnemyFunc);
            currentWave++;

            interface_functions.UpdateWave(currentWave);
        }

    }


    public bool HasEnemy()
    {
        GameObject[] enemiesInScene= GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in enemiesInScene)
        {
            if(enemy != null)
            {
                return true;
            }
        }
        return false;
    }

    private GameObject EnemySpawn()
    {
        GameObject enemyWhoGonnaSpawn = Instantiate(prefabEnemy, spawnPosition.transform.position, Quaternion.identity);
        //Definindo variaveis da Onda Atual nos inimigos
        enemyWhoGonnaSpawn.GetComponent<Enemy>().currentMesh = TrainChoose();
        enemyWhoGonnaSpawn.GetComponent<Enemy>().currentSpeed = atualVelocidadeInimigo;
        enemyWhoGonnaSpawn.GetComponent<Enemy>().maxLife = atualVidaInimigo;
        enemyWhoGonnaSpawn.GetComponent<Enemy>().valueBricks = atualValorBricks;

        return enemyWhoGonnaSpawn;
    }

    public Mesh TrainChoose()
    {
        if (enemyCurrentMesh == atualNumeroInimigos)
        {
            return firstTrainMesh;
        }
        else if (enemyCurrentMesh == 1)
        {
            return thirdTrainMesh;
        }
        else if(enemyCurrentMesh < atualNumeroInimigos)
        {
            return secondTrainMesh;
        }
        
        return null;
    }

}
