using UnityEngine;

/// <summary>Handles projectile firing, cooldown management, and projectile initialization.</summary>

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
    private ProjectileConfig projectileConfig;

    [SerializeField]
    private ProjectilePool projectilePool;

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private Transform firePoint;

    public Animator animator;

    private float lastFireTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (Time.time < lastFireTime + projectileConfig.cooldown)
        {
            return;
        }

        lastFireTime = Time.time;

        Projectile projectile =projectilePool.Get();

        //projectile.transform.SetParent(null,true);
        projectile.transform.position = firePoint.position;

        Vector3 shootDirection = new Vector3(
            animator.GetFloat("lastInputX"),
            animator.GetFloat("lastInputY"),
            0f
        );

        projectile.Initialize(projectileConfig, shootDirection, projectilePool);
    }
}