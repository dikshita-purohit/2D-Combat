using System.Collections.Generic;
using UnityEngine;

/// <summary>Manages reusable projectile instances to reduce runtime allocations.</summary>

public class ProjectilePool : MonoBehaviour
{
    [SerializeField]
    private Projectile projectilePrefab;

    [SerializeField]
    private int initialSize = 20;

    private Queue<Projectile> pool = new();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateProjectile();
        }
    }

    private void CreateProjectile()
    {
        Projectile p = Instantiate(projectilePrefab, transform);

        p.gameObject.SetActive(false);

        pool.Enqueue(p);
    }

    public Projectile Get()
    {
        if (pool.Count == 0)
        {
            CreateProjectile();
        }

        Projectile p = pool.Dequeue();

        p.gameObject.SetActive(true);

        return p;
    }

    public void Return(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);

        pool.Enqueue(projectile);
    }
}