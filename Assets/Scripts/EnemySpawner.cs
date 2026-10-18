using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnRadius = 10f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomEnemy), 1f, spawnInterval);
    }

    // ABSTRACTION - the spawner doesn't need to know how each enemy works
    private void SpawnRandomEnemy()
    {
        int roll = Random.Range(0, 3);

        switch (roll)
        {
            case 0: SpawnEnemy<Chaser>(PrimitiveType.Cube); break;
            case 1: SpawnEnemy<Shooter>(PrimitiveType.Sphere); break;
            default: SpawnEnemy<Tank>(PrimitiveType.Cylinder); break;
        }
    }

    private void SpawnEnemy<T>(PrimitiveType shape) where T : Enemy
    {
        GameObject obj = GameObject.CreatePrimitive(shape);
        Vector2 point = Random.insideUnitCircle.normalized * spawnRadius;
        obj.transform.position = new Vector3(point.x, 0.5f, point.y);
        obj.AddComponent<T>();
    }
}