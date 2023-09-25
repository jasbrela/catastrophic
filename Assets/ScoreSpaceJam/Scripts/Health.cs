using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ScoreSpaceJam.Scripts
{
    public class Health : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private float initialMaxHealth;

        [Header("Interface")]
        [Tooltip("Optional")][SerializeField] private Image healthBackground;

        [Header("Invulnerability")]
        [SerializeField] private Collider2D col2D;
        [SerializeField] private Animator animator;
        [SerializeField] private float invulnerabilityDuration;

        [Header("Events")]
        public UnityEvent onHit;
        public UnityEvent onDeath;

        public float MaxHealth => _maxHealth;

        private bool _isInvulnerable = false;
        private float _maxHealth;
        private float _currentHealth;

        private void Start()
        {
            _maxHealth = initialMaxHealth;
            _currentHealth = initialMaxHealth;
            UpdateUI();
        }

        public void Heal(float amount)
        {
            _currentHealth += amount;

            if (_currentHealth >= _maxHealth)
                _currentHealth = _maxHealth;

            UpdateUI();
        }

        public void Damage(float amount)
        {
            if (_isInvulnerable) return;

            _currentHealth -= amount;
            if (col2D != null && invulnerabilityDuration > 0f) StartCoroutine(BecomeInvulnerable());

            onHit?.Invoke();

            if (_currentHealth <= 0)
                Die();

            UpdateUI();
        }

        private void UpdateUI()
        {
            if (healthBackground == null) return;

            if (_maxHealth == 0)
            {
                Debug.Log("Failed to update the UI: Division by 0.");
                return;
            }

            healthBackground.fillAmount = _currentHealth / _maxHealth;
        }

        private void Die()
        {
            onDeath?.Invoke();

            _currentHealth = 0;
        }

        private IEnumerator BecomeInvulnerable()
        {
            if (animator != null) animator.SetBool("Invulnerable", true);
            col2D.enabled = false;
            _isInvulnerable = true;

            yield return new WaitForSecondsRealtime(invulnerabilityDuration);

            _isInvulnerable = false;
            if (animator != null) animator.SetBool("Invulnerable", false);
            col2D.enabled = true;
        }
    }
}
