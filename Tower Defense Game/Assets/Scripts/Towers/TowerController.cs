using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour, IPurchasble, ISellable, IUpgradeable
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
    [SerializeField] public List<EnemyController> enemyList;
    [SerializeField] private Collider[] enemyArray;
    public enum TowerTargetType {First, Last, Strongest, Weakest, Close};
    TowerTargetType targetType = TowerTargetType.First;

    private void Start() {
        projectilePool = FindObjectOfType<ProjectilePool>();
        enemyList = new List<EnemyController>();
        buildManager.ShowTowerUI(this.gameObject, towerName, upgradeOneCost, upgradeTwoCost, upgradeThreeCost);
    }

    public TowerTargetType GetTargetType(){
        return targetType;
    }

    public void IterateTargetType(){
        if(targetType != TowerTargetType.Close){
            targetType++;
        }
        else targetType = TowerTargetType.First; 
    }

    private void UpdateTarget(){
        GetClosestTarget();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void DetermineTarget(){
        switch (targetType)
        {
            case TowerTargetType.First:
                GetFirstTarget();
                break;
            case TowerTargetType.Last:
                GetLastTarget();
                break;
            case TowerTargetType.Strongest:
                GetStrongestTarget();
                break;
            case TowerTargetType.Weakest:
                GetWeakestTarget();
                break;
            case TowerTargetType.Close:
                GetClosestTarget();
                break;
            default:
                Debug.LogError("Enum out of range!");
                break;
        }
    }

    private void GetEnemyList(){
        enemyArray = Physics.OverlapSphere(transform.position, range, enemyLayer);
        foreach(Collider col in enemyArray){
            if(enemyList.Contains(col.gameObject.GetComponent<EnemyController>())){
               return; 
            }
            enemyList.Add(col.gameObject.GetComponent<EnemyController>());
        }
    }

    private void RemoveFromEnemyList(){
        foreach(EnemyController enemy in enemyList){
            for(int i = 0; i < enemyArray.Length; i++){
                if(enemyArray[i] == enemy.GetComponent<Collider>()){
                    return;
                }
            }
            enemyList.Remove(enemy);
        }
    }

    private void GetFirstTarget(){
        EnemyController furthestEnemy = enemyList[0];
        for(int i = 0; i < enemyList.Count; i++){
            furthestEnemy = CompareGreaterPathProgress(furthestEnemy, enemyList[i]);
            target = furthestEnemy.transform;
            }
    }

    private void GetLastTarget(){
        EnemyController lastEnemy = enemyList[0];
        for(int i = 0; i < enemyList.Count; i++){
            lastEnemy = CompareLeastPathProgress(lastEnemy, enemyList[i]);
            target = lastEnemy.transform;
        }
    }

    private void GetStrongestTarget(){
        EnemyController strongestEnemy = enemyList[0];
        for(int i = 0; i < enemyList.Count; i++){
            strongestEnemy = CompareStrongestEnemy(strongestEnemy, enemyList[i]);
            target = strongestEnemy.transform;
        }
    }

    private void GetWeakestTarget(){
        EnemyController weakestEnemy = enemyList[0];
        for(int i = 0; i < enemyList.Count; i++){
            weakestEnemy = CompareWeakestEnemy(weakestEnemy, enemyList[i]);
            target = weakestEnemy.transform;
        }
    }

    private void GetClosestTarget(){
        EnemyController closestEnemy = enemyList[0];
        float closestDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);

        for(int i = 0; i < enemyList.Count; i++){
            float currentEnemyDistance = Vector3.Distance(transform.position, enemyList[i].transform.position);
            if(currentEnemyDistance < closestDistance){
                closestEnemy = enemyList[i];
                closestDistance = currentEnemyDistance;
            }
        }
        target = closestEnemy.transform;
    }

    private EnemyController CompareGreaterPathProgress(EnemyController enemy1, EnemyController enemy2){
        if(enemy1.GetWaypointIndex() > enemy2.GetWaypointIndex()){
            return enemy1;
        }
        else if(enemy1.GetWaypointIndex() < enemy2.GetWaypointIndex()){
            return enemy2;
        }
        else if(enemy1.GetWaypointIndex() == enemy2.GetWaypointIndex()){
            if(enemy1.GetDistanceTraveled() >= enemy2.GetDistanceTraveled()){
                return enemy1;
            }
            else if(enemy1.GetDistanceTraveled() < enemy2.GetDistanceTraveled()){
                return enemy2;
            } 
        }
        return enemy1;
    }

    private EnemyController CompareLeastPathProgress(EnemyController enemy1, EnemyController enemy2){
        if(enemy1.GetWaypointIndex() < enemy2.GetWaypointIndex()){
            return enemy1;
        }
        if(enemy1.GetWaypointIndex() == enemy2.GetWaypointIndex()){
            if(enemy1.GetDistanceTraveled() < enemy2.GetDistanceTraveled()){
                return enemy1;
            }
            else return enemy2;
        }
        else return enemy2;
    }

    private EnemyController CompareStrongestEnemy(EnemyController enemy1, EnemyController enemy2){
        if(enemy1.GetMaxHP() > enemy2.GetMaxHP()){
            return enemy1;
        }
        else if(enemy1.GetMaxHP() < enemy2.GetMaxHP()){
            return enemy2;
        }
        return enemy1;
    }

    private EnemyController CompareWeakestEnemy(EnemyController enemy1, EnemyController enemy2){
        if(enemy1.GetMaxHP() < enemy2.GetMaxHP()){
            return enemy1;
        }
        else if(enemy1.GetMaxHP() > enemy2.GetMaxHP()){
            return enemy2;
        }
        return enemy1;
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
    protected void Update(){
        GetEnemyList();
        if(enemyList != null && enemyArray != null){
            DetermineTarget();
            RemoveFromEnemyList();
            if(fireCountdown <= 0f){
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }
        if(target == null){
            return;
        }
        fireCountdown -= Time.deltaTime;
    }
}