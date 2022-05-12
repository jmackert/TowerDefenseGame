using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlime : Enemy
{
    public GameObject enemyToSpawn;
    private int numEnemiesToSpawn = 5;

    public KingSlime(){
        this.maxHp = 50f;
        this.unitName = "King Slime";
        this.movementSpeed = 2f;
        this.rotationSpeed = 6f;
        this.goldWorth = 50;
        this.playerDamageAmount = 1 + numEnemiesToSpawn;
    }
    protected override void Die()
    {
        base.Die();
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            Instantiate(enemyToSpawn,transform.position,Quaternion.identity);
            WaveSpawner.numEnemiesAlive++;
        }
    }
}
