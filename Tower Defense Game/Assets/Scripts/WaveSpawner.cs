using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    //public GameObject enemyToSpawn;
    public Transform enemySpawnPoint;
    private int waveNumber = 1;
    public static int numEnemiesAlive = 0;
    public Player player;
    public Wave[] waves;
    private int waveIndex = 0;
    
    void Update()
    {
        if(numEnemiesAlive == 0 && player.GetCurrentLives() > 0){
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave(){
        /*for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            Debug.Log("WAVE: " + i);
            yield return new WaitForSeconds(0.5f);
        }
        waveNumber++;
        player.IncreaseRoundsSurvived();*/
        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.numberToSpawn; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }
        waveIndex++;
        waveNumber++;
        player.IncreaseRoundsSurvived();
    }
    private void SpawnEnemy(GameObject enemyToSpawn){
        numEnemiesAlive++;
        Instantiate(enemyToSpawn, enemySpawnPoint.position, enemySpawnPoint.rotation);
    }
}
