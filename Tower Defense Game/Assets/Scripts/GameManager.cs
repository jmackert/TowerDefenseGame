using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isGameEnded = false;
    public Player player;
    void Update()
    {
        if(isGameEnded){
            return;
        }
        if(player.GetCurrentLives() <= 0){
            Debug.Log("Game Over You Lose!");
            EndGame();
        }
    }

    private void EndGame(){
        isGameEnded = true;
        return;
    }
}
