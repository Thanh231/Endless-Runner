using UnityEngine;

public class CollectionObstacleSystem : MonoBehaviour
{
    public int obstacleLayer;
    public int laneLayer;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == obstacleLayer || other.gameObject.layer == laneLayer)
        {
            SpawnerManager.Ins.AddStack(other.gameObject);
        }
    }
}
