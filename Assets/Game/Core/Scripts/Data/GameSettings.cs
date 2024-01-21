using UnityEngine;

namespace Game.Core.Scripts.Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Custom/Game Settings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        public float SellingItemMultiplier = 0.75f;
    }
}
