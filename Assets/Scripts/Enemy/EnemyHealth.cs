using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int enemyHitPoints;

    EnemyHealthBar enemyHealthBar;
    int currentHealth;

    /*
     * Mainly resets the enemy's health
     */
    void OnEnable() 
    {
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
        currentHealth = maxHealth;    
    }

    public int GetCurrentEnemyHealth() { return currentHealth; }

    public int GetMaxEnemyHealth() { return maxHealth; }

    /*
     * Identifies the projectile that hit the enemy 
     * and updates and checks the enemy health
     */
    void OnCollisionEnter(Collision collision)
    {
        GameObject enemyInstance = this.gameObject;

        if (collision.gameObject.tag == "ArrowProjectile")
        {
            ArrowProjectile arrowProjectile = collision.gameObject.GetComponent<ArrowProjectile>();
            UpdateEnemyHealth(arrowProjectile.HitPoints);
            CheckEnemyHealth(enemyInstance);
        }

        if (collision.gameObject.tag == "BulletProjectile")
        {
            BulletProjectile bulletProjectile = collision.gameObject.GetComponent<BulletProjectile>();
            UpdateEnemyHealth(bulletProjectile.HitPoints);
            CheckEnemyHealth(enemyInstance);
        }
    }

    void CheckEnemyHealth(GameObject enemyInstance)
    {
        if (currentHealth < 1)
        {
            DetectWhichTowerHitEnemy(enemyInstance);
            DeactivateEnemy(enemyInstance);
        }
    }

    private static void DetectWhichTowerHitEnemy(GameObject enemyInstance)
    {
        GameObject[] tower = GameObject.FindGameObjectsWithTag("Tower");
        for (int i = 0; i < tower.Length; i++)
        {
            TowerTargetLocator towerTargetLocator = tower[i].GetComponent<TowerTargetLocator>();

            if (towerTargetLocator.towerTag == "Bullet")
            {
                towerTargetLocator.RemoveLockOnEnemy(enemyInstance);
            }
            else if (towerTargetLocator.towerTag == "Arrow")
            {
                towerTargetLocator.RemoveLockOnEnemy(enemyInstance);
            }
            else if (towerTargetLocator.towerTag == "Canon")
            {
                towerTargetLocator.RemoveLockOnEnemy(enemyInstance);
            }
        }
    }

    private void DeactivateEnemy(GameObject enemyInstance)
    {
        enemyInstance.SetActive(false);
        EnemyObjectPool.Instance.EnqueueEnemy(enemyInstance);
        Bank.Instance.Deposit(enemyHitPoints);
    }

    public void ReduceHealth(GameObject enemyInstance, int healthHit)
    {
        UpdateEnemyHealth(healthHit);
        CheckEnemyHealth(enemyInstance);
    }

    void UpdateEnemyHealth(int healthHit)
    {
        currentHealth -= healthHit;
        enemyHealthBar.UpdateEnemyHealthBar(currentHealth);
    }
}
