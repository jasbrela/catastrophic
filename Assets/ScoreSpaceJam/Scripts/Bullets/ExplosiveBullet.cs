using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScoreSpaceJam.Scripts.Bullets
{
    public class ExplosiveBullet : BaseBullet
    {
        [SerializeField] private float blastRadius = 0.5f;
        [SerializeField] private LayerMask blastLayerMask;

        public override void OnHit()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, blastRadius, blastLayerMask);
            if (hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    if (!hit.TryGetComponent<Health>(out Health hitHealth))
                        continue;

                    hitHealth.Damage(damage);
                }
            }
        }
    }
}
