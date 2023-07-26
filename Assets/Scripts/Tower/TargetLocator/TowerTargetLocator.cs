using System.Collections.Generic;
using UnityEngine;

public abstract class TowerTargetLocator : MonoBehaviour
{
    public Transform weapon;
    public Transform startPoint;
    public List<GameObject> enemiesInRange = new List<GameObject>();

    [Header("Tower Factors")]
    public float towerRange; 
    public string towerTag;
    public float shootDelay;
    public int hitDamage;

    protected SphereCollider sphereCollider;
    protected Transform target;
    protected Light lightRange;

    protected bool isTargetLocked = false;
    protected bool isShot = false;
    protected float shotTime = 0f;

    int rangeFactor = 2;

    protected void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = towerRange;
        lightRange = GetComponentInChildren<Light>();
        lightRange.range = towerRange + rangeFactor;
    }

    public void SetAndDisplayStats()
    {
        TowerStatsUI.Instance.InitializeCurrentTowerRange(towerRange);
        TowerStatsUI.Instance.InitializeCurrentShootDelay(shootDelay);
        TowerStatsUI.Instance.InitializeCurrentHitDamage(hitDamage);
        TowerStatsUI.Instance.DisplayTowerStats();
    }

    protected void LockTarget() 
    {
        GameObject[] enemies = enemiesInRange.ToArray();
        if (!isTargetLocked)
        {
            foreach (GameObject enemy in enemies)
            {
                target = enemy.transform;
                isTargetLocked = true;
                break;
            }
        }
    }

    protected abstract void FaceTarget(); 
    protected abstract void AttackTarget(); 
    protected abstract void Shoot();

    protected void ShootDelay(float shootDelay) 
    {
        if (isShot == true && shotTime < 1f)
        {
            shotTime += Time.deltaTime / shootDelay; 
        }
        else
        {
            shotTime = 0f;
            isShot = false;
        }
    }

    public void UpgradeTowerRange(float towerRangeFactor)
    {
        towerRange += towerRangeFactor;
        GetComponent<SphereCollider>().radius = towerRange;
        GetComponentInChildren<Light>().range = towerRange + 2;
        TowerStatsUI.Instance.InitializeCurrentTowerRange(towerRange);
    }

    public void UpgradeShootDelay(float shootDelayFactor)
    {
        shootDelay -= shootDelayFactor;
        TowerStatsUI.Instance.InitializeCurrentShootDelay(shootDelay);
    }

    public void UpgradeHitDamage(int hitDamageFactor)
    {
        hitDamage += hitDamageFactor;
        TowerStatsUI.Instance.InitializeCurrentHitDamage(hitDamage);
    }

    public void RemoveLockOnEnemy(GameObject enemyInstance)
    {
        enemiesInRange.Remove(enemyInstance);
        isTargetLocked = false;
    }

    protected void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject enemyInstance = other.gameObject;
            enemiesInRange.Add(enemyInstance);
            EnemyMover enemyMover = enemyInstance.GetComponent<EnemyMover>();
            PassEnemyMoveSpeed(enemyMover, enemyMover.SlowSpeedFactor);
        }
    }

    protected void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject enemyInstance = other.gameObject;
            enemiesInRange.Remove(enemyInstance);
            EnemyMover enemyMover = enemyInstance.GetComponent<EnemyMover>();
            PassEnemyMoveSpeed(enemyMover, enemyMover.GetEnemyMoveSpeed());
            isTargetLocked = false;
        }
    }

    void PassEnemyMoveSpeed(EnemyMover enemyMover, float enemyMoveSpeedFactor)
    {
        enemyMover.SetEnemyMoveSpeed(enemyMoveSpeedFactor);
    }

    protected void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, towerRange);
    }
}
