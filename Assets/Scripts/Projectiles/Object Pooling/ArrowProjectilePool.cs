using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectilePool : ProjectileObjectPool
{
    [SerializeField] Pool arrowPoolData;
    public static ArrowProjectilePool Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }

        projectileObjectPoolParent = GameObject.FindGameObjectWithTag("ArrowObjectPool").transform;
        PopulateProjectilePool();
    }

    protected override void PopulateProjectilePool()
    {
        for (int i = 0; i < arrowPoolData.poolSize; i++)
        {
            GameObject projectileInstance = Instantiate(arrowPoolData.prefab);
            projectileInstance.SetActive(false);
            EnqueueProjectile(ArrowProjectilePool.Instance.arrowPoolQueue, projectileInstance);
            projectileInstance.transform.parent = projectileObjectPoolParent;
        }
    }

    public override GameObject EnableProjectileInPool(Transform startPoint)
    {
        GameObject activeProjectile;

        if (arrowPoolQueue.Count > 0)
        {
            GameObject dequeuedProjectile = DequeueProjectile(ArrowProjectilePool.Instance.arrowPoolQueue);
            activeProjectile = dequeuedProjectile;
        }
        else
        {
            GameObject newInstance = Instantiate(arrowPoolData.prefab);
            activeProjectile = newInstance;
        }

        activeProjectile.transform.parent = projectileObjectPoolParent;
        SetProjectile(activeProjectile, startPoint);

        return activeProjectile;
    }
}
