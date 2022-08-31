using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private int currentLives;
    private int startingLives = 100;
    private int startingGold = 10;
    private int currentGold;
    private int roundsSurvived = -1;
    public TextMeshProUGUI currentLivesText;
    public TextMeshProUGUI currentGoldText;
    public TextMeshProUGUI roundsSurvivedText;


    void Start(){
        currentGold = startingGold;
        currentLives = startingLives;
        currentLivesText.text = "Lives: " + currentLives;
        currentGoldText.text = "Gold: " + currentGold;
    }

    public void ReducePlayerLives(int damageAmount){
        currentLives -= damageAmount;
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
    public void IncreaseRoundsSurvived(){
        roundsSurvived++;
    }
    public void UpdateRoundsSurvivedText(){
        roundsSurvivedText.text = roundsSurvived.ToString();
    }
}
