using System.Collections;
using System.Linq;
using ScoreSpaceJam.Scripts.Managers;
using UnityEngine;

namespace ScoreSpaceJam.Scripts.Entity.Turret
{
    public class TurretHandRotation : BaseHandRotation
    {
        [SerializeField] private GameManager manager;
        [SerializeField] private BaseGun gun;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private float radius;

        private readonly Collider2D[] results = new Collider2D[5];
        private Collider2D[] ordered;

        protected override void Initialize()
        {
            StartCoroutine(Shoot());
        }

        private IEnumerator Shoot()
        {
            while (manager.CurrentState != GameState.GAME_OVER)
            {
                while (manager.CurrentState != GameState.PLAYING) yield return null;
                while (ordered.Length == 0) yield return null;
                
                gun.Shoot(ordered[0].transform.position);
                yield return new WaitForSecondsRealtime(gun.FiringRate);
            }
        }

        private void OnDrawGizmos()
        {
            var center = transform.position;
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(center, radius);
        }

        void Update()
        {
            Physics2D.OverlapCircleNonAlloc(transform.position, radius, results, targetLayer);
            
            ordered = results.Where(col => col != null).OrderBy(c => (transform.position - c.transform.position).sqrMagnitude).ToArray();
            
            if (ordered.Length == 0) return;
            
            Transform t = null;

            foreach (Collider2D col in ordered)   
            {
                if (t != null) break;
                
                col.TryGetComponent(out t);
                Rotate(t.position);
            }
        }
    }
}
