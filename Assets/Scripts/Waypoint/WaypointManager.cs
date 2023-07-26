using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [System.Serializable]
    public class WaveData
    {
        public int numberOfWavesInCurrentWave; 
        public int totalNumberOfEnemies;
        public int enemySpawnDelay;
        public int wavesDelay;
        public int incomingNextWavesDelay;
    }

    [SerializeField] List<Transform> waypointsPath = new List<Transform>();
    public static WaypointManager Instance;

    Transform startWaypoint;
    Transform endWaypoint;
    bool isReachedEnd = false;
    public bool IsReachedEnd { get { return isReachedEnd; } set { isReachedEnd = value; } }

    void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this; 
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
        PopulateWaypointsToList();
    }

    void PopulateWaypointsToList()
    {
        foreach (Transform waypoint in transform)
        {
            waypointsPath.Add(waypoint);
        }

        startWaypoint = waypointsPath[0];
        endWaypoint = waypointsPath[waypointsPath.Count - 1];
    }

    public Transform GetFirstWaypoint() { return startWaypoint; }
    public Transform GetEndWaypoint() { return endWaypoint; }

    /*
     * Returns the very next waypoint keeping track of the 
     * current waypoint by passing the current waypoint as 
     * a parameter 
    */
    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        Transform segment = startWaypoint;

        if (currentWaypoint == null) { return segment; }

        int index = waypointsPath.IndexOf(currentWaypoint);
        if (index >= waypointsPath.Count - 1)
        {
            isReachedEnd = true;
            index = -1;
        }

        segment = waypointsPath[index + 1];
        return segment;
    }
}
