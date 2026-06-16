using UnityEngine;

/// <summary>Maintains the desired number of enemies by spawning new ones when needed.</summary>

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private GameObject enemyPrefab;

    private void Update()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length < enemyData.maxEnemiesInScene)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 pos = Random.insideUnitCircle * 10f;

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}