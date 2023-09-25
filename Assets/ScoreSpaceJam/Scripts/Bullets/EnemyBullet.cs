using System.Collections;
using ScoreSpaceJam.Scripts.Managers;
using UnityEngine;

namespace ScoreSpaceJam.Scripts.Bullets
{
    public class EnemyBullet : BaseBullet
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Health health))
            {
                health.Damage(damage);
                OnHit();
            }
        }
    }
}
