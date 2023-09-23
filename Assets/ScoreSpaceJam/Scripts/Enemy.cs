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

        private void Disable()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            if (_player == null) return;
            transform.Translate(transform.position - _player.position * speed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                health.Damage(bullet.Damage);
                
                Debug.Log("trigger ENTER bullet");
            }
        }
    }
}
