using System.Collections;
using ScoreSpaceJam.Scripts.Managers;
using UnityEngine;

namespace ScoreSpaceJam.Scripts.Bullets
{
    public class BaseBullet : MonoBehaviour
    {
        [SerializeField] protected float damage = 1f;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float lifetime = 2f;

        [HideInInspector] public GameManager manager;

        public float Damage => damage;
        private void FixedUpdate()
        {
            if (!gameObject.activeInHierarchy) return;
            if (manager.CurrentState == GameState.PAUSED || manager.CurrentState == GameState.GAME_OVER) return;

            // rb.velocity = transform.right * (speed * Time.deltaTime);
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }

        public virtual void OnShoot()
        {
            StartCoroutine(DisableAfterTime());
        }

        public virtual void OnHit()
        {
            gameObject.SetActive(false);
        }

        IEnumerator DisableAfterTime()
        {
            yield return new WaitForSeconds(lifetime);
            gameObject.SetActive(false);
        }
    }
}
