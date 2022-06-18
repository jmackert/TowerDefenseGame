using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField]
    public List<WaveInfo> listOfWaves;

    public class WaveInfo{
        public List<GameObject> enemyType = new List<GameObject>();
        public GameObject enemy;
        public int numberToSpawn;
        public float spawnRate;
    }
}
