using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Configuration")]


    [Header("Stats")]
    [Range(1, 1000)]
    public float maxHealth = 100f;

    [Range(1, 20)]
    public float moveSpeed = 3f;

    [Header("Detection")]
    [Range(0, 100)]
    public float detectionRange = 5f;

    [Range(0, 100)]
    public float attackRange = 1f;

    [Header("Attack")]
    [Range(1, 100)]
    public float attackDamage = 10f;

    [Range(0, 100)]
    public float attackCooldown = 1f;

    [Header("Wander")]
    [Range(0, 100)]
    public float directionChangeMinTime = 2f;

    [Range(0, 100)]
    public float directionChangeMaxTime = 4f;

    [Header("Population")]
    [Range(1, 100)]
    public int maxEnemiesInScene = 20;
}