 using UnityEngine;
 using TMPro;

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
    public GameObject selectedTower;
    public GameObject towerUi;
    public GameObject towerToBuild;
    public GameObject archerTower;
    public bool isTowerUIOpen = false;
    public Player player;
    public TextMeshProUGUI towerNameText;


    public bool CanBuild{get{return towerToBuild != null;}}

    public GameObject GetTowerToBuild(){
        return towerToBuild;
    }
    public void SetTowerToBuild(GameObject _towerToBuild){
        towerToBuild = _towerToBuild;
    }
    public void DeselectTowerToBuild(){
        towerToBuild = null;
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
    public void ShowTowerUI(string towerName, GameObject _selectedTower){
        towerNameText.text = towerName;
        selectedTower = _selectedTower;
        towerUi.SetActive(true);
        isTowerUIOpen = true;
    }
    public void HideTowerUI(){
        towerUi.SetActive(false);
        isTowerUIOpen = false;
    }
    public void SellTower(){
        ISellable sellable = selectedTower.GetComponent<ISellable>();
        player.IncreasePlayerGold(sellable.GetTowerSellValue());
        Destroy(selectedTower);
    }
}
