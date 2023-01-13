using System;
using UnityEngine;

namespace Ship
{
    public class Health : MonoBehaviour
    {
        public event Action<int> OnTakeDamage;

        public int initialHealth = 10;

        private int currentHealth;

        public int CurrentHealth => currentHealth;

        private void Start()
        {
            currentHealth = initialHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Max(0, currentHealth);
            
            OnTakeDamage?.Invoke(currentHealth);
            if (currentHealth <= 0) Destroy(gameObject);
        }
    }
}
