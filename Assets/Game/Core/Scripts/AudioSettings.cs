using UnityEngine;

namespace Game.Core.Scripts
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "Custom/Audio Settings", order = 1)]
    public class AudioSettings : ScriptableObject
    {
        public AudioClip uiCloseSound;
        public AudioClip uiOpenSound;
        public AudioClip uiInspectSound;
        public AudioClip uiSellSound;
        public AudioClip uiBuySound;
        public AudioClip uiAddItem;
        public AudioClip uiEquipItem;
        public AudioClip uiOpenInventory;
        public AudioClip uiCloseInventory;
        public AudioClip uiUpdateCoinStat;
        public AudioClip uiBeginDrag;
        public AudioClip uiEndDrag;
        public AudioClip uiNotEnoughCashStranger;

        public AudioClip sfxFootstepSound;
        public AudioClip sfxOpenShop;
        public AudioClip sfxCloseShop;
    }
}
