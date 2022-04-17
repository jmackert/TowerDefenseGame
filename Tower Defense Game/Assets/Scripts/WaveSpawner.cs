using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public Transform enemySpawnPoint;
    //private float timeBetweenWaves = 5f;
    private int waveNumber = 1;
    public static int numEnemiesAlive = 0;
    void Update()
    {
        if(numEnemiesAlive == 0){
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave(){
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        waveNumber++;
    }
    private void SpawnEnemy(){
        numEnemiesAlive++;
        Instantiate(enemyToSpawn, enemySpawnPoint.position, enemySpawnPoint.rotation);
    }
}
