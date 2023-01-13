using Asteroids;
using Ship;
using UnityEngine;

public class DifficultyApplier : MonoBehaviour
{
     public DifficultyPreset defaultPreset;
     [Space] [SerializeField] private Health health;
     [SerializeField] private Hull hull;
     [SerializeField] private AsteroidSpawner spawner;
     
     private void Awake()
     {
          if(defaultPreset == null) return;
          var scalars = defaultPreset.scalars;
          
          health.initialHealth = scalars.health;
          hull.asteroidDamage = scalars.damageOfAsteroids;
          
          spawner._minSpawnTime = scalars.spawnRateRange.x;
          spawner._maxSpawnTime = scalars.spawnRateRange.y;
          spawner._minAmount = scalars.spawnAmountRange.x;
          spawner._maxAmount = scalars.spawnAmountRange.y;
     }
}