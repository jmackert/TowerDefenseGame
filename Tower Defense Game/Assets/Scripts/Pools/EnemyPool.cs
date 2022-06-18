using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> enemyPool = new Dictionary<string, Queue<GameObject>>();

    public GameObject GetEnemy(GameObject enemyToGet){
        if(enemyPool.TryGetValue(enemyToGet.name, out Queue<GameObject> enemyList)){
            if(enemyList.Count == 0){
                return CreateNewEnemy(enemyToGet);
            }
            else
            {
                GameObject _enemy = enemyList.Dequeue();
                _enemy.SetActive(true);
                return _enemy;
            }
        }
        else
        {
            return CreateNewEnemy(enemyToGet);
        }
    }
    private GameObject CreateNewEnemy(GameObject enemyToCreate){
        GameObject newEnemy = Instantiate(enemyToCreate);
        newEnemy.name = enemyToCreate.name;
        return newEnemy;
    }
    public void ReturnEnemy(GameObject enemyToReturn){
        if(enemyPool.TryGetValue(enemyToReturn.name, out Queue<GameObject> enemyList)){
            enemyList.Enqueue(enemyToReturn);
        }
        else
        {
        Queue<GameObject> newEnemyQueue = new Queue<GameObject>();
        newEnemyQueue.Enqueue(enemyToReturn);
        enemyPool.Add(enemyToReturn.name, newEnemyQueue);
        }
        enemyToReturn.SetActive(false);
    }
}
