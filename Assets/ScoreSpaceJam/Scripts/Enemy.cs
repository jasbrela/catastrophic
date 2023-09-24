using ScoreSpaceJam.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace ScoreSpaceJam.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private float speed;
        private Transform _player;
        private Transform _base;

        public GameManager gameManager;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _base = GameObject.FindWithTag("Base").transform;
            
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
            transform.position = Vector3.MoveTowards(transform.position, GetClosestTarget(), speed * Time.deltaTime);
        }

        private Vector3 GetClosestTarget()
        {
            var currentPos = transform.position;
            
            if (Vector3.Distance(currentPos, _player.position) >
                Vector3.Distance(currentPos, _base.position)) return _base.position;
            return _player.position;
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
