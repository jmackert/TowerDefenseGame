using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSlime : KingSlime
{
    public MinionSlime(){
        this.maxHp = 10f;
        this.unitName = "Slime";
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
        Debug.Log("NEW START");
    }
        protected override void Die()
    {
        base.Die();   
    }
}
