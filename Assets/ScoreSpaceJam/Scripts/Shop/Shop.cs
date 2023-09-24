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
        
        private SaleableData BuySaleable(SaleableData data, SaleableUI ui)
        {
            if (manager.CurrentMoney < data.price) return data;
            
            manager.SpendCurrency(data.price);

            data = data.nextSaleable;
                
            if (data == null)
            {
                ui.gameObject.SetActive(false);
                return null;
            }
                
            ui.Display(data);
            return data;
        }
        
        public void BuyGun() => currentGun = BuySaleable(currentGun, upgradeUI);
        public void BuyGunUpgrade() => currentUpgrade = BuySaleable(currentUpgrade, gunUI);
        public void BuyTurret() => currentTurret = BuySaleable(currentTurret, turretUI);

        public void ShowUI() => shoppingUI.gameObject.SetActive(true);
        public void HideUI() => shoppingUI.gameObject.SetActive(false);

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
        
        
    }
}
