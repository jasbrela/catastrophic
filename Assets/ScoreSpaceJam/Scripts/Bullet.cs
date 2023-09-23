using System.Collections;
using UnityEngine;

namespace ScoreSpaceJam.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float damage = 1f;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float lifetime = 2f;
        [SerializeField] private Rigidbody2D rb;

        public float Damage => damage;
        private void FixedUpdate()
        {
            if (!gameObject.activeInHierarchy) return;

            // rb.velocity = transform.right * (speed * Time.deltaTime);
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }

        public void OnShoot()
        {
            StartCoroutine(DisableAfterTime());
        }

        IEnumerator DisableAfterTime()
        {
            yield return new WaitForSeconds(lifetime);
            gameObject.SetActive(false);
        }
    }
}
