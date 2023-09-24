using ScoreSpaceJam.Scripts.Bullets;
using ScoreSpaceJam.Scripts.Utils;
using UnityEngine;

namespace ScoreSpaceJam.Scripts.Entity.Player
{
    public class PlayerShotgun : PlayerGun
    {
        public float angleVariationPerBullet = 15;
        public int bulletCountPerShot = 3;

        public override void Shoot(Vector3 target)
        {
            for (int i = 0; i < bulletCountPerShot; i++)
            {
                var go = bulletPool.GetObject();

                if (go == null)
                {
                    // negative feedback
                    return;
                }

                if (!go.TryGetComponent(out BaseBullet bullet))
                {
                    Debug.Log("[BaseGun]".Bold() + " Missing Bullet script in Bullet Pool's prefab");
                    return;
                }

                bullet.manager = manager;

                var dir = target - Camera.main.WorldToScreenPoint(transform.position);
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + Random.Range(-angleVariationPerBullet, angleVariationPerBullet);

                go.transform.position = muzzle.position;
                go.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                go.SetActive(true);
                bullet.OnShoot();
            }
        }
    }
}
