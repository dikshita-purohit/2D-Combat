using UnityEngine;

/// <summary> Handles enemy projectile firing when the player is within attack range. </summary>

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private Transform player;
    private float shootTimer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > enemyData.attackRange)
            return;

        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            shootTimer = enemyData.attackCooldown;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Vector2 dir = (player.position - firePoint.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = dir * 8f;

    }

}