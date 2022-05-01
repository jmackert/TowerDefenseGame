using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public Slime(){
        this.maxHp = 10f;
        this.unitName = "Slime";
        this.movementSpeed = 2.5f;
        this.rotationSpeed = 6f;
        this.goldWorth = 5;
    }
}