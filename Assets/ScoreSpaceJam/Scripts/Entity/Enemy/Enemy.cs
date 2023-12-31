using System;
using System.Collections;
using System.Collections.Generic;
using ScoreSpaceJam.Scripts.Bullets;
using ScoreSpaceJam.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace ScoreSpaceJam.Scripts.Entity.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [HideInInspector] public GameManager gameManager;
        [SerializeField] private Transform flip;
        [SerializeField] private int currency;
        [SerializeField] private int score;
        [SerializeField] private Health health;
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private bool destroyOnCollide;

        private Vector3 _defaultScale;
        private Coroutine _hitCoroutine;
        protected Transform _player;
        protected Transform _base;
        private readonly List<Collider2D> _inside = new();

        private void Start()
        {
            if (flip != null) _defaultScale = flip.localScale;
            _player = GameObject.FindWithTag("Player").transform;
            _base = GameObject.FindWithTag("Base").transform;
        }

        public void RegisterOnDeathEvent(UnityAction call)
        {
            health.onDeath.AddListener(call);
        }

        public virtual void RegisterGameManager(GameManager gm)
        {
            gameManager = gm;
        }

        public void Disable()
        {
            gameManager.AddCurrency(currency);
            gameManager.Score(score);
            Destroy(gameObject);
        }

        private void Update()
        {
            if (_player == null) return;
            if (gameManager == null || gameManager.CurrentState != GameState.PLAYING) return;
            var targetPos = GetClosestTargetPosition();
            
            if (flip != null) flip.localScale = targetPos.x > transform.position.x ? new Vector3(_defaultScale.x * -1, _defaultScale.y, _defaultScale.z) : _defaultScale;
            
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

        protected virtual Vector3 GetClosestTargetPosition()
        {
            var currentPos = transform.position;

            if (Vector3.Distance(currentPos, _player.position) >
                Vector3.Distance(currentPos, _base.position)) return _base.position;
            return _player.position;
        }

        private void TriggerLogic(Collider2D other)
        {
            if (other.TryGetComponent(out BaseBullet bullet))
            {
                health.Damage(bullet.Damage);
                bullet.OnHit();
            }

            if (other.gameObject.TryGetComponent(out Health otherHealth))
            {
                otherHealth.Damage(damage);
                if (destroyOnCollide)
                    health.Damage(health.MaxHealth);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_inside.Contains(other)) return;

            _inside.Add(other);

            if (_hitCoroutine != null) return;
            _hitCoroutine = StartCoroutine(Hit());
        }

        private IEnumerator Hit()
        {
            while (_inside.Count > 0)
            {
                TriggerLogic(_inside[0]);
                yield return new WaitForEndOfFrame();
            }

            _hitCoroutine = null;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_inside.Contains(other)) return;
            _inside.Remove(other);
        }
    }
}
