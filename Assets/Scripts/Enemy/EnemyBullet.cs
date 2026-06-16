using UnityEngine;

/// <summary>Damages the player on collision and destroys itself(bullet) after impact.</summary>

public class EnemyBullet : MonoBehaviour
{

    [SerializeField] private EnemyData enemyData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wall"))
        {
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Player"))
        {
            PlayerHealth health =
                other.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(enemyData.attackDamage);
            }

            Destroy(gameObject);
        }
    }
}