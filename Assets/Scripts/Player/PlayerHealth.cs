using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>Manages player health, damage, death, and respawn behavior.</summary>

public class PlayerHealth : MonoBehaviour
{
    private CameraFollow cameraFollow;

    [SerializeField]
    private PlayerData playerData;

    private float health;

    private Vector3 respawnPosition;

    private PlayerMovement movement;
    private PlayerShooter shooter;


    [SerializeField] private TMP_Text textMesh;

    private void Start()
    {
        health = playerData.maxHealth;

        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        respawnPosition = transform.position;

        movement = GetComponent<PlayerMovement>();
        shooter = GetComponent<PlayerShooter>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        textMesh.text = $"Player Health: {health} / {playerData.maxHealth}";

        if (cameraFollow != null)
        {
            cameraFollow.Shake(0.15f, 0.12f);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("PLAYER DEA");

        if (movement != null)
            movement.enabled = false;

        if (shooter != null)
            shooter.enabled = false;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr != null)
            sr.enabled = false;

        Collider2D col = GetComponent<Collider2D>();

        if (col != null)
            col.enabled = false;

        if (cameraFollow != null)
        {
            cameraFollow.Shake(2f, 0.3f);
        }

        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(3f);

        Respawn();
    }

    private void Respawn()
    {
        Debug.Log("PLAYER RESPAWNED");

        transform.position = respawnPosition;

        health = playerData.maxHealth;

        if (movement != null)
            movement.enabled = true;

        if (shooter != null)
            shooter.enabled = true;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr != null)
            sr.enabled = true;

        Collider2D col = GetComponent<Collider2D>();

        if (col != null)
            col.enabled = true;
    }
}