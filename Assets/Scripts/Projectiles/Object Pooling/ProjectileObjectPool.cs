using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public int poolSize;
        public GameObject prefab;
    }

    protected Transform projectileObjectPoolParent;

    public Queue<GameObject> bulletPoolQueue = new Queue<GameObject>(); 
    public Queue<GameObject> arrowPoolQueue = new Queue<GameObject>(); 
    public Queue<GameObject> canonPoolQueue = new Queue<GameObject>();

    protected abstract void PopulateProjectilePool();
    public abstract GameObject EnableProjectileInPool(Transform startPoint);

    public void EnqueueProjectile(Queue<GameObject> projectilePoolQueue, GameObject projectileInstance)
    {
        projectilePoolQueue.Enqueue(projectileInstance);
    }

    public GameObject DequeueProjectile(Queue<GameObject> projectilePoolQueue)
    {
        return projectilePoolQueue.Dequeue();
    }
    
    protected void SetProjectile(GameObject activeProjectile, Transform startPoint)
    {
        activeProjectile.GetComponent<Rigidbody>().isKinematic = false;
        SetProjectilePositionAndRotation(activeProjectile, startPoint);
        activeProjectile.SetActive(true);
    }

    protected void SetProjectilePositionAndRotation(GameObject activeProjectile, Transform startPoint)
    {
        activeProjectile.transform.position = startPoint.position;
        activeProjectile.transform.rotation = startPoint.rotation;
    }
}