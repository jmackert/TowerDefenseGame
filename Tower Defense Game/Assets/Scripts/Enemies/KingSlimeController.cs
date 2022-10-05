using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlimeController : EnemyController
{
    [SerializeField]private GameObject enemyToSpawn;
    private int numEnemiesToSpawn = 5;
    private int i = 0;
    private Vector3 positionToSpawnEnemies;
    public MeshRenderer meshRend;

    public KingSlimeController(){
        this.maxHp = 25f;
        this.unitName = "King Slime";
        this.movementSpeed = 2f;
        this.rotationSpeed = 6f;
        this.goldWorth = 50;
        this.playerDamageAmount = 1 + numEnemiesToSpawn;
    }
    protected override void Die()
    {
        if(enemyToSpawn != null){
            StartCoroutine(SpawnEnemies());   
        }
        else{
            base.Die();
        }
    }

    IEnumerator SpawnEnemies(){
        ISpawnable<int, Transform> spawnable = enemyToSpawn.GetComponent<ISpawnable<int, Transform>>();
        movementSpeed = 0;
        meshRend.enabled = false;
        while (i < numEnemiesToSpawn)
        {
            Instantiate(enemyToSpawn,transform.position,Quaternion.identity);
            spawnable.SetWaypointIndex(waypointIndex, targetWaypoint);
            WaveSpawner.numEnemiesAlive++;
            Debug.Log("TEST: " + i);
            i++;
            yield return new WaitForSeconds(0.15f);
        }
        base.Die();
    }
}