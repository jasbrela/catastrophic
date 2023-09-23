using System.Collections;
using UnityEngine;

namespace ScoreSpaceJam.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private void Update()
        {
            if (!gameObject.activeInHierarchy) return;
        
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }

        public void OnShoot()
        {
            StartCoroutine(DisableAfterTime());
        }

        IEnumerator DisableAfterTime()
        {
            yield return new WaitForSeconds(3);
            gameObject.SetActive(false);
        }
    }
}
