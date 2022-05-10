using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower_0_0_1 : Tower
{
    public ArcherTower_0_0_1(){
        this.towerName = "Archer Tower 1_0_0";
        this.range = 3f;
        this.fireRate = 1f;
        this.fireCountdown = 0f;
        this.goldCost = 100;
        this.upgradeOneCost = 100;
        this.upgradeTwoCost = 100;
        this.upgradeThreeCost = 100;
        this.sellValue = 50;
    }

    private void Start() {
        buildManager.ShowTowerUI(this.gameObject, towerName, upgradeOneCost, upgradeTwoCost, upgradeThreeCost);
    }
}
