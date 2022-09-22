using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour, IDamageable<float>, ISpawnable<int,Transform>
{
    [SerializeField]protected float movementSpeed;
    [SerializeField]protected float rotationSpeed;
    [SerializeField]protected float maxHp;
    [SerializeField]protected float currentHp;
    [SerializeField]protected int goldWorth;
    [SerializeField]protected string unitName;
    [SerializeField]protected int playerDamageAmount;
    [SerializeField]protected int waypointIndex = 0;
    
    [SerializeField]protected Transform previousWaypoint;
    [SerializeField]protected Transform targetWaypoint;
    protected Player player;
    protected EnemyPool enemyPool;
    [SerializeField] private List<Tower> towerList;

    private void Start() {
        enemyPool = FindObjectOfType<EnemyPool>();
        GameObject Player = GameObject.Find("Player");
        player = Player.GetComponent<Player>();
        currentHp = maxHp;
        previousWaypoint = Waypoints.waypoints[0];
        targetWaypoint = Waypoints.waypoints[1];
        towerList = new List<Tower>();
    }
    private void Update() {
        Move();
        if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.2f){
            GetNextWaypoint();
            GetPreviousWaypoint();
        }
        GetDistanceTraveled();
    }
    private void Move(){
        Vector3 direction = targetWaypoint.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World); 
    }
    private void GetNextWaypoint(){
        if(waypointIndex >= Waypoints.waypoints.Length - 1){
            WaveSystem.numEnemiesAlive--;
            player.ReducePlayerLives(playerDamageAmount);
            Disable();
            return;
        }
        waypointIndex++;
        targetWaypoint = Waypoints.waypoints[waypointIndex];
    }

    private void GetPreviousWaypoint(){
        previousWaypoint = Waypoints.waypoints[waypointIndex - 1];
    }
    public void TakeDamage(float damageAmount){
        currentHp -= damageAmount;
        if(currentHp <= 0){
            Die();
        }
    }
    private void Disable(){
        currentHp = maxHp;
        waypointIndex = 0;
        targetWaypoint = Waypoints.waypoints[waypointIndex];
        gameObject.SetActive(false);
    }
    protected virtual void Die(){
        Disable();
        player.IncreasePlayerGold(goldWorth);
        WaveSystem.numEnemiesAlive--;
        foreach (Tower tower in towerList)
        {
            tower.enemyList.Remove(this.gameObject);
        }
    }
    public int GetWaypointIndex(){
        return waypointIndex;
    }
    public void SetWaypointIndex(int newWaypointIndex, Transform newTargetWaypoint){
        waypointIndex = newWaypointIndex;
        targetWaypoint = newTargetWaypoint;
    }
    private void GetLists(){

    }
    private void OnCollisionEnter(Collision collisionInfo) {
        if(collisionInfo.collider.gameObject.layer == LayerMask.NameToLayer("Towers"))
        {
            Debug.Log("Entered Tower");
            towerList.Add(collisionInfo.gameObject.GetComponent<Tower>());
        }
    }

    private void GetDistanceTraveled()
    {
        float distanceFromPreviousWaypoint = Vector3.Distance(transform.position, previousWaypoint.transform.position);
        float distanceBetweenWaypoints = Vector3.Distance(previousWaypoint.transform.position, targetWaypoint.transform.position);
        float distanceTraveled = (distanceFromPreviousWaypoint / distanceBetweenWaypoints) * 100;
        //Debug.Log("Distance Between Waypoints: " + distanceBetweenWaypoints);
        //Debug.Log("Distance Traveled: " + distanceTraveled);
    }
}