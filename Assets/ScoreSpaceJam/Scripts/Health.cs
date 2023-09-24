using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ScoreSpaceJam.Scripts
{
    public class Health : MonoBehaviour
    {
        public UnityEvent onHit;
        public UnityEvent onDeath;
        [SerializeField] private Image healthBackground;
        [SerializeField] private float initialMaxHealth;
        private float maxHealth;
        private float currentHealth;

        private void Start()
        {
            maxHealth = initialMaxHealth;
            currentHealth = initialMaxHealth;
            UpdateUI();
        }

        public void Heal(float amount)
        {
            currentHealth += amount;

            if (currentHealth >= maxHealth)
                currentHealth = maxHealth;

            UpdateUI();
        }

        public void Damage(float amount)
        {
            currentHealth -= amount;

            onHit?.Invoke();

            if (currentHealth <= 0)
                Die();

            UpdateUI();
        }

        private void UpdateUI()
        {
            if (healthBackground == null) return;

            if (maxHealth == 0)
            {
                Debug.Log("Failed to update the UI: Division by 0.");
                return;
            }

            healthBackground.fillAmount = currentHealth / maxHealth;
        }

        private void Die()
        {
            onDeath?.Invoke();

            currentHealth = 0;
        }
    }
}
