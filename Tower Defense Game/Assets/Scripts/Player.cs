using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private int currentLives;
    private int startingLives = 10;
    public TextMeshProUGUI currentLivesText;


    void Start(){
        currentLives = startingLives;
        currentLivesText.text = "Lives: " + currentLives;
    }

    public void ReducePlayerLives(){
        currentLives--;
    }

    public void UpdatePlayerLivesText(){
        currentLivesText.text = "Lives: " + currentLives;
    }
    public int GetCurrentLives(){
        return currentLives;
    }
}
