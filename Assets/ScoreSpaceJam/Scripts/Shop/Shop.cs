using System;
using ScoreSpaceJam.Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScoreSpaceJam.Scripts.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private GameObject shoppingUI;
        
        [SerializeField] private SaleableData firstGun;
        [SerializeField] private SaleableUI gunUI;
        
        [SerializeField] private SaleableData firstUpgrade;
        [SerializeField] private SaleableUI upgradeUI;
        
        [SerializeField] private SaleableData firstTurret;
        [SerializeField] private SaleableUI turretUI;
        
        [HideInInspector] public GameManager manager;

        private SaleableData currentGun;
        private SaleableData currentUpgrade;
        private SaleableData currentTurret;

        private void Start()
        {
            ToggleButtonsVisibility(false);
            
            currentGun = firstGun;
            currentUpgrade = firstUpgrade;
            currentTurret = firstTurret;
        }

        public void BuyGun()
        {
            if (manager.CurrentMoney > currentGun.price)
            {
                currentGun = currentGun.nextSaleable;
                if (currentGun == null) gunUI.gameObject.SetActive(false);
            }
        }
        
        public void BuyGunUpgrade()
        {
            if (manager.CurrentMoney > currentUpgrade.price)
            {
                currentUpgrade = currentUpgrade.nextSaleable;
                if (currentUpgrade == null) upgradeUI.gameObject.SetActive(false);
            }
        }
        
        public void BuyTurret()
        {
            if (manager.CurrentMoney > currentTurret.price)
            {
                currentTurret = currentTurret.nextSaleable;
                if (currentTurret == null) turretUI.gameObject.SetActive(false);
            }
        }

        public void ShowUI()
        {
            shoppingUI.gameObject.SetActive(true);
        }

        public void EnableShopButtons()
        {
            ToggleButtonsVisibility(true);
            UpdateDisplay();
        }

        private void ToggleButtonsVisibility(bool visible)
        {
            gunUI.gameObject.SetActive(visible);
            upgradeUI.gameObject.SetActive(visible);
            turretUI.gameObject.SetActive(visible);
        }

        private void UpdateDisplay()
        {
            gunUI.Display(currentGun);
            upgradeUI.Display(currentUpgrade);
            turretUI.Display(currentTurret);
        }
        
        public void HideUI()
        {
            shoppingUI.gameObject.SetActive(false);
        }
    }
}
