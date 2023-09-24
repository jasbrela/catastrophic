using ScoreSpaceJam.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace ScoreSpaceJam.Scripts.Entity.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int score;
        [SerializeField] private Health health;
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private bool destroyOnCollide;
        private Transform _player;
        private Transform _base;

        [HideInInspector] public GameManager gameManager;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _base = GameObject.FindWithTag("Base").transform;

            //health.onDeath.AddListener(Disable);
        }

        public void RegisterOnDeathEvent(UnityAction call)
        {
            health.onDeath.AddListener(call);
        }

        public void Disable()
        {
            gameManager.Score(score);
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

        private void TriggerLogic(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                health.Damage(bullet.Damage);
                bullet.OnHit();
            }
            else if (other.gameObject.TryGetComponent(out Health otherHealth))
            {
                otherHealth.Damage(damage);
                if (destroyOnCollide)
                    this.health.Damage(health.MaxHealth);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerLogic(other);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            TriggerLogic(other);
        }
    }
}
