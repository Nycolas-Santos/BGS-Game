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

        public static event Action OnCloseShop;
        public static event Action OnOpenShop;
        public static event Action OnOpenInventory;
        public static event Action OnCloseInventory;

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
            SoundManager.Instance.PlayUISound(SoundManager.Instance.audioSettings.uiOpenInventory);
            OnOpenInventory?.Invoke();
        }

        public void CloseInventory()
        {
            InventoryController.gameObject.SetActive(false);
            SoundManager.Instance.PlayUISound(SoundManager.Instance.audioSettings.uiCloseInventory);
            OnCloseInventory?.Invoke();
        }

        public void OpenShop()
        {
            ShopController.gameObject.SetActive(true);
            GameManager.Instance.PlayerInstance.SetState(PlayerState.Interacting);
            SoundManager.Instance.PlayUISound(SoundManager.Instance.audioSettings.uiOpenInventory);
            OnOpenShop?.Invoke();
        }

        public void CloseShop()
        {
            ShopController.gameObject.SetActive(false);
            GameManager.Instance.PlayerInstance.SetState(PlayerState.Idle);
            SoundManager.Instance.PlayUISound(SoundManager.Instance.audioSettings.uiCloseInventory);
            OnCloseShop?.Invoke();
        }

        public void UpdateCoinStat(int coinAmount)
        {
            CoinStatsController.UpdateCoinStats(coinAmount);
            SoundManager.Instance.PlayUISound(SoundManager.Instance.audioSettings.uiUpdateCoinStat);
        }
    }
}
