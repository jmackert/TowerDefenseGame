using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    //[SerializeField] private Wave[] waveArray;
    public Wave[] waveArray;
    public Transform enemySpawnPoint;
    public Player player;
    public static int numEnemiesAlive = 0;
    public int waveNumber = 0;

    private void Update() {

            if(numEnemiesAlive == 0 && player.GetCurrentLives() > 0){
                StartCoroutine(SpawnWave());
            }

    }

    IEnumerator SpawnWave(){
        player.IncreaseRoundsSurvived();

        Wave wave = waveArray[waveNumber];
        
        for(int waveInfoArrayElement = 0; waveInfoArrayElement < waveArray.Length; waveInfoArrayElement++){
            //SpawnWave(wave.enemyToSpawn);
            //spawn enemy(enemytospawn)
            //Debug.Log(wave.waveInfoArray[waveInfoArrayElement].enemyToSpawn);
            SpawnEnemy(wave.waveInfoArray[waveInfoArrayElement].enemyToSpawn);
            yield return new WaitForSeconds(1f / wave.waveInfoArray[waveInfoArrayElement].spawnInterval);
        }
        waveNumber++;
    }

    private void SpawnEnemy(GameObject enemy){
        Instantiate(enemy, enemySpawnPoint.position, enemySpawnPoint.rotation);
    }


}   
