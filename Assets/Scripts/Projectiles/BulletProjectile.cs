using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 25f;
    [SerializeField] int hitPoints;
    public int HitPoints { get { return hitPoints; } set { hitPoints = value; } }

    public static BulletProjectile Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ShootProjectile(Transform startPoint) 
    {
        GetComponent<Rigidbody>().velocity = startPoint.forward * moveSpeed;
        //GetComponent<Rigidbody>().AddForce(startPoint.forward * moveSpeed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        EnqueueProjectile();
        DisableProjectile();
    }

    void DisableProjectile()
    {
        gameObject.SetActive(false);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void EnqueueProjectile()
    {
        BulletProjectilePool.Instance.EnqueueProjectile(BulletProjectilePool.Instance.bulletPoolQueue, this.gameObject);
    }
}
