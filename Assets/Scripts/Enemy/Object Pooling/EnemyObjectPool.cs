using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public int poolSize;
        public GameObject prefab;
    }

    [SerializeField] Queue<GameObject> enemyPoolQueue = new Queue<GameObject>();
    [SerializeField] List<Pool> enemyPools;

    public static EnemyObjectPool Instance = null;

    int randomEnemyPoolIndex;
    Transform enemyObjectPoolParent;

    void Awake()
    {
        enemyObjectPoolParent = GameObject.FindGameObjectWithTag("EnemyObjectPool").transform;
        if (Instance == null) Instance = this;
        PopulateEnemyPool();
    }

    /* 
     *  Pre-instantiates the enemies and adds them 
     *  onto the pool
    */ 
    void PopulateEnemyPool()
    {
        for (int i = 0; i < enemyPools[randomEnemyPoolIndex].poolSize; i++) 
        {
            RandomizeEnemy();
            GameObject enemyInstance = Instantiate(enemyPools[randomEnemyPoolIndex].prefab);
            enemyInstance.SetActive(false);
            EnqueueEnemy(enemyInstance);
            enemyInstance.transform.parent = enemyObjectPoolParent;
        }
    }

    void RandomizeEnemy()
    {
        randomEnemyPoolIndex = Mathf.FloorToInt(UnityEngine.Random.Range(0, enemyPools.Count));
    }

    /*
     *  If enemy is available in the queue, that particular enemy will
     *  be enabled, if not available new enemies will be instantiated.
     */
    public void EnableEnemyInPool(Transform enemyStartPoint)
    {
        GameObject activeEnemyInstance;

        if (enemyPoolQueue.Count > 0)
        {
            GameObject dequeuedEnemy = DequeueEnemy();
            dequeuedEnemy.SetActive(true);
            activeEnemyInstance = dequeuedEnemy;
        }
        else
        {
            RandomizeEnemy();
            activeEnemyInstance = Instantiate(enemyPools[randomEnemyPoolIndex].prefab);
        }

        SetPositionAndRotation(enemyStartPoint, activeEnemyInstance);
    }

    private void SetPositionAndRotation(Transform enemyStartPoint, GameObject activeEnemyInstance)
    {
        activeEnemyInstance.transform.position = enemyStartPoint.position;
        activeEnemyInstance.transform.rotation = enemyStartPoint.rotation;
        activeEnemyInstance.transform.parent = enemyObjectPoolParent;
    }

    public void EnqueueEnemy(GameObject enemyInstance)
    {
        enemyPoolQueue.Enqueue(enemyInstance);
    }

    public GameObject DequeueEnemy()
    {
        return enemyPoolQueue.Dequeue();
    }
}
