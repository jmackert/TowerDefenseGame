using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower_1_2_0 : Tower
{
        public ArcherTower_1_2_0(){
        this.towerName = "Archer Tower 1_2_0";
        this.range = 2.5f;
        this.fireRate = 2f;
        this.fireCountdown = 0f;
        this.goldCost = 100;
        this.upgradeOneCost = 100;
        this.upgradeTwoCost = 100;
        this.upgradeThreeCost = 100;
        this.sellValue = 50;
    }
}
