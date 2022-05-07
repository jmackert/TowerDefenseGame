using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPurchasble{
    int GetTowerCost();
}

public interface ISellable{
    int GetTowerSellValue();
}

public interface IUpgradeable{
    GameObject GetTowerOneUpgrade();
    GameObject GetTowerTwoUpgrade();
    GameObject GetTowerThreeUpgrade();
    int GetUpgradeOneCost();
    int GetUpgradeTwoCost();
    int GetUpgradeThreeCost();
}