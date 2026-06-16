using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectile System/Projectile Config")]
public class ProjectileConfig : ScriptableObject
{
    [Header("Projectile Configuration")]

    [Header("Combat")]

    [Range(1, 100)]
    public float damage = 10;

    [Range(1, 20)]
    public float speed = 10;

    [Range(1, 50)]
    public float range = 10;

    [Range(0.1f, 10)]
    public float cooldown = 0.5f;

    [Range(0, 10)]
    public int pierceCount = 0;

    [Range(0, 20)]
    public float knockback = 0;

    [Header("Visual")]

    public Color color = Color.yellow;

    [Range(0.1f, 5)]
    public float scale = 1f;
}