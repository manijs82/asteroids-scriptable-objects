using System;
using DefaultNamespace.ScriptableEvents;
using Ship;
using TMPro;
using UnityEngine;
using Variables;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [Header("Health:")] 
        [SerializeField] private Health health;
        [SerializeField] private TextMeshProUGUI _healthText;
        
        [Header("Score:")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        [Header("Timer:")]
        [SerializeField] private TextMeshProUGUI _timerText;
        
        [Header("Laser:")]
        [SerializeField] private TextMeshProUGUI _laserText;
        
        private void Start()
        {
            if (health == null)
                health = FindObjectOfType<Health>();
            SetHealthText($"Health: {health.initialHealth}");
            health.OnTakeDamage += OnTakeDamage;
        }

        private void OnTakeDamage(int currentHealth)
        {
            SetHealthText($"Health: {currentHealth}");
            if(currentHealth <= 0)
                SetHealthText("Dead");
        }

        private void SetHealthText(string text)
        {
            _healthText.text = text;
        }
        
        private void SetScoreText(string text)
        {
            _scoreText.text = text;
        }
        
        private void SetTimerText(string text)
        {
            _timerText.text = text;
        }
        
        private void SetLaserText(string text)
        {
            _laserText.text = text;
        }
    }
}
