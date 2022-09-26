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
    [SerializeField] private GameObject selectedTower;
    [SerializeField] private GameObject towerUi;
    [SerializeField] private bool isTowerUIOpen = false;
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI towerNameText;
    [SerializeField] private TextMeshProUGUI upgradeOneCostText;
    [SerializeField] private TextMeshProUGUI upgradeTwoCostText;
    [SerializeField] private TextMeshProUGUI upgradeThreeCostText;
    [SerializeField] private Button pathOneUi;
    [SerializeField] private Button pathTwoUi;
    [SerializeField] private Button pathThreeUi;
    
    private GameObject upgradedTower;
    private GameObject temp;
    private TowerController selectedTowerScript;

    public bool GetUIState(){
        return isTowerUIOpen;
    }

    public void CheckUpgradePaths(){
        IUpgradeable upgradeable = selectedTower.GetComponent<IUpgradeable>();
        if(upgradeable.GetTowerOneUpgrade() == null || player.GetCurrentGold() < upgradeable.GetUpgradeOneCost()){
          pathOneUi.interactable = false;  
        }
        else pathOneUi.interactable = true; 
        if(upgradeable.GetTowerOneUpgrade() == null || player.GetCurrentGold() < upgradeable.GetUpgradeOneCost()){
            pathTwoUi.interactable = false;
        }
        else pathTwoUi.interactable = true;
        if(upgradeable.GetTowerOneUpgrade() == null || player.GetCurrentGold() < upgradeable.GetUpgradeOneCost()){
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
        player.DecreasePlayerGold(upgradeable.GetUpgradeOneCost());
        temp = Instantiate(upgradeable.GetTowerOneUpgrade(), selectedTower.transform.position, Quaternion.identity);
        upgradedTower = upgradeable.GetTowerOneUpgrade();
        DeleteOldTower();
    }
    public void UpgradePathTwo(){
        IUpgradeable upgradeable = selectedTower.GetComponent<IUpgradeable>();
        player.DecreasePlayerGold(upgradeable.GetUpgradeTwoCost());
        temp = Instantiate(upgradeable.GetTowerTwoUpgrade(), selectedTower.transform.position, Quaternion.identity);
        upgradedTower = upgradeable.GetTowerTwoUpgrade();
        DeleteOldTower();
    }
    public void UpgradePathThree(){
        IUpgradeable upgradeable = selectedTower.GetComponent<IUpgradeable>();
        player.DecreasePlayerGold(upgradeable.GetUpgradeThreeCost());
        temp = Instantiate(upgradeable.GetTowerThreeUpgrade(), selectedTower.transform.position, Quaternion.identity);
        upgradedTower = upgradeable.GetTowerThreeUpgrade();
        DeleteOldTower();
    }
    private void DeleteOldTower(){
        Destroy(selectedTower);
        selectedTower = upgradedTower;
        upgradedTower = null;
        selectedTowerScript = selectedTower.GetComponent<TowerController>();
        ShowTowerUI(selectedTower,selectedTowerScript.GetTowerName(),selectedTowerScript.GetUpgradeOneCost(), selectedTowerScript.GetUpgradeTwoCost(), selectedTowerScript.GetUpgradeThreeCost()); 
    }
}