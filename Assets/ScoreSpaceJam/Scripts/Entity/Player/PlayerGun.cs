using System.Collections;
using ScoreSpaceJam.Scripts.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScoreSpaceJam.Scripts.Entity.Player
{
    public class PlayerGun : BaseGun
    {
        [SerializeField] private PlayerInput input;
        private bool isHolding;
        private bool allowShooting = true;
        Coroutine shootingCoroutine = null;

        protected override void Initialize()
        {
            input.actions["Shoot"].performed += ButtonDown;
            input.actions["Shoot"].canceled += ButtonUp;
        }

        private void ButtonDown(InputAction.CallbackContext ctx)
        {
            isHolding = true;
            if (shootingCoroutine == null)
                shootingCoroutine = StartCoroutine(KeepShooting());
        }

        private void ButtonUp(InputAction.CallbackContext ctx)
        {
            isHolding = false;
        }

        private IEnumerator KeepShooting()
        {
            while (isHolding)
            {
                if (allowShooting) Shoot(Input.mousePosition);
                allowShooting = false;
                yield return new WaitForSeconds(FiringRate);
                allowShooting = true;
            }

            shootingCoroutine = null;
        }
    }
}
