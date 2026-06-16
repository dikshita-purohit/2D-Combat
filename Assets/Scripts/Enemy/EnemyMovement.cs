using UnityEngine;

/// <summary> Controls enemy movement </summary>

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private EnemyData enemyData;

    [Header("Movement")]
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Components")]
    [SerializeField] private Animator animator;

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 8f;

    private Rigidbody2D rb;
    private Transform player;


    private Vector2 wanderDirection;
    private Vector2 facingDirection = Vector2.down;

    private float directionTimer;
    private float attackTimer;

    private enum State
    {
        Wander,
        Chase
    }

    private State currentState = State.Wander;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        PickNewDirection();
    }

    private void Update()
    {
        if (player == null)
        {
            currentState = State.Wander;
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        currentState = distance <= enemyData.detectionRange ? State.Chase : State.Wander;
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Wander:
                Wander();
                break;

            case State.Chase:
                Chase();
                break;
        }

        rb.rotation = 0f;
    }

    private void Wander()
    {
        directionTimer -= Time.fixedDeltaTime;

        if (directionTimer <= 0f)
        {
            PickNewDirection();
        }

        Move(wanderDirection);
    }

    private void Chase()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= enemyData.attackRange)
        {
            rb.velocity = Vector2.zero;

            animator.SetBool("isWalking", false);
          //  AttackPlayer();
            return;
        }

        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
        Move(direction);
    }

    private Vector2 lastDirection = Vector2.down;

    private void Move(Vector2 direction)
    {
        Vector2 rayOrigin = (Vector2)transform.position + direction * 0.5f;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, 0.5f, obstacleLayer);

        if (hit.collider != null)
        {
            PickNewDirection();
            return;
        }

        rb.velocity = direction * enemyData.moveSpeed;

        bool isWalking = direction != Vector2.zero;

        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            lastDirection = direction.normalized;

            animator.SetFloat("inputx", direction.x);

            animator.SetFloat("inputy", direction.y);

            animator.SetFloat("lastInputX", direction.x);

            animator.SetFloat("lastInputY", direction.y);
        }
    }

    private void PickNewDirection()
    {
        wanderDirection = Random.insideUnitCircle.normalized;

        directionTimer = Random.Range(enemyData.directionChangeMinTime, enemyData.directionChangeMaxTime);
    }

    public void FireBullet()
    {
        Shoot();
    }

    private void Shoot()
    {
        Vector2 direction =
            ((Vector2)player.position -
             (Vector2)firePoint.position).normalized;

        float angle =
            Mathf.Atan2(direction.y, direction.x) *
            Mathf.Rad2Deg - 90f;

        GameObject bullet =
            Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.Euler(0f, 0f, angle));

        Rigidbody2D bulletRb =
            bullet.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            bulletRb.velocity =
                direction * bulletSpeed;
        }

        EnemyBulletCollision collision =
            bullet.AddComponent<EnemyBulletCollision>();

        collision.damage =
            enemyData.attackDamage;

        Destroy(bullet, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            PickNewDirection();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (enemyData == null)
            return;

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, enemyData.detectionRange);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, enemyData.attackRange);
    }
}

public class EnemyBulletCollision : MonoBehaviour
{
    public float damage;

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
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}