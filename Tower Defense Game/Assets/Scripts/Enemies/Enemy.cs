using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour, IDamageable<float>, ISpawnable<int,Transform>
{
    [SerializeField]
    protected float movementSpeed;
    [SerializeField]
    protected float rotationSpeed;
    //[SerializeField]
    protected float maxHp;
    //[SerializeField]
    protected float currentHp;
    [SerializeField]
    protected int goldWorth;
    [SerializeField]
    protected string unitName;
    [SerializeField]
    protected int playerDamageAmount;
    [SerializeField]
    protected int waypointIndex = 0;
    [SerializeField]
    protected Transform target;
    protected Player player;
    protected EnemyPool enemyPool;

    void Start() {
        enemyPool = FindObjectOfType<EnemyPool>();
        GameObject Player = GameObject.Find("Player");
        player = Player.GetComponent<Player>();
        currentHp = maxHp;
        target = Waypoints.waypoints[0];
    }
    void Update() {
        Move();
        if (Vector3.Distance(transform.position, target.position) <= 0.2f){
            GetNextWaypoint();
        }
    }
    private void Move(){
        Vector3 direction = target.position - transform.position;
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
        target = Waypoints.waypoints[waypointIndex];
    }
    public void TakeDamage(float damageAmount){
        currentHp -= damageAmount;
        if(currentHp <= 0){
            Die();
        }
    }
    private void Disable(){
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
    public void SetWaypointIndex(int newWaypointIndex, Transform newTarget){
        waypointIndex = newWaypointIndex;
        target = newTarget;
    }
}