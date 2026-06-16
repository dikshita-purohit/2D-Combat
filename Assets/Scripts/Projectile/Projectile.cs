using UnityEngine;

/// <summary>Controls projectile movement, collision handling, damage, and pooling.</summary>

public class Projectile : MonoBehaviour
{
    private bool hasHit = false;

    private float damage;
    private float speed;
    private float range;
    private float knockback;

    private int pierceRemaining;

    private Vector3 startPosition;
    private Vector3 direction;

    private ProjectilePool pool;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Initialize(
        ProjectileConfig config,
        Vector3 dir,
        ProjectilePool projectilePool)
    {
        hasHit = false;

        GetComponent<Collider2D>().enabled = true;
        damage = config.damage;
        speed = config.speed;
        range = config.range;
        knockback = config.knockback;

        pierceRemaining = config.pierceCount;

        direction = dir.normalized;

        pool = projectilePool;

        startPosition = transform.position;

        spriteRenderer.color = config.color;

        transform.localScale = Vector3.one * config.scale;

        animator.Play("Idle", 0, 0);

        animator.SetFloat("lastInputX", direction.x);
        animator.SetFloat("lastInputY", direction.y);
    }

    private void Update()
    {
        if (hasHit)
            return;

        transform.position += direction * speed * Time.deltaTime;

        float sqrDistance = (transform.position - startPosition).sqrMagnitude;

        if (sqrDistance > range * range)
        {
            pool.Return(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit)
            return;

        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damage);

            if (other.attachedRigidbody)
            {
                other.attachedRigidbody.AddForce(direction * knockback, ForceMode2D.Impulse);
            }

            PlayHitAnimation();

            if (pierceRemaining > 0)
            {
                pierceRemaining--;
                hasHit = false;
            }
        }

        if (other.CompareTag("wall"))
        {
            PlayHitAnimation();
        }
    }

    private void PlayHitAnimation()
    {
        hasHit = true;

        GetComponent<Collider2D>().enabled = false;

        animator.SetTrigger("Hit");
    }

    public void OnHitAnimationFinished()
    {
        pool.Return(this);
    }
}