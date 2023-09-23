using System;
using ScoreSpaceJam.Scripts.Player;
using UnityEngine;

namespace ScoreSpaceJam.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private float speed;
        private Transform _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerMovement>().transform;
            health.onDeath.AddListener(Disable);
        }

        public void Disable()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            if (_player == null) return;
            transform.position = Vector3.MoveTowards(transform.position, _player.position, speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("trigger");
            if (other.TryGetComponent(out Bullet bullet))
            {
                health.Damage(bullet.Damage);
            }
        }
    }
}
