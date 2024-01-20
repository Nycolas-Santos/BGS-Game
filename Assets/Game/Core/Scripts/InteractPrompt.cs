using Game.Core.Scripts.Data;
using UnityEngine;

namespace Game.Core.Scripts
{
    public class InteractPrompt : MonoBehaviour
    {
        public InteractionType InteractionType { get; set; }

        private SpriteRenderer _spriteRenderer;
        private Vector3 _defaultScale;

        [SerializeField] private UserInterfaceData _userInterfaceData;
    
        private const float BREATHING_EFFECT_SPEED = 3.0f; // Adjust the breathing speed
        private const float BREATHING_EFFECT_SCALE = 0.2f; // Adjust the breathing scale
    

        private void Start()
        {
            if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
            switch (InteractionType)
            {
                case InteractionType.Dialogue:
                    _spriteRenderer.sprite = _userInterfaceData.interactionDialogueSprite;
                    break;
                case InteractionType.Shop:
                    _spriteRenderer.sprite = _userInterfaceData.interactionShopSprite;
                    break;
                case InteractionType.Question:
                    _spriteRenderer.sprite = _userInterfaceData.interactionQuestionSprite;
                    break;
                case InteractionType.Other:
                    _spriteRenderer.sprite = _userInterfaceData.interactionOtherSprite;
                    break;
                default:
                    _spriteRenderer.sprite = _userInterfaceData.interactionDialogueSprite;
                    break;
            }

            _defaultScale = transform.localScale;
        }

        private void Update()
        {
            // Calculate the breathing effect using a sine wave
            float breathing = Mathf.Sin(Time.time * BREATHING_EFFECT_SPEED) * BREATHING_EFFECT_SCALE;

            // Apply the breathing effect to the sprite's scale
            transform.localScale = _defaultScale + new Vector3(breathing, breathing, 0);
        }
    }

    public enum InteractionType
    {
        Dialogue,
        Shop, 
        Question,
        Other
    }
}