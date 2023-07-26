using System;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Transform currentWaypoint;

    [SerializeField] int enemyWithdrawAmount;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float slowSpeedFactor;
    public float SlowSpeedFactor { get { return slowSpeedFactor; } }

    float lerpTime = 0f;
    GameObject waypointManagerObj;
    Transform nextWaypoint;
    float enemySpeed;

    void Awake()
    {
        enemySpeed = moveSpeed;
    }

    /*
     * Resets enemy speed and sets waypoint 
     */
    void OnEnable()
    {
        waypointManagerObj = GameObject.FindGameObjectWithTag("WaypointManager");
        moveSpeed = enemySpeed; 
        lerpTime = 0f;
        currentWaypoint = waypointManagerObj.GetComponent<WaypointManager>().GetNextWaypoint(null);
    }

    void Update() 
    {
        Movement();
    }

    /*
     *  Using lerp function enemy moves from current waypoint 
     *  to the next waypoint
     *  
     *  When enemy reaches the next waypoint, the waypoint 
     *  gets updated and lerpTime is resetted
     */
    void Movement()
    {
        if (lerpTime < 1f && currentWaypoint != WaypointManager.Instance.GetEndWaypoint())
        {
            lerpTime += Time.deltaTime * moveSpeed;
            nextWaypoint = WaypointManager.Instance.GetNextWaypoint(currentWaypoint);
            transform.LookAt(nextWaypoint);
            transform.position = Vector3.Lerp(currentWaypoint.position, nextWaypoint.position, lerpTime);
        }
        else
        {
            lerpTime = 0f;
            UpdateWaypoint();
            FinishPath();
        }
    }

    void UpdateWaypoint()
    {
        currentWaypoint = WaypointManager.Instance.GetNextWaypoint(currentWaypoint);
    }

    /*
     * When Enemy reaches the end waypoint : 
     *  -> Instance is being enqueued to pool
     *  -> Position is resetted and coins are 
     *     reduced from the currency system
    */
    void FinishPath()
    {
        if (WaypointManager.Instance.IsReachedEnd)
        {
            WaypointManager.Instance.IsReachedEnd = false;

            GameObject currentInstance = this.gameObject;
            currentInstance.SetActive(false);
            EnemyObjectPool.Instance.EnqueueEnemy(currentInstance);

            transform.position = WaypointManager.Instance.GetFirstWaypoint().position;
            Bank.Instance.Withdraw(enemyWithdrawAmount);
        }
    }

    public float GetEnemyMoveSpeed()
    {
        return enemySpeed;
    }

    public void SetEnemyMoveSpeed(float enemySpeedFactor)
    {
        moveSpeed = enemySpeedFactor;
    }
}
