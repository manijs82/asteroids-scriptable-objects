using UnityEngine;

[System.Serializable]
public class DifficultyScalars
{
    [Header("Player")]
    public int health;
    public int damageOfAsteroids;
    public bool destroyProjectileOnTouch;
    [Header("Spawner")]
    public Vector2 spawnRateRange;
    public Vector2Int spawnAmountRange;
    [Header("Asteroids")]
    public Vector2 asteroidSizeRange;
    public Vector2 asteroidSpeedRange;
    public Vector2 asteroidRotationSpeedRange;
}