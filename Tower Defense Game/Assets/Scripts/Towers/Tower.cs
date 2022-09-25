using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IPurchasble, ISellable, IUpgradeable
{
    protected BuildManager buildManager = BuildManager.instance;
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
    private ProjectilePool projectilePool;
    private int enemyLayer = 1 << 8;
    [SerializeField] public List<Enemy> enemyList;
    [SerializeField] private Collider[] enemyArray;


    private void Start() {
        projectilePool = FindObjectOfType<ProjectilePool>();
        enemyList = new List<Enemy>();
        //buildManager.ShowTowerUI(this.gameObject, towerName, upgradeOneCost, upgradeTwoCost, upgradeThreeCost);
    }

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

    private void GetEnemyList(){
        enemyArray = Physics.OverlapSphere(transform.position, range, enemyLayer);
        foreach(Collider col in enemyArray){
            if(enemyList.Contains(col.gameObject.GetComponent<Enemy>())){
               return; 
            }
            enemyList.Add(col.gameObject.GetComponent<Enemy>());
        }
    }
    private void RemoveFromEnemyList(){
        foreach(Enemy enemy in enemyList){
            for(int i = 0; i < enemyArray.Length; i++){
                if(enemyArray[i] == enemy.GetComponent<Collider>()){
                    return;
                }
            }
            enemyList.Remove(enemy);
        }
    }
    private void GetFirstTarget(){
        Enemy firstEnemy = enemyList[0];
        for(int i = 0; i < enemyList.count; i++){
            if(firstEnemy.GetTargetWaypoint() < enemyList[i].GetProjectile())
            enemyList[i] = firstEnemy; // continue here
        }
        _enemy.GetDistanceTraveled();
        _enemy.GetTargetWaypoint();
    }
    /*private void OnCollisionEnter(Collision collisionInfo) {
        if(collisionInfo.collider.tag == "Enemy")
        {
            Debug.Log("Entered");
            enemyList.Add(collisionInfo.gameObject);
        }
    }
    private void OnCollisionExit(Collision collisionInfo) {
        if(collisionInfo.collider.tag == "Enemy")
        {
            Debug.Log("Removed");
            enemyList.Remove(collisionInfo.gameObject);
        }
    }*/
    private void Shoot(){

        GameObject projectileGO = projectilePool.GetProjectile(projectilePrefab);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.GetComponent<Projectile>();
        projectileGO.transform.position = firePoint.position;
        projectileGO.transform.rotation = firePoint.rotation;

        if(projectile != null){
            projectile.Seek(target);
        }
    }
    public int GetTowerCost (){
        return goldCost;
    }
    public int GetTowerSellValue(){
        return sellValue;
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
    public string GetTowerName(){
        return towerName;
    }
    protected void Update()
    {
        GetEnemyList();
        UpdateTarget();
        RemoveFromEnemyList();
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