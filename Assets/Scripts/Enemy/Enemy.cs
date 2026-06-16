using TMPro;
using UnityEngine;

/// <summary> Handles enemy health, damage, and death  behavior.</summary>
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData enemyData;

    private float health;
    public static int EnemiesDead;
    [SerializeField] private TMP_Text textMesh;
    [SerializeField] private TMP_Text healthtextMesh;

    private void Awake()
    {
        health = enemyData.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        //Debug.Log($"Enemy HP: {health}");
        healthtextMesh.text = $" {health} / {enemyData.maxHealth}" ;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        EnemiesDead++;
        textMesh.text = $"Enemies Killed: {Enemy.EnemiesDead}";

        //Debug.Log("Enemies Dead: " + EnemiesDead);
        Destroy(gameObject);
    }
}