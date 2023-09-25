using ScoreSpaceJam.Scripts.Bullets;
using ScoreSpaceJam.Scripts.Managers;
using ScoreSpaceJam.Scripts.Utils;
using UnityEngine;

namespace ScoreSpaceJam.Scripts.Entity
{
    public abstract class BaseGun : MonoBehaviour
    {
        public GameManager manager;
        [SerializeField] protected float firingRate = 0.5f;

        [SerializeField] protected AudioSource sfxSource;
        [SerializeField] protected Transform muzzle;
        [SerializeField] protected ObjectPool bulletPool;

        public float FiringRate => firingRate;

        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize() { }

        public virtual void Shoot(Vector3 target)
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
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            go.transform.position = muzzle.position;
            go.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            go.SetActive(true);
            bullet.OnShoot();
            sfxSource.Play();
        }
    }
}
