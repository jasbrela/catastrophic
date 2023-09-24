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

        private void OnEnable()
        {
            input.actions["Shoot"].performed += ButtonDown;
            input.actions["Shoot"].canceled += ButtonUp;
        }

        private void OnDisable()
        {
            input.actions["Shoot"].performed -= ButtonDown;
            input.actions["Shoot"].canceled -= ButtonUp;

            if (shootingCoroutine != null)
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
            }
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
                yield return new WaitForSeconds(1 / FiringRate);
                allowShooting = true;
            }

            shootingCoroutine = null;
        }
    }
}
