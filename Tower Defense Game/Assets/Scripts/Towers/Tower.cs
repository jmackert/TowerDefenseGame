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
    [SerializeField] public List<GameObject> enemyList;

    private void Start() {
        projectilePool = FindObjectOfType<ProjectilePool>();
        enemyList = new List<GameObject>();
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

    /*private void GetEnemyList(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach(Collider col in hitColliders){
            if(enemyList.Contains(col.gameObject)){
                return;
            }
            if(col.transform.tag == "Enemy"){
                enemyList.Add(col.gameObject);
            }
        }
        
    }*/
    private void OnCollisionEnter(Collision collisionInfo) {
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
    }
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
        //GetEnemyList();
        UpdateTarget();
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