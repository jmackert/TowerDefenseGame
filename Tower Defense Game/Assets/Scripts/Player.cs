using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private int currentLives;
    private int startingLives = 10;
    private int startingGold = 500;
    private int currentGold;
    public TextMeshProUGUI currentLivesText;
    public TextMeshProUGUI currentGoldText;


    void Start(){
        currentGold = startingGold;
        currentLives = startingLives;
        currentLivesText.text = "Lives: " + currentLives;
        currentGoldText.text = "Gold: " + currentGold;
    }

    public void ReducePlayerLives(){
        currentLives--;
        UpdatePlayerLivesText();
    }

    public void IncreasePlayerGold(int goldAmount){
        currentGold += goldAmount;
        UpdatePlayerGoldText();
    }
    public void DecreasePlayerGold(int goldAmount){
        currentGold-= goldAmount;
        UpdatePlayerGoldText();
    }

    public void UpdatePlayerLivesText(){
        currentLivesText.text = "Lives: " + currentLives;
        
    }
    public void UpdatePlayerGoldText(){
        currentGoldText.text = "Gold: " + currentGold;
    }
    public int GetCurrentLives(){
        return currentLives;
    }
    public int GetCurrentGold(){
        return currentGold;
    }
}
