using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Player Configuration")]

    [Header("Health")]
    [Range(1, 1000)]
    public float maxHealth = 100f;

    [Header("Movement")]
    [Range(1, 20)]
    public float moveSpeed = 5f;

}