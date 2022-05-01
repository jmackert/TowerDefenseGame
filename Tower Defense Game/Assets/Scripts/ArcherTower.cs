using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    public ArcherTower(){
        this.range = 2.5f;
        this.fireRate = 1f;
        this.fireCountdown = 0f;
        this.goldCost = 100f;
        this.target = target;
    }
}
