using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public Wave[] waveArray;
    public Transform enemySpawnPoint;
    public Player player;
    public static int numEnemiesAlive = 0;
    private int waveNumber = 0;
    protected EnemyPool enemyPool;

    private void Start() {
        enemyPool = FindObjectOfType<EnemyPool>();
    }

    private void Update() {

            if(numEnemiesAlive == 0 && player.GetCurrentLives() > 0){
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
                //Debug.Log(numEnemiesAlive);
                yield return new WaitForSeconds(1f / wave.waveInfoArray[waveInfoArrayElement].spawnInterval);
            }
        }
        waveNumber++;
    }

    private void SpawnEnemy(GameObject enemy){
        GameObject enemyGO = enemyPool.GetEnemy(enemy);
        Enemy enemySpawning = enemyGO.GetComponent<Enemy>();
        enemySpawning.GetComponent<Enemy>();
        enemyGO.transform.position = enemySpawnPoint.position;
        enemyGO.transform.rotation = enemySpawnPoint.rotation;
        numEnemiesAlive++;
    }
}   
