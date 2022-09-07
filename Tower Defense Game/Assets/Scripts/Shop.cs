using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField]private GameObject tower;
    [SerializeField]private Player player;
    [SerializeField]private Button btn;
    private void Update() 
    {
        CheckTowerCost();
    }

    /*BuildManager buildManager;
    private void Start() {
        buildManager = BuildManager.instance;
    }

    public void SelectArcherTower(){
            Debug.Log("Archer Tower Selected");
            buildManager.SetTowerToBuild(buildManager.archerTower);
    }*/

    public void CheckTowerCost()
    {
        IPurchasble purchasble = tower.GetComponent<IPurchasble>();
        if(player.GetCurrentGold() < purchasble.GetTowerCost())
        {
            btn.interactable = false;
        }
        else btn.interactable = true;
    }

    /*private void CheckUpgradePaths(){
        IUpgradeable upgradeable = tower.GetComponent<IUpgradeable>();
        IPurchasble purchasble = tower.GetComponent<IPurchasble>();
        if(upgradeable.GetTowerOneUpgrade() == null || player.GetCurrentGold() < purchasble.GetTowerCost()){
          pathOneUi.interactable = false;  
        }
        else pathOneUi.interactable = true; 
        if(upgradeable.GetTowerOneUpgrade() == null || player.GetCurrentGold() < purchasble.GetTowerCost()){
            pathTwoUi.interactable = false;
        }
        else pathTwoUi.interactable = true;
        if(upgradeable.GetTowerOneUpgrade() == null || player.GetCurrentGold() < purchasble.GetTowerCost()){
            pathThreeUi.interactable = false;
        } 
        else pathThreeUi.interactable = true;
    }*/
}
