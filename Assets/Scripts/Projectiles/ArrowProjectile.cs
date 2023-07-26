using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] int hitPoints;
    public int HitPoints { get { return hitPoints; } set { hitPoints = value; } }

    public static ArrowProjectile Instance = null;

    void Awake()
    {
        Instance = this;
    }

    public void ShootProjectile(Transform startPoint)
    {
        GetComponent<Rigidbody>().velocity = startPoint.forward * moveSpeed;
        //this.GetComponent<Rigidbody>().AddForce(startPoint.forward * moveSpeed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        EnqueueProjectile();
        DisableProjectile();
    }

    void DisableProjectile()
    {
        this.gameObject.SetActive(false);
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    void EnqueueProjectile()
    {
        ArrowProjectilePool.Instance.EnqueueProjectile(ArrowProjectilePool.Instance.arrowPoolQueue, this.gameObject);
    }
}
