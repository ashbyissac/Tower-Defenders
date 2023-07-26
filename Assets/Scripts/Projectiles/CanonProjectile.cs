using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonProjectile : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayerMask;
    [SerializeField] ParticleSystem hitEffectFX;

    [SerializeField] float moveSpeed;
    [SerializeField] int hitDamageFactor;
    public int HitDamageFactor { get { return hitDamageFactor; } set { hitDamageFactor = value; } }
    [SerializeField] float canonProjectileDamageRadius = 8f;
    [SerializeField] float projectileGravity = -9.81f;
    [SerializeField] float gravityFactor;

    public static CanonProjectile Instance = null;

    void Awake()
    {
        Instance = this;
    }

    public void ShootProjectile(Transform startPoint)
    {
        GetComponent<Rigidbody>().velocity = startPoint.up * moveSpeed;
        //GetComponent<Rigidbody>().AddForce(startPoint.up * moveSpeed, ForceMode.Impulse);
        Physics.gravity = new Vector3(0, projectileGravity * gravityFactor, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitEffectFX, transform.position, Quaternion.identity);
        if (collision.gameObject.tag == "Enemy") 
        {
            GameObject enemyInstance = collision.gameObject;
            EnemyHealth enemyHealth = enemyInstance.GetComponent<EnemyHealth>();
            int enemyHitPoints = enemyHealth.GetCurrentEnemyHealth(); 
            enemyHealth.ReduceHealth(enemyInstance, enemyHitPoints);
            EnqueueProjectile();
            DisableProjectile();
        }
        else if (collision.gameObject.tag == "Ground")
        {
            ProjectileBasedEnemyDetection();
            EnqueueProjectile();
            DisableProjectile();
        }
    }

    void ProjectileBasedEnemyDetection()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, canonProjectileDamageRadius, enemyLayerMask);
        EnemyHealth enemyHealth;
        int hitPoints = 0;

        foreach (Collider enemyCollider in enemyColliders)
        {
            float distanceToEnemyInstance = Vector3.Distance(transform.position, enemyCollider.transform.position);

            if (distanceToEnemyInstance < canonProjectileDamageRadius / 3) 
            {
                hitPoints = 10 + hitDamageFactor; 
            }
            else if (distanceToEnemyInstance < canonProjectileDamageRadius / 1.5) 
            {
                hitPoints = 5 + hitDamageFactor;
            }
            else if (distanceToEnemyInstance < canonProjectileDamageRadius) 
            {
                hitPoints = 1 + hitDamageFactor;
            }

            enemyHealth = enemyCollider.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.ReduceHealth(enemyCollider.gameObject, hitPoints);
        }
    }

    void EnqueueProjectile()
    {
        CanonProjectilePool.Instance.EnqueueProjectile(CanonProjectilePool.Instance.canonPoolQueue, this.gameObject);
    }
    
    void DisableProjectile()
    {
        gameObject.SetActive(false);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    //public void SetHitDamageFactor(int damageFactor)
    //{
    //    hitDamageFactor += damageFactor;
    //}
}
