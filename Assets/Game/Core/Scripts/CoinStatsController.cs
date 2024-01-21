using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.Scripts
{
    public class CoinStatsController : MonoBehaviour
    {
        private Text coinText;

        private void Awake()
        {
            coinText = GetComponent<Text>();
        }

        public void UpdateCoinStats(int amount)
        {
            coinText.text = amount.ToString();
        }
    }
}
