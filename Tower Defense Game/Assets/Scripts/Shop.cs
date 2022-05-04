using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    private bool isTowerSelected = false;
    private void Start() {
        buildManager = BuildManager.instance;
    }
    public void SelectArcherTower(){
        if(isTowerSelected == false){
            Debug.Log("Archer Tower Selected");
            buildManager.SetTowerToBuild(buildManager.archerTower);
            isTowerSelected = true;
        }
        else if(isTowerSelected == true){
            buildManager.DeselectTowerToBuild();
            isTowerSelected = false;
            return;
        }

    }
}
