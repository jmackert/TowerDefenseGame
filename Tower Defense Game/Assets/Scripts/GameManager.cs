using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isGameEnded = false;
    [SerializeField]private Player player;
    [SerializeField]private GameObject gameUI;
    [SerializeField]private GameObject gameOverUI;
    [SerializeField]private WaveSystem waveSystem;
    void Update()
    {
        if(isGameEnded){
            return;
        }
        if(player.GetCurrentLives() <= 0){
            Debug.Log("Game Over You Lose!");
            EndGame();
        }
        if(player.GetCurrentLives() > 0 && waveSystem.GetWaveNumber() == waveSystem.GetNumberOfWaves() && waveSystem.GetNumberOfEnemiesAlive() == 0){
            Debug.Log("WIN");
            EndGame();
        }
    }

    private void EndGame(){
        isGameEnded = true;
        player.UpdateRoundsSurvivedText();
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
    }
}
