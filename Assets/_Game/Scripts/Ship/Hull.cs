using UnityEngine;

namespace Ship
{
    public class Hull : MonoBehaviour
    {
        public int asteroidDamage = 1;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (string.Equals(other.gameObject.tag, "Asteroid"))
            {
                GetComponent<Health>().TakeDamage(asteroidDamage);
            }
        }
    }
}
