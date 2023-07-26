using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static WaypointManager;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveData> waveDatas;
    [SerializeField] float nextLevelLoadDelay = 5f;

    public static EnemySpawner Instance = null;

    EnemyObjectPool enemyObjectPoolInstance;
    Transform firstWaypoint;

    bool isEnemiesSpawned = false;

    bool isCurrentLevelOver = false;
    public bool IsCurrentLevelOver { get { return isCurrentLevelOver; } set { isCurrentLevelOver = value; } }

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
    }

    void Start()
    {
        StartEnemyMovement();
    }

    void Update()
    {
        if (isEnemiesSpawned && FindObjectsOfType<EnemyMover>().Length == 0)
        {
            isCurrentLevelOver = true;
        }

    }

    public void StartEnemyMovement()
    {
        firstWaypoint = WaypointManager.Instance.GetFirstWaypoint();
        enemyObjectPoolInstance = EnemyObjectPool.Instance;
        StartCoroutine(SpawnEnemies());
    }

    /*
     *  Enables the enemy that is present in the pool 
     *  based up on the waveDatas list.
    */
    IEnumerator SpawnEnemies()
    {
        int wavesControllerIndex = 0; /* particular waveData element */
        int currentWaveNumber = 0;

        yield return new WaitForSeconds(2f);
        GameManager.Instance.DisplayWaveSignalText(true);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.DisplayWaveSignalText(false);

        while (true)
        {
     
            if (wavesControllerIndex > waveDatas.Count - 1) { break; }

            if (currentWaveNumber < waveDatas[wavesControllerIndex].numberOfWavesInCurrentWave)
            {
                for (int j = 0; j < waveDatas[wavesControllerIndex].totalNumberOfEnemies; j++)
                {
                    enemyObjectPoolInstance.EnableEnemyInPool(firstWaypoint);
                    yield return new WaitForSeconds(waveDatas[wavesControllerIndex].enemySpawnDelay); // variable
                }
                yield return new WaitForSeconds(waveDatas[wavesControllerIndex].wavesDelay); // variable
                currentWaveNumber++;
            }
            else if (currentWaveNumber == waveDatas[wavesControllerIndex].numberOfWavesInCurrentWave)
            {
                wavesControllerIndex++;
                currentWaveNumber = 0;
                if (wavesControllerIndex != waveDatas.Count)
                {
                    GameManager.Instance.DisplayWaveSignalText(true);
                    yield return new WaitForSeconds(waveDatas[wavesControllerIndex].incomingNextWavesDelay); // variable
                    GameManager.Instance.DisplayWaveSignalText(false);
                }
                else
                {
                    yield return new WaitForSeconds(nextLevelLoadDelay);
                }
            }
        }
        isEnemiesSpawned = true;
    }
}
