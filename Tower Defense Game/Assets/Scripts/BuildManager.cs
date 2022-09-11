 using UnityEngine;
 using UnityEngine.UI;
 using TMPro;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake() {
        if (instance != null){
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }
    public GameObject selectedTower;
    private GameObject upgradedTower;
    private GameObject temp;
    public GameObject towerUi;
    //public GameObject towerToBuild;
    public bool isTowerUIOpen = false;
    public Player player;
    public TextMeshProUGUI towerNameText;
    public TextMeshProUGUI upgradeOneCostText;
    public TextMeshProUGUI upgradeTwoCostText;
    public TextMeshProUGUI upgradeThreeCostText;
    public Button pathOneUi;
    public Button pathTwoUi;
    public Button pathThreeUi;
    private Tower selectedTowerScript;

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
        temp = null;
    }
    public void SellTower(){
        ISellable sellable = selectedTower.GetComponent<ISellable>();
        player.IncreasePlayerGold(sellable.GetTowerSellValue());
        if(temp != null)
        {
            Destroy(temp);
        }
        else Destroy(selectedTower);
        HideTowerUI();
    }
    public void UpgradePathOne(){
        IUpgradeable upgradeable = selectedTower.GetComponent<IUpgradeable>();
        if(player.GetCurrentGold() < upgradeable.GetUpgradeOneCost()){
            Debug.Log("Not enough money for that upgrade");
            return;
        }
        player.DecreasePlayerGold(upgradeable.GetUpgradeOneCost());
        temp = Instantiate(upgradeable.GetTowerOneUpgrade(), selectedTower.transform.position, Quaternion.identity);
        upgradedTower = upgradeable.GetTowerOneUpgrade();
        DeleteOldTower();
    }
    public void UpgradePathTwo(){
        IUpgradeable upgradeable = selectedTower.GetComponent<IUpgradeable>();
        if(player.GetCurrentGold() < upgradeable.GetUpgradeTwoCost()){
            Debug.Log("Not enough money for that upgrade");
            return;
        }
        player.DecreasePlayerGold(upgradeable.GetUpgradeTwoCost());
        temp = Instantiate(upgradeable.GetTowerTwoUpgrade(), selectedTower.transform.position, Quaternion.identity);
        upgradedTower = upgradeable.GetTowerTwoUpgrade();
        DeleteOldTower();
    }
    public void UpgradePathThree(){
        IUpgradeable upgradeable = selectedTower.GetComponent<IUpgradeable>();
        if(player.GetCurrentGold() < upgradeable.GetUpgradeThreeCost()){
            Debug.Log("Not enough money for that upgrade");
            return;
        }
        player.DecreasePlayerGold(upgradeable.GetUpgradeThreeCost());
        temp = Instantiate(upgradeable.GetTowerThreeUpgrade(), selectedTower.transform.position, Quaternion.identity);
        upgradedTower = upgradeable.GetTowerThreeUpgrade();
        DeleteOldTower();
    }
    private void DeleteOldTower(){
        Destroy(selectedTower);
        selectedTower = upgradedTower;
        upgradedTower = null;
        selectedTowerScript = selectedTower.GetComponent<Tower>();
        ShowTowerUI(selectedTower,selectedTowerScript.GetTowerName(),selectedTowerScript.GetUpgradeOneCost(), selectedTowerScript.GetUpgradeTwoCost(), selectedTowerScript.GetUpgradeThreeCost()); 
    }
}