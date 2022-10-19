using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlimeController : EnemyController
{
    [SerializeField]private GameObject enemyToSpawn;
    [SerializeField]private int numEnemiesToSpawn = 5;
    [SerializeField]private Vector3 center;
    private float spawnRadius = 1;

    public KingSlimeController(){
        this.maxHp = 25f;
        this.unitName = "King Slime";
        this.movementSpeed = 2f;
        this.rotationSpeed = 6f;
        this.goldWorth = 50;
        this.playerDamageAmount = 1 + numEnemiesToSpawn;
    }

    private void Update() {
        Move();
        center = transform.position;
        if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.2f){
            CalculateTargetWaypoint();
            CalculatePreviousWaypoint();
        }
        GetDistanceTraveled();
    }

    protected override void Die()
    {
        SpawnMinions();
        base.Die();
    }

    private void SpawnMinions(){
        ISpawnable<int, Transform, Transform> spawnable = enemyToSpawn.GetComponent<ISpawnable<int, Transform, Transform>>();
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            Vector3 randomPos = center + new Vector3(Random.Range(-spawnRadius / 2, spawnRadius / 2), 0f, Random.Range(-spawnRadius / 2, spawnRadius / 2));
            spawnable.SetWaypointIndex(waypointIndex, previousWaypoint, targetWaypoint);
            Instantiate(enemyToSpawn, randomPos, Quaternion.identity);
            WaveSystem.numEnemiesAlive++;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}