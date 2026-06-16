# 2D-Combat
A Unity 2D top-down gameplay project. The project demonstrates a modular, data-driven projectile system with runtime configurable projectile behavior, enemy interaction, and object pooling

## Features

### Projectile System

* Fire projectiles using the Spacebar.
* Configurable projectile parameters through the Unity Inspector.
* Runtime editing support for projectile properties.
* Projectile collision with enemies and walls.
* Knockback support.
* Pierce support.
* Projectile animations.
* Object pooling for improved performance.

### Configurable Parameters

#### Combat

* Damage
* Speed
* Range
* Cooldown
* Pierce Count
* Knockback

#### Visual

* Color
* Scale

### Enemy System

* Health and damage system.
* Enemy death handling.
* Enemy spawning.
* Wander and chase behavior.
* Enemy ranged attacks.

### Player System

* Top-down movement.
* Directional animations.
* Projectile firing.
* Health system.
* Death and respawn mechanics.

### Additional Features

* ScriptableObject-based configuration.
* Interface-based damage system (`IDamageable`).
* Camera shake feedback.
* Data-driven architecture.

---

## Project Structure

```text
Scripts
│
├── ScriptableObject
│   ├── PlayerData
│   ├── EnemyData
│   └── ProjectileConfig
│
├── Player
│   ├── PlayerMovement
│   ├── PlayerShooter
│   └── PlayerHealth
│
├── Projectile
│   └── Projectile
|
├── ProjectilePool
│   └── ProjectilePool
│
├── Enemy
│   ├── Enemy
│   ├── EnemyMovement
│   ├── EnemyShooter
│   └── EnemySpawner
│
├── Interfaces
│   └── IDamageable
│
└── Helper
    └── CameraFollow
```

---

## Design Decisions

### ScriptableObjects

Projectile, player, and enemy data are stored using ScriptableObjects. This allows gameplay balancing without modifying code and enables runtime parameter editing.

### Object Pooling

Projectiles are reused through a pooling system instead of being repeatedly instantiated and destroyed. This reduces allocations and improves performance.

### Interface-Based Damage

The `IDamageable` interface allows projectiles to interact with any damageable object while keeping systems loosely coupled.

### Modular Architecture

Gameplay responsibilities are separated into focused components such as movement, shooting, health, and projectile management, improving maintainability and scalability.

---

## Runtime Editing

Projectile properties can be modified during Play Mode through the `ProjectileConfig` asset.

Changes affect newly fired projectiles without restarting the scene.

---

## Controls

| Action          | Key               |
| --------------- | ----------------- |
| Move            | WASD / Arrow Keys |
| Fire Projectile | Spacebar          |

---

## Unity Version
Unity 2022.3.22f1

---

## Future Improvements

* Multiple projectile types
* Additional projectile shapes
* Sound effects
* Visual effects
* UI-based projectile configuration
