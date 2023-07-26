using UnityEngine;

public class CanonTargetLocator : TowerTargetLocator
{
    public static CanonTargetLocator Instance = null;

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
            Invoke("AttackTarget", 0.5f);
        }
    }

    protected override void FaceTarget()
    {
        Vector3 direction = target.position - startPoint.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        weapon.rotation = Quaternion.Slerp(startPoint.rotation, lookRotation, Time.deltaTime * 5f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
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
        GameObject canonProjectileObj = CanonProjectilePool.Instance.EnableProjectileInPool(startPoint);
        CanonProjectile canonProjectile = canonProjectileObj.GetComponent<CanonProjectile>();
        canonProjectile.HitDamageFactor = hitDamage;
        canonProjectile.ShootProjectile(startPoint);
    }
}
