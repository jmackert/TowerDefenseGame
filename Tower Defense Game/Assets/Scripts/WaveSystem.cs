using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] waveArray;
    //public Transform enemySpawnPoint;
    public Player player;
    public static int numEnemiesAlive = 0;
    public int waveNumber = 1;
    //private Wave wave;

    private void Update() {
            foreach (Wave wave in waveArray)
            {
                //if (numEnemiesAlive == 0 && player.GetCurrentLives() > 0){
                    wave.SpawnWave();
                //}
            }
    }

    [System.Serializable]
    public class Wave{
        //private WaveSystem waveSystem;
        public Transform enemySpawnPoint;
        private WaveInfo waveInfo;
        [SerializeField] private WaveInfo[] waveInfoArray;

                public void SpawnWave(){
                for (int i = 0; i < waveInfoArray.Length; i++)
                {
                    SpawnEnemies();
                    Debug.Log("WAVE: " + i);
                }
                //waveSystem.waveNumber++;
                //waveSystem.player.IncreaseRoundsSurvived();
            }

                private void SpawnEnemies(){
                    foreach (WaveInfo waveI in waveInfoArray)
                    {
                        for(int i = 0; i < waveI.numEnemiesToSpawn; i++){
                            numEnemiesAlive++;
                            Debug.Log("NUM ENEMIES ALIVE: " + numEnemiesAlive);
                            Instantiate(waveI.enemyToSpawn, enemySpawnPoint.position, enemySpawnPoint.rotation);
                            //yield return new WaitForSeconds(waveInfo.spawnInterval);
                        } 
                    }
                }

        [System.Serializable]
        public class WaveInfo{
                //[SerializeField] public GameObject[] enemyToSpawn;
                [SerializeField] public GameObject enemyToSpawn;
                [SerializeField] public float spawnInterval;
                [SerializeField] public int numEnemiesToSpawn;
        }
    }
}
