using ScoreSpaceJam.Scripts.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScoreSpaceJam.Scripts.Entity.Player
{
    public class PlayerGun : MonoBehaviour
    {
        [SerializeField] private Transform muzzle;
        [SerializeField] private ObjectPool bulletPool;
        [SerializeField] private PlayerInput input;

        private void Start()
        {
            input.actions["Shoot"].performed += Shoot;
        }

        private void Shoot(InputAction.CallbackContext ctx)
        {
            var go = bulletPool.GetObject();

            if (!go.TryGetComponent(out Bullet bullet))
            {
                Debug.Log("[PlayerGun]".Bold() + " Missing Bullet script in Bullet Pool's prefab");
                return;
            }
            
            if (go == null)
            {
                // negative feedback
                return;
            }
            
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            go.transform.position = muzzle.position;
            go.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            go.SetActive(true);
            bullet.OnShoot();
        }
    }
}
