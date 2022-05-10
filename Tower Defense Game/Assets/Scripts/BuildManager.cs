 using UnityEngine;
 using UnityEngine.UI;
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
    //public GameObject towerToUpgradeTo;
    public bool isTowerUIOpen = false;
    public Player player;
    public TextMeshProUGUI towerNameText;
    public TextMeshProUGUI upgradeOneCostText;
    public TextMeshProUGUI upgradeTwoCostText;
    public TextMeshProUGUI upgradeThreeCostText;
    public Button pathOneUi;
    public Button pathTwoUi;
    public Button pathThreeUi;
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
        GameObject tower = towerToBuild;
        IPurchasble purchasble = tower.GetComponent<IPurchasble>();
        if(player.GetCurrentGold() < purchasble.GetTowerCost()){
            Debug.Log("Not enough money to build that!");
            towerToBuild = null;
            return;
        }
        player.DecreasePlayerGold(purchasble.GetTowerCost());
        Instantiate(tower, tile.GetBuildPosition(), Quaternion.identity);
        tile.tower = tower;
        towerToBuild = null;
        return;
    }
    private void CheckUpgradePaths(){
        IUpgradeable upgradeable = selectedTower.GetComponent<IUpgradeable>();
        if(upgradeable.GetTowerOneUpgrade() == null){
          pathOneUi.interactable = false;  
        }
        else pathOneUi.interactable = true; 
        if(upgradeable.GetTowerOneUpgrade() == null){
            pathTwoUi.interactable = false;
        }
        else pathTwoUi.interactable = true;
        if(upgradeable.GetTowerOneUpgrade() == null){
            pathThreeUi.interactable = false;
        } 
        else pathThreeUi.interactable = true;
    }
    public void ShowTowerUI(GameObject _selectedTower, string towerName, int upgradeOneCost, int upgradeTwoCost, int upgradeThreeCost){
        towerNameText.text = towerName;
        upgradeOneCostText.text = upgradeOneCost.ToString();
        upgradeTwoCostText.text = upgradeTwoCost.ToString();
        upgradeThreeCostText.text = upgradeThreeCost.ToString();
        selectedTower = _selectedTower;
        CheckUpgradePaths();
        towerUi.SetActive(true);
        isTowerUIOpen = true;
        
    }
    public void HideTowerUI(){
        towerUi.SetActive(false);
        selectedTower = null;
        isTowerUIOpen = false;
    }
    public void SellTower(){
        ISellable sellable = selectedTower.GetComponent<ISellable>();
        player.IncreasePlayerGold(sellable.GetTowerSellValue());
        Destroy(selectedTower);
        HideTowerUI();
    }
    public void UpgradePathOne(){
        IUpgradeable upgradeable = selectedTower.GetComponent<IUpgradeable>();
        if(player.GetCurrentGold() < upgradeable.GetUpgradeOneCost()){
            Debug.Log("Not enough money for that upgrade");
            return;
        }
        player.DecreasePlayerGold(upgradeable.GetUpgradeOneCost());
        Instantiate(upgradeable.GetTowerOneUpgrade(), selectedTower.transform.position, Quaternion.identity);
        Destroy(selectedTower);   
    }
    public void UpgradePathTwo(){
        IUpgradeable upgradeable = selectedTower.GetComponent<IUpgradeable>();
        if(player.GetCurrentGold() < upgradeable.GetUpgradeTwoCost()){
            Debug.Log("Not enough money for that upgrade");
            return;
        }
        player.DecreasePlayerGold(upgradeable.GetUpgradeTwoCost());
        Instantiate(upgradeable.GetTowerTwoUpgrade(), selectedTower.transform.position, Quaternion.identity);
        Destroy(selectedTower); 
    }
    public void UpgradePathThree(){
        IUpgradeable upgradeable = selectedTower.GetComponent<IUpgradeable>();
        if(player.GetCurrentGold() < upgradeable.GetUpgradeThreeCost()){
            Debug.Log("Not enough money for that upgrade");
            return;
        }
        player.DecreasePlayerGold(upgradeable.GetUpgradeThreeCost());
        Instantiate(upgradeable.GetTowerThreeUpgrade(), selectedTower.transform.position, Quaternion.identity);
        Destroy(selectedTower); 
    }
}