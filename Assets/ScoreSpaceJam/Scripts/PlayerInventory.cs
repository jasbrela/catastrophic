using System;
using System.Collections.Generic;
using ScoreSpaceJam.Scripts.Entity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScoreSpaceJam.Scripts
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private List<BaseGun> guns;
        [SerializeField] private PlayerInput input;

        private int gunIndex;
        private int unlockedGunsQuantity = 1;

        private void Start()
        {
            input.actions["NextWeapon"].performed += NextWeapon;
            input.actions["PreviousWeapon"].performed += PreviousWeapon;
        }

        public void UnlockWeapon()
        {
            unlockedGunsQuantity++;
        }

        private void NextWeapon(InputAction.CallbackContext callbackContext)
        {
            guns[gunIndex].gameObject.SetActive(false);
            gunIndex++;
            if (gunIndex >= unlockedGunsQuantity) gunIndex = 0;
            guns[gunIndex].gameObject.SetActive(true);
        }

        private void PreviousWeapon(InputAction.CallbackContext callbackContext)
        {
            guns[gunIndex].gameObject.SetActive(false);
            gunIndex--;
            if (gunIndex < 0) gunIndex = unlockedGunsQuantity;
            guns[gunIndex].gameObject.SetActive(true);
        }
    }
}
