using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectilePool : ProjectileObjectPool
{
    [SerializeField] Pool bulletPoolData;
    public static BulletProjectilePool Instance;

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

        projectileObjectPoolParent = GameObject.FindGameObjectWithTag("BulletObjectPool").transform;
        PopulateProjectilePool();
    }

    protected override void PopulateProjectilePool()
    {
        for (int i = 0; i < bulletPoolData.poolSize; i++)
        {
            GameObject projectileInstance = Instantiate(bulletPoolData.prefab);
            projectileInstance.SetActive(false);
            EnqueueProjectile(BulletProjectilePool.Instance.bulletPoolQueue, projectileInstance);
            projectileInstance.transform.parent = projectileObjectPoolParent;
        }
    }

    public override GameObject EnableProjectileInPool(Transform startPoint)
    {
        GameObject activeProjectile;

        if (bulletPoolQueue.Count > 0)
        {
            GameObject dequeuedProjectile = DequeueProjectile(BulletProjectilePool.Instance.bulletPoolQueue);
            activeProjectile = dequeuedProjectile;
        }
        else
        {
            GameObject newInstance = Instantiate(bulletPoolData.prefab);
            activeProjectile = newInstance;
        }

        activeProjectile.transform.parent = projectileObjectPoolParent;
        SetProjectile(activeProjectile, startPoint);

        return activeProjectile;
    }
}
