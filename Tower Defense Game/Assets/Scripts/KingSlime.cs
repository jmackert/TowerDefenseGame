using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlime : Enemy
{
    public GameObject enemyToSpawn;
    private int numEnemiesToSpawn = 5;
    private Vector3 positionToSpawnEnemies;

    public KingSlime(){
        this.maxHp = 5f;
        this.unitName = "King Slime";
        this.movementSpeed = 2f;
        this.rotationSpeed = 6f;
        this.goldWorth = 50;
        this.playerDamageAmount = 1 + numEnemiesToSpawn;
    }
    protected override void Die()
    {
        StartCoroutine(SpawnEnemies());   
    }

    IEnumerator SpawnEnemies(){
        ISpawnable<int, Transform> spawnable = enemyToSpawn.GetComponent<ISpawnable<int, Transform>>();
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            WaveSpawner.numEnemiesAlive++;
            Instantiate(enemyToSpawn,transform.position,Quaternion.identity);
            spawnable.SetWaypointIndex(waypointIndex, target);
            yield return new WaitForSeconds(0.25f);
        }
        base.Die();
    }
}
