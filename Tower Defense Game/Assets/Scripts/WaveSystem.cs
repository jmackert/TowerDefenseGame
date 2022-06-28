using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] waveArray;
    public Wave wave;
    //public Transform enemySpawnPoint;
    public Player player;
    public static int numEnemiesAlive = 0;
    public int waveNumber = 1;
    private int waveIndex = 0;

    private void Update() {
            /*foreach (Wave wave in waveArray)
            {
                //if (numEnemiesAlive == 0 && player.GetCurrentLives() > 0){
                    //wave.SpawnWave();
                    //wave.Update();
                //}
            }*/

            Wave wave = waveArray[waveIndex];
            for (int i = 0; i < waveArray.Length; i++)
            {
                wave.SpawnWaves();
            }
            waveIndex++;
    }

    // [System.Serializable]
    // public class Wave{
    //     //private WaveSystem waveSystem;
    //     public Transform enemySpawnPoint;
    //     //private WaveInfo waveInfo;
    //     //private int arrayIndex = 0;
    //     [SerializeField] private WaveInfo[] waveInfoArray;

    //             public void SpawnWave(){
    //             for (int i = 0; i < waveInfoArray.Length; i++)
    //             {
    //                 //SpawnEnemies();
    //                 Debug.Log("WAVE: " + i);
    //             }
    //             //waveSystem.waveNumber++;
    //             //waveSystem.player.IncreaseRoundsSurvived();
    //         }
    //             public void Update() {
    //                 foreach (WaveInfo waveInfo in waveInfoArray)
    //                 {
    //                     if (waveInfo.spawnInterval >= 0)
    //                     {
    //                         waveInfo.spawnInterval -= Time.deltaTime;
    //                         if (waveInfo.spawnInterval <= 0)
    //                         {
    //                             Instantiate(waveInfo.enemyToSpawn, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //                         }
    //                     }
    //                 }
    //             }
    //             /*private void SpawnEnemies(){
    //                 foreach (WaveInfo waveInfo in waveInfoArray)
    //                 {
    //                     for(int i = 0; i < waveInfo.numEnemiesToSpawn; i++){
    //                         numEnemiesAlive++;
    //                         Debug.Log("NUM ENEMIES ALIVE: " + numEnemiesAlive);
    //                         Instantiate(waveInfo.enemyToSpawn, enemySpawnPoint.position, enemySpawnPoint.rotation);
    //                         //yield return new WaitForSeconds(waveInfo.spawnInterval);
    //                     } 
    //                 }
    //             }*/

    //     [System.Serializable]
    //     public class WaveInfo{
    //             //[SerializeField] public GameObject[] enemyToSpawn;
    //             [SerializeField] public GameObject enemyToSpawn;
    //             [SerializeField] public float spawnInterval;
    //             [SerializeField] public int numEnemiesToSpawn;
    //     }
    // }
}
