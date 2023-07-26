using UnityEngine;

public class BulletTargetLocator : TowerTargetLocator
{
    public static BulletTargetLocator Instance = null;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
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
        GameObject bulletProjectileObj = BulletProjectilePool.Instance.EnableProjectileInPool(startPoint);
        BulletProjectile bulletProjectile = bulletProjectileObj.GetComponent<BulletProjectile>();
        bulletProjectile.HitPoints = hitDamage;
        bulletProjectile.ShootProjectile(startPoint);
    }
}
