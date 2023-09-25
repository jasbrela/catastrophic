using System;
using System.Collections.Generic;
using ScoreSpaceJam.Scripts.Entity;
using ScoreSpaceJam.Scripts.Entity.Turret;
using ScoreSpaceJam.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScoreSpaceJam.Scripts
{
    public class PlayerInventory : MonoBehaviour
    {
        [Header("Turret")]
        [SerializeField] private Turret turretPrefab;

        [Header("Guns")]
        [SerializeField] private List<BaseGun> guns;
        [SerializeField] private PlayerInput input;
        [SerializeField] private GameManager manager;
        [SerializeField] private GameObject shopTooltip;

        private int gunIndex;
        private int unlockedGunsQuantity = 1;

        private void Start()
        {
            input.actions["NextWeapon"].performed += NextWeapon;
            input.actions["PreviousWeapon"].performed += PreviousWeapon;

            shopTooltip.SetActive(false);

            manager.onChangeState.AddListener(OnChangeState);
        }

        private void OnChangeState()
        {
            if (manager.CurrentState == GameState.SHOPPING)
            {
                shopTooltip.SetActive(unlockedGunsQuantity > 1);
            }
        }

        public void UnlockWeapon()
        {
            if (guns.Count == unlockedGunsQuantity) return;
            unlockedGunsQuantity++;

            shopTooltip.SetActive(true);
        }

        public void PlaceTurret(ObjectPool bulletPool)
        {
            var t = transform;

            Turret turret = Instantiate(turretPrefab, t.position, t.rotation);
            turret.RegisterGameManager(manager);
            turret.RegisterGunBulletPool(bulletPool);
        }

        private void NextWeapon(InputAction.CallbackContext callbackContext)
        {
            if (manager.CurrentState == GameState.PAUSED
            || manager.CurrentState == GameState.GAME_OVER) return;

            guns[gunIndex].gameObject.SetActive(false);
            gunIndex++;
            if (gunIndex >= unlockedGunsQuantity) gunIndex = 0;
            guns[gunIndex].gameObject.SetActive(true);
        }

        private void PreviousWeapon(InputAction.CallbackContext callbackContext)
        {
            if (manager.CurrentState == GameState.PAUSED
            || manager.CurrentState == GameState.GAME_OVER) return;

            guns[gunIndex].gameObject.SetActive(false);
            gunIndex--;
            if (gunIndex < 0) gunIndex = unlockedGunsQuantity - 1;
            guns[gunIndex].gameObject.SetActive(true);
        }
    }
}
