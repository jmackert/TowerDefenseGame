using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    private void Start() {
        buildManager = BuildManager.instance;
    }
    public void PurchaseArcherTower(){
        Debug.Log("Archer Tower Selected");
        buildManager.SetTowerToBuild(buildManager.archerTower);
    }
}
