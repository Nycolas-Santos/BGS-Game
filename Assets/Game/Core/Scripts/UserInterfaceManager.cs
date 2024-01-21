using System;
using Game.Core.Scripts.Data;

namespace Game.Core.Scripts
{
    public class UserInterfaceManager : Singleton<UserInterfaceManager>
    {
        public UserInterfaceData UserInterfaceData;
        public SubtitlesController SubtitlesController { get; set; }
        public InventoryController InventoryController { get; set; }
        public CoinStatsController CoinStatsController { get; set; }
        public ShopController ShopController { get; set; }

        public bool IsInventoryOpen()
        {
            return InventoryController.gameObject.activeSelf;
        }

        private void Start()
        {
            if (SubtitlesController == null) SubtitlesController = GetComponentInChildren<SubtitlesController>(true);
            if (InventoryController == null) InventoryController = GetComponentInChildren<InventoryController>(true);
            if (CoinStatsController == null) CoinStatsController = GetComponentInChildren<CoinStatsController>(true);
            if (ShopController == null) ShopController = GetComponentInChildren<ShopController>(true);
            if (GameManager.Instance.PlayerInstance != null) GameManager.Instance.PlayerInstance.Inventory.OnCoinChange += UpdateCoinStat;
        }
        

        public void OpenInventory()
        {
            InventoryController.gameObject.SetActive(true);
        }

        public void CloseInventory()
        {
            InventoryController.gameObject.SetActive(false);
        }

        public void OpenShop()
        {
            ShopController.gameObject.SetActive(true);
            GameManager.Instance.PlayerInstance.SetState(PlayerState.Interacting);
        }

        public void CloseShop()
        {
            ShopController.gameObject.SetActive(false);
            GameManager.Instance.PlayerInstance.SetState(PlayerState.Idle);
        }

        public void UpdateCoinStat(int coinAmount)
        {
            CoinStatsController.UpdateCoinStats(coinAmount);
        }
    }
}
