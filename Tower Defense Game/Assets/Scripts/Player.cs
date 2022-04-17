using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int currentLives;
    private int startingLives = 10;

    void Start(){
        currentLives = startingLives;
    }
}
