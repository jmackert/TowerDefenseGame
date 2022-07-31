using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class Wave{
        [SerializeField] public WaveInfo[] waveInfoArray;
        

        [System.Serializable]
        public class WaveInfo{
                [SerializeField] public GameObject enemyToSpawn;
                [SerializeField] public float spawnInterval;
                [SerializeField] public int numEnemiesToSpawn;
        }

        public float getSpawnInterval(int index){
            return waveInfoArray[index].spawnInterval;
        }
    }