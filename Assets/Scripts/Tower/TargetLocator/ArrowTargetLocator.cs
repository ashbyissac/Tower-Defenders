using UnityEngine;

public class ArrowTargetLocator : TowerTargetLocator
{
    public static ArrowTargetLocator Instance = null;

    void Awake()
    { 
        Instance = this; 
    }

    void Update()
    {
        LockTarget();
        if (isTargetLocked)
        {
            FaceTarget();
            AttackTarget();
        }
    }

    protected override void FaceTarget()
    {
        weapon.LookAt(target);
    }

    protected override void AttackTarget()
    {
        if (Vector3.Distance(transform.position, target.position) < towerRange && !isShot)
        {
            Shoot();
            isShot = true;
        }
        ShootDelay(shootDelay);
    }

    protected override void Shoot()
    {
        GameObject arrowProjectileObj = ArrowProjectilePool.Instance.EnableProjectileInPool(startPoint);
        ArrowProjectile arrowProjectile = arrowProjectileObj.GetComponent<ArrowProjectile>();
        arrowProjectile.HitPoints = hitDamage;
        arrowProjectile.ShootProjectile(startPoint); 
    }
}