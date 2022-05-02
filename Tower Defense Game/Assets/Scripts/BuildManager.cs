 using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake() {
        if (instance != null){
            Debug.LogError("More one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    private GameObject towerToBuild;
    public GameObject archerTower;
    public Player player;

    public bool CanBuild{get{return towerToBuild != null;}}

    public GameObject GetTowerToBuild(){
        return towerToBuild;
    }
    public void SetTowerToBuild(GameObject _towerToBuild){
        towerToBuild = _towerToBuild;
    }
    public void BuildTowerOn(Tile tile){
        //GameObject tower = (GameObject)Instantiate(towerToBuild, tile.GetBuildPosition(), Quaternion.identity);
        GameObject tower = towerToBuild;
        IPurchasble purchasble = tower.GetComponent<IPurchasble>();
        if(player.GetCurrentGold() < purchasble.GetTowerCost()){
            Debug.Log("Not enough money to build that!");
            return;
        }
        player.DecreasePlayerGold(purchasble.GetTowerCost());
        Instantiate(tower, tile.GetBuildPosition(), Quaternion.identity);
        tile.tower = tower;
    }
}
