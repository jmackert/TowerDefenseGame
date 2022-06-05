using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower_0_2_0 : Tower
{
        public ArcherTower_0_2_0(){
        this.towerName = "Archer Tower 0_2_0";
        this.range = 2.5f;
        this.fireRate = 2f;
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
