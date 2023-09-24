using ScoreSpaceJam.Scripts.Managers;
using UnityEngine;

namespace ScoreSpaceJam.Scripts.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private GameObject shoppingUI;
        [SerializeField] private PlayerInventory inventory;
        
        [Header("Gun")]
        [SerializeField] private SaleableData firstGun;
        [SerializeField] private SaleableUI gunUI;
        
        [Header("Upgrade")]
        [SerializeField] private SaleableData firstUpgrade;
        [SerializeField] private SaleableUI upgradeUI;
        
        [Header("Turret")]
        [SerializeField] private SaleableData firstTurret;
        [SerializeField] private SaleableUI turretUI;
        [SerializeField] private float priceMultiplier;
        
        [HideInInspector] public GameManager manager;

        private SaleableData currentGun;
        private SaleableData currentUpgrade;
        private SaleableData currentTurret;

        private int turretCount = 0;
        
        private void Start()
        {
            ToggleButtonsVisibility(false, true);
            
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
        
        public void BuyGun()
        {
            var item = BuySaleable(currentGun, upgradeUI);
            
            if (item == currentGun) return;
            
            currentGun = item;
            inventory.UnlockWeapon();
        }

        public void BuyGunUpgrade() => currentUpgrade = BuySaleable(currentUpgrade, gunUI);
        public void BuyTurret()
        {
            var price = currentTurret.price + currentTurret.price * turretCount;
            
            if (manager.CurrentMoney < price) return;
            
            manager.SpendCurrency(price);
            
            turretCount++;
            
            price = currentTurret.price + currentTurret.price * turretCount;
            
            turretUI.Display(currentTurret, price);
            
            inventory.PlaceTurret();
        }

        public void ShowUI() => shoppingUI.gameObject.SetActive(true);
        public void HideUI() => shoppingUI.gameObject.SetActive(false);

        public void EnableShopButtons()
        {
            ToggleButtonsVisibility(true);
            UpdateDisplay();
        }

        private void ToggleButtonsVisibility(bool visible, bool force = false)
        {
            if (force || manager.CurrentMoney > currentGun.price) gunUI.gameObject.SetActive(visible);
            if (force || manager.CurrentMoney > currentUpgrade.price) upgradeUI.gameObject.SetActive(visible);
            if (force || manager.CurrentMoney > currentTurret.price) turretUI.gameObject.SetActive(visible);
        }

        private void UpdateDisplay()
        {
            gunUI.Display(currentGun);
            upgradeUI.Display(currentUpgrade);
            turretUI.Display(currentTurret);
        }
        
        
    }
}
