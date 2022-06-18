using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturn : MonoBehaviour
{
    private EnemyPool enemyPool;
    void Start()
    {
        enemyPool = FindObjectOfType<EnemyPool>();
    }

    private void OnDisable() {
        if(enemyPool != null){
            enemyPool.ReturnEnemy(this.gameObject);
        }
    }
}
