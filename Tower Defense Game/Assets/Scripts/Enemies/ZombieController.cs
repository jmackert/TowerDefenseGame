using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : EnemyController
{
    [SerializeField]private DayNightCycleController dayNightCycleController;

    public ZombieController(){
        this.maxHp = 15f;
        this.unitName = "Zombie";
        this.movementSpeed = 2f;
        this.rotationSpeed = 6f;
        this.goldWorth = 5;
        this.playerDamageAmount = 1;
    }
    
    private void Start() {
        enemyPool = FindObjectOfType<EnemyPool>();
        GameObject Player = GameObject.Find("Player");
        player = Player.GetComponent<Player>();
        GameObject DayNightCycleController = GameObject.Find("DayNightCycleController");
        dayNightCycleController = DayNightCycleController.GetComponent<DayNightCycleController>();
        currentHp = maxHp;
        previousWaypoint = Waypoints.waypoints[0];
        targetWaypoint = Waypoints.waypoints[1];
    }

    private void Update() {
        Move();
        CheckTimeOfDay();
        if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.2f){
            CalculateTargetWaypoint();
            CalculatePreviousWaypoint();
        }
        GetDistanceTraveled();
    }

    private void CheckTimeOfDay()
    {
        if(dayNightCycleController.CheckIsNightTime() == true)
        {
            movementSpeed = 4f;
        }
        else movementSpeed = 2f;
    }
}
