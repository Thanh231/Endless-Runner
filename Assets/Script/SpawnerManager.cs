using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    public GameObject smallObstaclePrefabs;
    public GameObject doubleObstaclePrefabs;
    public GameObject bigObstaclePrefabs;
    public GameObject sandLanePrefabs;
    public GameObject rockLanePrefabs;
    private Stack<GameObject> smallObstaclePool = new Stack<GameObject>();
    private Stack<GameObject> doubleObstaclePool = new Stack<GameObject>();
    private Stack<GameObject> bigObstaclePool = new Stack<GameObject>();
    private Stack<GameObject> sandLanePool = new Stack<GameObject>();
    private Stack<GameObject> rockLanePool = new Stack<GameObject>();
    private Vector3 lastSpawnPosition = Vector3.zero;
    private Vector3 defaultSpawnPosition = new Vector3(0, -10f, 0);
    public float spawnZ = 15f;
    public float spawnObstacleRate = 1f;
    public float spawnLaneRate = 2f;
    private float timerSpawnObstacle = 0f;
    private float timerSpawnLane = 0f;
    private int numberOfObstacle = 3;
    private int amountObstacle = 10;
    private int numberOfLane = 3;
    public Vector3 firstLanePos;
    private Vector3 lastLanePos;
    public bool isLevelUp = false;

    private void Awake()
    {
        InitPool(smallObstaclePool, smallObstaclePrefabs, amountObstacle);
        InitPool(doubleObstaclePool, doubleObstaclePrefabs, amountObstacle);
        InitPool(bigObstaclePool, bigObstaclePrefabs, amountObstacle);
        // for (int i = 0; i < numberOfObstacle; i++)
        // {

        // }
        GameObject sandLane = new GameObject("sandLane");
        sandLane.transform.position = Vector3.zero;
        GameObject rockLane = new GameObject("rockLane");
        rockLane.transform.position = Vector3.zero;
        for (int i = 0; i < numberOfLane; i++)
        {
            sandLanePool.Push(Instantiate(sandLanePrefabs, defaultSpawnPosition, Quaternion.identity, sandLane.transform));
            rockLanePool.Push(Instantiate(rockLanePrefabs, defaultSpawnPosition, Quaternion.identity, rockLane.transform));
        }
        GameObject firstLane = sandLanePool.Pop();
        firstLane.transform.position = firstLanePos;
        // GameObject firstLane = Instantiate(sandLanePrefabs);
        // firstLane.transform.position = firstLanePos;
        lastLanePos = firstLanePos;

    }

    void Update()
    {
        timerSpawnObstacle += Time.deltaTime;
        timerSpawnLane += Time.deltaTime;
        if (timerSpawnObstacle >= spawnObstacleRate)
        {
            SpawnObstacle();
            timerSpawnObstacle = 0f;
        }
        else if (timerSpawnLane >= spawnLaneRate)
        {
            SpawnLane();
            timerSpawnLane = 0f;
            
        }
    }

    void InitPool(Stack<GameObject> pool, GameObject prefab, int amount)
    {
        GameObject obstacleParent = new GameObject("ParentObstacle 1");
        obstacleParent.transform.position = Vector3.zero;
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(prefab, defaultSpawnPosition, Quaternion.identity, obstacleParent.transform);
            obj.SetActive(false);
            pool.Push(obj);
        }
    }   

    void SpawnLane()
    {
        GameObject lane;
        if (!isLevelUp)
        {
            if (sandLanePool.Count > 0)
            {
                lane = sandLanePool.Pop();
                lastLanePos.z += 97.1593f;
                lane.transform.position = new Vector3(firstLanePos.x, firstLanePos.y, lastLanePos.z);
            }
        }
        else
        {
            if (rockLanePool.Count > 0)
            {
                lane = rockLanePool.Pop();
                lastLanePos.z += 97.1593f;
                lane.transform.position = new Vector3(firstLanePos.x, firstLanePos.y, lastLanePos.z);
            }
        }
        // GameObject lane = Instantiate(sandLanePrefabs);

    }
    void SpawnObstacle()
    {
        int obstacleIndex = Random.Range(0, 3);

        switch (obstacleIndex)
        {
            case 0:
                SpawnObstacleFromPool(smallObstaclePool,
                new Vector3(Random.Range(-1, 2) , 0, lastSpawnPosition.z + spawnZ));
                break;
            case 1:
                SpawnObstacleFromPool(doubleObstaclePool,
                new Vector3(Random.Range(0, 2) == 0 ? 0.6f: -0.6f, 0, lastSpawnPosition.z + spawnZ));
                break;
            default:
                SpawnObstacleFromPool(bigObstaclePool, new Vector3(0, 0, lastSpawnPosition.z + spawnZ));
                break;
        }
    }

    private void SpawnObstacleFromPool(Stack<GameObject> pool, Vector3 position)
    {
        if (pool.Count == 0) return;
        lastSpawnPosition = position;
        GameObject obstacle = pool.Pop();
        Obstacle script = obstacle.GetComponent<Obstacle>();
        script.SetHealth();
        obstacle.transform.position = position;
        obstacle.SetActive(true);
    }

    public void AddStack(GameObject obstacle)
    {

        switch (obstacle.tag)
        {
            case "smallObstacle":
                obstacle.transform.position = defaultSpawnPosition;
                smallObstaclePool.Push(obstacle);
                break;
            case "doubleObstacle":
                obstacle.transform.position = defaultSpawnPosition;
                doubleObstaclePool.Push(obstacle);
                break;
            case "bigObstacle":
                obstacle.transform.position = defaultSpawnPosition;
                bigObstaclePool.Push(obstacle);
                break;
            case "sandLane":
                GameObject parent = obstacle.gameObject.transform.parent.gameObject;
                parent.transform.position = defaultSpawnPosition;
                sandLanePool.Push(parent);
                break;
            case "rockLane":
                GameObject gameObject = obstacle.gameObject.transform.parent.gameObject;
                gameObject.transform.position = defaultSpawnPosition;
                rockLanePool.Push(gameObject);
                break;
            default:
                Debug.LogWarning($"Unrecognized obstacle tag: {obstacle.tag}");
                break;
        }
    }
}
