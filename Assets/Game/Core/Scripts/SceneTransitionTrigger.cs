using System;
using UnityEngine;

namespace Game.Core.Scripts
{
    public class SceneTransitionTrigger : MonoBehaviour
    {
        public string sceneName;
        public int entranceIndex;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                GameManager.Instance.LoadScene(sceneName,entranceIndex);
            }
        }
    }
}