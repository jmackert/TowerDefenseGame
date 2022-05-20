using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSlime : KingSlime
{
    public MinionSlime(){
        this.maxHp = 100f;
        this.unitName = "Slime";
        this.movementSpeed = 2.5f;
        this.rotationSpeed = 6f;
        this.goldWorth = 5;
        this.playerDamageAmount = 1;
    }

    private void Start() {
        GameObject Player = GameObject.Find("Player");
        player = Player.GetComponent<Player>();
        currentHp = maxHp;
        Debug.Log("NEW START");
    }
}
