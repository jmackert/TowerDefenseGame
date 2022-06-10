using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> projectilePool = new Dictionary<string, Queue<GameObject>>();

    public GameObject GetProjectile(GameObject projectileToGet){
        if(projectilePool.TryGetValue(projectileToGet.name, out Queue<GameObject> projectileList)){
            if(projectileList.Count == 0){
                return CreateNewProjectile(projectileToGet);
            }
            else
            {
                GameObject _projectile = projectileList.Dequeue();
                _projectile.SetActive(true);
                return _projectile;
            }
        }
        else
        {
            return CreateNewProjectile(projectileToGet);
        }
    }
    private GameObject CreateNewProjectile(GameObject projectileToCreate){
        GameObject newProjectile = Instantiate(projectileToCreate);
        newProjectile.name = projectileToCreate.name;
        return newProjectile;
    }
    public void ReturnProjectile(GameObject projectileToReturn){
        if(projectilePool.TryGetValue(projectileToReturn.name, out Queue<GameObject> projectileList)){
            projectileList.Enqueue(projectileToReturn);
        }
        else
        {
        Queue<GameObject> newProjectileQueue = new Queue<GameObject>();
        newProjectileQueue.Enqueue(projectileToReturn);
        projectilePool.Add(projectileToReturn.name, newProjectileQueue);
        }
        projectileToReturn.SetActive(false);
    }
}
