using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IPurchasble, ISellable, IUpgradeable
{
    protected BuildManager buildManager = BuildManager.instance;
    [SerializeField]
    protected Transform target01;
    [SerializeField]
    protected Transform target02;
    [SerializeField]
    protected Transform target03;
    protected string enemyTag = "Enemy";
    [SerializeField]
    protected float range;
    [SerializeField]
    protected float fireRate;
    [SerializeField]
    protected string towerName;
    protected float fireCountdown;
    protected int goldCost;
    protected int sellValue;
    protected int upgradeOneCost;
    protected int upgradeTwoCost;
    protected int upgradeThreeCost;
    protected int numTargets;
    public GameObject upgradeOne;
    public GameObject upgradeTwo;
    public GameObject upgradeThree;
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
            target01 = nearestEnemy.transform;
        }else{
            target01 = null;
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
            projectile.Seek(target01);
        }
    }
    public int GetTowerCost (){
        return goldCost;
    }
    public int GetTowerSellValue(){
        return sellValue;
    }
    private void OnMouseDown() {
        if(buildManager.towerToBuild != null){
            Debug.Log("Can't Place Turret Here - TODO: Add to UI");
            return;
        }
        if(buildManager.isTowerUIOpen == false){
            buildManager.ShowTowerUI(this.gameObject, towerName, upgradeOneCost, upgradeTwoCost, upgradeThreeCost);
        }
        else if(buildManager.isTowerUIOpen == true && buildManager.selectedTower != this.gameObject){
                buildManager.ShowTowerUI(this.gameObject, towerName, upgradeOneCost, upgradeTwoCost, upgradeThreeCost);
            }
        else if(buildManager.isTowerUIOpen == true && buildManager.selectedTower == this.gameObject){
                buildManager.HideTowerUI();
                return;
        }
    }
    public GameObject GetTowerOneUpgrade(){
        return upgradeOne;
    }
    public GameObject GetTowerTwoUpgrade(){
        return upgradeTwo;
    }
    public GameObject GetTowerThreeUpgrade(){
        return upgradeThree;
    }
    public int GetUpgradeOneCost(){
        return upgradeOneCost;
    }
    public int GetUpgradeTwoCost(){
        return upgradeTwoCost;
    }
    public int GetUpgradeThreeCost(){
        return upgradeThreeCost;
    }
    protected void Update()
    {
        UpdateTarget();
        if(target01 == null){
            return;
        }
        if(fireCountdown <= 0f){
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
}