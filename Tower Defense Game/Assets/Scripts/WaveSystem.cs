using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private WaveInfo[] waveInfoArray;
    public Transform enemySpawnPoint;
    public Player player;
    public static int numEnemiesAlive = 0;

    [System.Serializable]
    private class WaveInfo{
        WaveSystem waveSystem;
        [SerializeField] private GameObject[] enemyArray;
        //public Transform enemySpawnPoint;

        public void SpawnEnemies(){
            foreach (GameObject enemyToSpawn in enemyArray){
                numEnemiesAlive++;
                Instantiate(enemyToSpawn, waveSystem.enemySpawnPoint.position, waveSystem.enemySpawnPoint.rotation);
            } 
        }
    }
}
