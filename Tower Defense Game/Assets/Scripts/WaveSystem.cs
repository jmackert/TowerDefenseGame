using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]private Wave[] waveArray;
    [SerializeField]private Transform enemySpawnPoint;
    [SerializeField]private Player player;
    [SerializeField]private int waveNumber = 0;
    public static int numEnemiesAlive = 0;
    private EnemyPool enemyPool;

    private void Start() {
        enemyPool = FindObjectOfType<EnemyPool>();
    }

    private void Update() {

            if(numEnemiesAlive == 0 && player.GetCurrentLives() > 0 && waveNumber <= waveArray.Length){
                StartCoroutine(SpawnWave());
            }
    }

    IEnumerator SpawnWave(){
        player.IncreaseRoundsSurvived();

        Wave wave = waveArray[waveNumber];
    

        for(int waveInfoArrayElement = 0; waveInfoArrayElement < wave.waveInfoArray.Length; waveInfoArrayElement++){
            for (int i = 0; i < wave.waveInfoArray[waveInfoArrayElement].numEnemiesToSpawn; i++)
            {
                SpawnEnemy(wave.waveInfoArray[waveInfoArrayElement].enemyToSpawn);
                yield return new WaitForSeconds(1f / wave.waveInfoArray[waveInfoArrayElement].spawnInterval);
            }
        }
        waveNumber++;
    }

    private void SpawnEnemy(GameObject enemy){
        GameObject enemyGO = enemyPool.GetEnemy(enemy);
        EnemyController enemySpawning = enemyGO.GetComponent<EnemyController>();
        enemySpawning.GetComponent<EnemyController>();
        enemyGO.transform.position = enemySpawnPoint.position;
        enemyGO.transform.rotation = enemySpawnPoint.rotation;
        numEnemiesAlive++;
    }

    public int GetWaveNumber(){
        return waveNumber;
    }
    public int GetNumberOfWaves(){
        return waveArray.Length;
    }
    public int GetNumberOfEnemiesAlive(){
        return numEnemiesAlive;
    }
}   
