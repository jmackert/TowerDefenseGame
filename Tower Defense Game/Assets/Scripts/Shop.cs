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
    private void CheckTowerCost()
    {
        IPurchasble purchasble = tower.GetComponent<IPurchasble>();
        if(player.GetCurrentGold() < purchasble.GetTowerCost())
        {
            btn.interactable = false;
        }
        else btn.interactable = true;
    }
}
