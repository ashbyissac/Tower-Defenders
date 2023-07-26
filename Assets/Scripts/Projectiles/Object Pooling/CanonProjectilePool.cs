using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonProjectilePool : ProjectileObjectPool
{
    [SerializeField] Pool canonPoolData;
    public static CanonProjectilePool Instance;

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

        projectileObjectPoolParent = GameObject.FindGameObjectWithTag("CanonObjectPool").transform;
        PopulateProjectilePool();
    }

    protected override void PopulateProjectilePool()
    {
        for (int i = 0; i < canonPoolData.poolSize; i++)
        {
            GameObject projectileInstance = Instantiate(canonPoolData.prefab);
            projectileInstance.SetActive(false);
            EnqueueProjectile(CanonProjectilePool.Instance.canonPoolQueue, projectileInstance);
            projectileInstance.transform.parent = projectileObjectPoolParent;
        }
    }

    public override GameObject EnableProjectileInPool(Transform startPoint)
    {
        GameObject activeProjectile;

        if (canonPoolQueue.Count > 0)
        {
            GameObject dequeuedProjectile = DequeueProjectile(CanonProjectilePool.Instance.canonPoolQueue);
            activeProjectile = dequeuedProjectile;
        }
        else
        {
            GameObject newInstance = Instantiate(canonPoolData.prefab);
            activeProjectile = newInstance;
        }

        activeProjectile.transform.parent = projectileObjectPoolParent;
        SetProjectile(activeProjectile, startPoint);

        return activeProjectile;
    }
}
