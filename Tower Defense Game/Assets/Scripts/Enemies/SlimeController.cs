using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController
{
    public SlimeController(){
        this.maxHp = 10f;
        this.unitName = "Slime";
        this.movementSpeed = 2.5f;
        this.rotationSpeed = 6f;
        this.goldWorth = 5;
        this.playerDamageAmount = 1;
    }
}
