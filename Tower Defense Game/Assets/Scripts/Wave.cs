using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class Wave{
        //private WaveSystem waveSystem;
        public Transform enemySpawnPoint;
        //private WaveInfo waveInfo;
        //private int arrayIndex = 0;
        [SerializeField] private WaveInfo[] waveInfoArray;

                public void SpawnWave(){
                for (int i = 0; i < waveInfoArray.Length; i++)
                {
                    //SpawnEnemies();
                    Debug.Log("WAVE: " + i);
                }
                //waveSystem.waveNumber++;
                //waveSystem.player.IncreaseRoundsSurvived();
            }
                public void SpawnWaves() {
                    foreach (WaveInfo waveInfo in waveInfoArray)
                    {
                        if (waveInfo.spawnInterval >= 0)
                        {
                            waveInfo.spawnInterval -= Time.deltaTime;
                            if (waveInfo.spawnInterval <= 0)
                            {
                                //Instantiate(waveInfo.enemyToSpawn, enemySpawnPoint.position, enemySpawnPoint.rotation);
                            }
                        }
                    }
                }
                /*private void SpawnEnemies(){
                    foreach (WaveInfo waveInfo in waveInfoArray)
                    {
                        for(int i = 0; i < waveInfo.numEnemiesToSpawn; i++){
                            numEnemiesAlive++;
                            Debug.Log("NUM ENEMIES ALIVE: " + numEnemiesAlive);
                            Instantiate(waveInfo.enemyToSpawn, enemySpawnPoint.position, enemySpawnPoint.rotation);
                            //yield return new WaitForSeconds(waveInfo.spawnInterval);
                        } 
                    }
                }*/

        [System.Serializable]
        public class WaveInfo{
                //[SerializeField] public GameObject[] enemyToSpawn;
                [SerializeField] public GameObject enemyToSpawn;
                [SerializeField] public float spawnInterval;
                [SerializeField] public int numEnemiesToSpawn;
        }
    }
