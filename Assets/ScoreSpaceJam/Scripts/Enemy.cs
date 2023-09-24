using ScoreSpaceJam.Scripts.Managers;
using ScoreSpaceJam.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace ScoreSpaceJam.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private float speed;
        private Transform _player;

        public GameManager gameManager;

        private void Start()
        {
            _player = FindObjectOfType<PlayerMovement>().transform;
            health.onDeath.AddListener(Disable);
        }

        public void RegisterOnDeathEvent(UnityAction call)
        {
            health.onDeath.AddListener(call);
        }

        public void Disable()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            if (_player == null) return;
            if (gameManager == null || gameManager.CurrentState != GameState.PLAYING) return;
            transform.position = Vector3.MoveTowards(transform.position, _player.position, speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                health.Damage(bullet.Damage);
                bullet.OnHit();
            }
        }
    }
}
