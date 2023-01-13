using Ship;
using UnityEngine;

public class DifficultyApplier : MonoBehaviour
{
     public DifficultyPreset defaultPreset;
     [Space] [SerializeField] private Health health;
     [SerializeField] private Hull hull;

     private void Awake()
     {
          if(defaultPreset == null) return;

          var scalars = defaultPreset.scalars;
          health.initialHealth = scalars.health;
          hull.asteroidDamage = scalars.damageOfAsteroids;
     }
}