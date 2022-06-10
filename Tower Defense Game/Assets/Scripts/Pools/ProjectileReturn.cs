using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileReturn : MonoBehaviour
{
    private ProjectilePool projectilePool;
    void Start()
    {
        projectilePool = FindObjectOfType<ProjectilePool>();
    }

    private void OnDisable() {
        if(projectilePool != null){
            projectilePool.ReturnProjectile(this.gameObject);
        }
    }
}
