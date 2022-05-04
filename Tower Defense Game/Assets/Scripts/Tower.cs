using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IPurchasble
{
    BuildManager buildManager = BuildManager.instance;
    [SerializeField]
    protected Transform target;
    protected string enemyTag = "Enemy";
    [SerializeField]
    protected float range;
    [SerializeField]
    protected float fireRate;
    [SerializeField]
    protected string towerName;
    protected float fireCountdown;
    protected int goldCost;
    protected int upgradeCost;
    protected int sellValue;
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
    public int GetTowerCost (){
        return goldCost;
    }
    private void OnMouseDown() {
        if(buildManager.towerToBuild != null){
            Debug.Log("Can't Place Turret Here - TODO: Add to UI");
            return;
        }
        if(buildManager.isTowerUIOpen == false){
            buildManager.ShowTowerUI(towerName);
            Debug.Log("This tower is selected!");
        }
        else if(buildManager.isTowerUIOpen == true){
            buildManager.HideTowerUI();
            Debug.Log("Deselected tower!");
            return;
        }
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