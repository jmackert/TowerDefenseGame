using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour, IDamageable<float>
{
    [SerializeField]
    protected float movementSpeed;
    [SerializeField]
    protected float rotationSpeed;
    [SerializeField]
    protected float maxHp;
    [SerializeField]
    protected float currentHp;
    [SerializeField]
    protected int goldWorth;
    [SerializeField]
    protected string unitName;
    [SerializeField]
    protected int playerDamageAmount;
    protected int waypointIndex = 0;
    protected Transform target;
    protected Player player;

    void Start() {
        GameObject Player = GameObject.Find("Player");
        player = Player.GetComponent<Player>();
        target = Waypoints.waypoints[0];
        currentHp = maxHp;
    }
    void Update() {
        Move();
        if (Vector3.Distance(transform.position, target.position) <= 0.2f){
            GetNexWaypoint();
        }
    }
    private void Move(){
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World); 
    }
    private void GetNexWaypoint(){
        if(waypointIndex >= Waypoints.waypoints.Length - 1){
            WaveSpawner.numEnemiesAlive--;
            player.ReducePlayerLives(playerDamageAmount);
            Destroy(gameObject);
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
    protected virtual void Die(){
        Destroy(gameObject);
        player.IncreasePlayerGold(goldWorth);
        WaveSpawner.numEnemiesAlive--;
    }
}
