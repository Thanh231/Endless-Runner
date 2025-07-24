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
    public float spawnRate = 2f;
    private float timer = 0f;
    private int numberOfObstacle = 10;
    private int numberOfLane = 3;
    public Vector3 firstLanePos;
    public Vector3 lastLanePos;

    private void Awake()
    {
        for (int i = 0; i < numberOfObstacle; i++)
        {
            InitPool(smallObstaclePool, smallObstaclePrefabs, numberOfObstacle);
            InitPool(doubleObstaclePool, doubleObstaclePrefabs, numberOfObstacle);
            InitPool(bigObstaclePool, bigObstaclePrefabs, numberOfObstacle);
        }
        // for (int i = 0; i < numberOfLane; i++)
        // {
        //     sandLanePool.Push(Instantiate(sandLanePrefabs, defaultSpawnPosition, Quaternion.identity));
        //     rockLanePool.Push(Instantiate(rockLanePrefabs, defaultSpawnPosition, Quaternion.identity));
        // }
        // GameObject firstLane = sandLanePool.Pop();
        // firstLane.transform.position = firstLanePos;
        GameObject firstLane = Instantiate(sandLanePrefabs);
        firstLane.transform.position = firstLanePos;
        lastLanePos = firstLanePos;

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            // if (sandLanePool.Count > 0)
            // {
            //     SpawnLane();
            // }
            SpawnLane();
            SpawnObstacle();
            timer = 0f;
        }
    }

    void InitPool(Stack<GameObject> pool, GameObject prefab, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(prefab, defaultSpawnPosition, Quaternion.identity);
            obj.SetActive(false);
            pool.Push(obj);
        }
    }   

    void SpawnLane()
    {
        // GameObject lane = sandLanePool.Pop();
        GameObject lane = Instantiate(sandLanePrefabs);
        lastLanePos.z += 97.1593f;
        lane.transform.position = new Vector3(firstLanePos.x, firstLanePos.y, lastLanePos.z);
    }
    void SpawnObstacle()
    {
        int obstacleIndex = Random.Range(0, 3);

        switch (obstacleIndex)
        {
            case 0:
                SpawnObstacleFromPool(smallObstaclePool,
                new Vector3(Random.Range(-1, 2) * 2, 0, lastSpawnPosition.z + spawnZ));
                break;
            case 1:
                SpawnObstacleFromPool(doubleObstaclePool,
                new Vector3(Random.Range(0, 2) == 0 ? 1 : -1, 0, lastSpawnPosition.z + spawnZ));
                break;
            default:
                SpawnObstacleFromPool(bigObstaclePool, new Vector3(0, 0, lastSpawnPosition.z + spawnZ));
                break;
        }
    }

    private void SpawnObstacleFromPool(Stack<GameObject> pool, Vector3 position)
    {
        lastSpawnPosition = position;
        if (pool.Count == 0) return;
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
            // case "sandLane":
            //     obstacle.transform.position = defaultSpawnPosition;
            //     sandLanePool.Push(obstacle);
            //     break;
            // case "rockLane":
            //     obstacle.transform.position = defaultSpawnPosition;
            //     rockLanePool.Push(obstacle);
            //     break;
            default:
                Debug.LogWarning($"Unrecognized obstacle tag: {obstacle.tag}");
                break;
        }
    }
}
