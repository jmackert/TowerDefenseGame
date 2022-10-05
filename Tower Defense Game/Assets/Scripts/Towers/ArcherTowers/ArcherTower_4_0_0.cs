using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower_4_0_0 : TowerController
{
    public ArcherTower_4_0_0(){
        this.towerName = "Archer Tower 4_0_0";
        this.range = 2.5f;
        this.fireRate = 1f;
        this.fireCountdown = 0f;
        this.goldCost = 100;
        this.upgradeOneCost = 100;
        this.upgradeTwoCost = 100;
        this.upgradeThreeCost = 100;
        this.sellValue = 50;
    }
}
