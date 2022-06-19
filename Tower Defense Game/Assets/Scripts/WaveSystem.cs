using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] waveArray;
    public Transform enemySpawnPoint;
    public Player player;
    public static int numEnemiesAlive = 0;

    /*private void Update() {
        foreach (WaveInfo waveInfo in waveInfoArray)
        {
            waveInfo.SpawnWave();
        }
    }*/
    [System.Serializable]
    private class Wave{
        [SerializeField] private WaveInfo[] waveInfoArray;

        [System.Serializable]
        private class WaveInfo{

            [SerializeField] private EnemyInfo[] enemyInfoArray;
            [System.Serializable]
            private class EnemyInfo{

                WaveSystem waveSystem;
                //[SerializeField] private GameObject[] enemyArray;
                [SerializeField] private GameObject enemy;
                [SerializeField] private float timer;
                [SerializeField] private int numEnemies;

                public IEnumerator SpawnWave(){
                for (int i = 0; i < numEnemies; i++)
                {
                    //SpawnEnemies();
                    //Debug.Log("WAVE: " + i);
                    yield return new WaitForSeconds(timer);
                }
                //waveNumber++;
                //player.IncreaseRoundsSurvived();
            }

                /*private void SpawnEnemies(){
                    foreach (GameObject enemyToSpawn in enemyArray){
                        numEnemiesAlive++;
                        Instantiate(enemyToSpawn, waveSystem.enemySpawnPoint.position, waveSystem.enemySpawnPoint.rotation);
                    } 
                }*/
            }
        }
    }
}
