using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSlimeController : KingSlimeController
{
    public MinionSlimeController(){
        this.maxHp = 10f;
        this.unitName = "Minion Slime";
        this.movementSpeed = 2.5f;
        this.rotationSpeed = 6f;
        this.goldWorth = 5;
        this.playerDamageAmount = 1;
    }

    private void Start() {
        enemyPool = FindObjectOfType<EnemyPool>();
        GameObject Player = GameObject.Find("Player");
        player = Player.GetComponent<Player>();
        currentHp = maxHp;
    }

    protected override void Die()
    {
        Disable();
        player.IncreasePlayerGold(goldWorth);
        WaveSystem.numEnemiesAlive--;
    }
}