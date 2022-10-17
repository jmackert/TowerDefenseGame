using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour, IDamageable<float>, ISpawnable<int,Transform, Transform>
{
    [SerializeField]protected float movementSpeed;
    [SerializeField]protected float rotationSpeed;
    [SerializeField]protected float maxHp;
    [SerializeField]protected float currentHp;
    [SerializeField]protected int goldWorth;
    [SerializeField]protected string unitName;
    [SerializeField]protected int playerDamageAmount;
    [SerializeField]protected int waypointIndex = 0;
    [SerializeField]private float distanceTraveled;
    
    [SerializeField]protected Transform previousWaypoint;
    [SerializeField]protected Transform targetWaypoint;
    protected Player player;
    protected EnemyPool enemyPool;

    private void Start() {
        enemyPool = FindObjectOfType<EnemyPool>();
        GameObject Player = GameObject.Find("Player");
        player = Player.GetComponent<Player>();
        currentHp = maxHp;
        previousWaypoint = Waypoints.waypoints[0];
        targetWaypoint = Waypoints.waypoints[1];
    }
    private void Update() {
        Move();
        if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.2f){
            CalculateTargetWaypoint();
            CalculatePreviousWaypoint();
        }
        GetDistanceTraveled();
    }
    protected void Move(){
        Vector3 direction = targetWaypoint.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World); 
    }
    protected void CalculateTargetWaypoint(){
        if(waypointIndex >= Waypoints.waypoints.Length - 1){
            WaveSystem.numEnemiesAlive--;
            player.ReducePlayerLives(playerDamageAmount);
            Disable();
            return;
        }
        waypointIndex++;
        targetWaypoint = Waypoints.waypoints[waypointIndex];
    }

    protected void CalculatePreviousWaypoint(){
        previousWaypoint = Waypoints.waypoints[waypointIndex - 1];
    }
    public void TakeDamage(float damageAmount){
        currentHp -= damageAmount;
        if(currentHp <= 0){
            Die();
        }
    }
    protected void Disable(){
        currentHp = maxHp;
        waypointIndex = 0;
        previousWaypoint = Waypoints.waypoints[0];
        targetWaypoint = Waypoints.waypoints[1];
        //targetWaypoint = Waypoints.waypoints[waypointIndex];
        gameObject.SetActive(false);
    }
    protected virtual void Die(){
        Disable();
        player.IncreasePlayerGold(goldWorth);
        WaveSystem.numEnemiesAlive--;
    }
    public int GetWaypointIndex(){
        return waypointIndex;
    }
    public void SetWaypointIndex(int newWaypointIndex, Transform newPreviousWaypoint, Transform newTargetWaypoint){
        waypointIndex = newWaypointIndex;
        previousWaypoint = newPreviousWaypoint;
        targetWaypoint = newTargetWaypoint;
    }
    private void CalculateDistanceTraveled()
    {
        float distanceFromPreviousWaypoint = Vector3.Distance(transform.position, previousWaypoint.transform.position);
        float distanceBetweenWaypoints = Vector3.Distance(previousWaypoint.transform.position, targetWaypoint.transform.position);
        distanceTraveled = (distanceFromPreviousWaypoint / distanceBetweenWaypoints) * 100;
        //Debug.Log("Distance Between Waypoints: " + distanceBetweenWaypoints);
        //Debug.Log("Distance Traveled: " + distanceTraveled);
    }
    public float GetDistanceTraveled(){
        return distanceTraveled;
    }
    public float GetMaxHP(){
        return maxHp;
    }
}