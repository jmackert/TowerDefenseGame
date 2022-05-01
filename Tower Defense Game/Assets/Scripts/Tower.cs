using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IPurchasble
{
    [SerializeField]
    protected Transform target;
    protected string enemyTag = "Enemy";
    [SerializeField]
    protected float range;
    [SerializeField]
    protected float fireRate;
    protected float fireCountdown;
    protected float goldCost;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private void UpdateTarget(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance){
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && shortestDistance <= range){
            target = nearestEnemy.transform;
        }else{
            target = null;
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void Shoot(){
        GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if(projectile != null){
            projectile.Seek(target);
        }
    }
    public float GetTowerCost (){
        return goldCost;
    }
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }
    void Update()
    {
        if(target == null){
            return;
        }
        if(fireCountdown <= 0f){
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
}