using System;
using System.Collections.Generic;
using Game.Core.Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Core.Scripts
{
    public enum PlayerState
    {
        Idle,
        Moving,
        Interacting
    }

    [RequireComponent(typeof(SpriteSheetController))]
    public class Player : MonoBehaviour
    {
        public float moveSpeed = 5f; // Adjust the speed as needed
        public Item equippedHead;
        public Item equippedClothes;
        public List<GameObject> _interactables;
        public Inventory Inventory { get; set; }

        private Rigidbody2D _rigidBody2d;
        private PlayerState _currentState = PlayerState.Idle;
        private Animator _animator;
        private float _horizontalInput;
        private float _verticalInput;
        private bool _interact;
        private CircleCollider2D _interactionRadius;
        private SpriteSheetController _spriteSheetController;

        private static readonly int HorizontalInput = Animator.StringToHash("HorizontalInput");
        private static readonly int VerticalInput = Animator.StringToHash("VerticalInput");

        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        private const float STOP_PLAYER_LERP_SPEED = 10f;
        private const float STOP_PLAYER_LERP_TIME = 1f;
        private const float MOVEMENT_TOLERANCE = 0.01f;
        private const float INPUT_THRESHOLD = 0f;
        private const int CLOTHES_SPRITE_LAYER_INDEX = 1;
        private const int HEAD_SPRITE_LAYER_INDEX = 2;

        public bool IsItemEquipped(Item item)
        {
            return item == equippedClothes || item == equippedHead;
        }

        private void Awake()
        {
            if (Inventory == null) Inventory = GetComponent<Inventory>();
        }

        void Start()
        {
            if (_rigidBody2d == null)_rigidBody2d = GetComponent<Rigidbody2D>();
            if (_animator == null) _animator = GetComponent<Animator>();
            if (_interactionRadius == null) _interactionRadius = GetComponent<CircleCollider2D>();
            if (_spriteSheetController == null) _spriteSheetController = GetComponent<SpriteSheetController>();
        }

        private void OnEnable()
        {
            Inventory.OnAddItem += AssignItemToSlot;
            Inventory.OnRemoveItem += UnassignItemToSlot;
        }

        private void OnDisable()
        {
            Inventory.OnAddItem -= AssignItemToSlot;
            Inventory.OnRemoveItem -= UnassignItemToSlot;
        }

        void Update()
        {
            UpdatePlayerState();
            HandleInput();
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            if (IsPlayerMoving())_animator.SetFloat(HorizontalInput, _horizontalInput);
            else
            {
                _animator.SetFloat(HorizontalInput, Mathf.Lerp(_animator.GetFloat(HorizontalInput), 0f, STOP_PLAYER_LERP_TIME));
            }
            if (IsPlayerMoving())_animator.SetFloat(VerticalInput,_verticalInput);
            else
            {
                _animator.SetFloat(VerticalInput, Mathf.Lerp(_animator.GetFloat(VerticalInput), 0f, STOP_PLAYER_LERP_TIME));
            }
        }

        void UpdatePlayerState()
        {
            switch (_currentState)
            {
                case PlayerState.Idle:
                    break;
                case PlayerState.Moving:
                    MovePlayer();
                    break;
                case PlayerState.Interacting:
                    _rigidBody2d.velocity = Vector2.Lerp(_rigidBody2d.velocity, Vector2.zero, STOP_PLAYER_LERP_SPEED * Time.deltaTime);
                    break;
            }
        }

        private void HandleInput()
        {
            _horizontalInput = Input.GetAxis(HORIZONTAL_AXIS);
            _verticalInput = Input.GetAxis(VERTICAL_AXIS);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_currentState != PlayerState.Interacting) TryInteract();
            }
            
            if (_currentState == PlayerState.Interacting) return;
            
            if (Mathf.Approximately(_horizontalInput, INPUT_THRESHOLD) && Mathf.Approximately(_verticalInput, INPUT_THRESHOLD))
            {
                SetState(PlayerState.Idle);
            }
            else
            {
                SetState(PlayerState.Moving);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (UserInterfaceManager.Instance.IsInventoryOpen())
                {
                    UserInterfaceManager.Instance.CloseInventory();
                }
                else
                {
                    UserInterfaceManager.Instance.OpenInventory();
                }
            }
        }

        public void SetState(PlayerState newState)
        {
            if (_currentState == newState)
                return;

            _currentState = newState;
        }

        void MovePlayer()
        {
            float horizontal = Input.GetAxis(HORIZONTAL_AXIS);
            float vertical = Input.GetAxis(VERTICAL_AXIS);

            Vector2 movement = new Vector2(horizontal, vertical);
            _rigidBody2d.velocity = movement * moveSpeed;

            movement.Normalize();
        }
        
        // Called when another collider enters the trigger area
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the entered object has an IInteractable component
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // Call the Interact method on the interactable object
                interactable.CanInteract(true);
                if (!_interactables.Contains(interactable.gameObject))_interactables.Add(interactable.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // Check if the exit object has an IInteractable component
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // Call the Interact method on the interactable object
                interactable.CanInteract(false);
                if (_interactables.Contains(interactable.gameObject))_interactables.Remove(interactable.gameObject);
            }
        }

        private void AssignItemToSlot(Item item)
        {
            UserInterfaceManager.Instance.InventoryController.TryAddItemToSlot(item);
        }

        private void UnassignItemToSlot(Item item)
        {
            UserInterfaceManager.Instance.InventoryController.RemoveItemFromSlot(item);
        }
        
        bool IsPlayerMoving()
        {
            if (_rigidBody2d != null)
            {
                // Check if the magnitude of the velocity is greater than the tolerance
                return _rigidBody2d.velocity.magnitude > MOVEMENT_TOLERANCE;
            }
            else
            {
                return false;
            }
        }

        private void TryInteract()
        {
            GameObject closest = null;
            float closestDistance = float.MaxValue;
            Vector3 playerPosition = transform.position;

            foreach (GameObject interactable in _interactables)
            {
                float distance = Vector3.Distance(playerPosition, interactable.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = interactable.gameObject;
                }
            }
            if (closest != null) closest.GetComponent<IInteractable>().Interact();

        }

        public void UnequipItem(Item item)
        {
            switch (item.ItemType)
            {
                case ItemType.Clothes:
                    EquipClothes(null);
                    break;
                case ItemType.Head:
                    EquipHead(null);
                    break;
                case ItemType.Weapon:
                    break;
                case ItemType.Consumable:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void EquipClothes(Item clothes)
        {
            if (clothes == null)
            {
                equippedClothes = null;
                _spriteSheetController.ChangeSpriteData(null,CLOTHES_SPRITE_LAYER_INDEX);
            }
            else
            {
                _spriteSheetController.ChangeSpriteData(clothes.SpriteData,CLOTHES_SPRITE_LAYER_INDEX);
            }
            
        }
        

        public void EquipHead(Item head)
        {
            if (head == null)
            {
                equippedHead = null;
                _spriteSheetController.ChangeSpriteData(null,HEAD_SPRITE_LAYER_INDEX);
            }
            else
            {
                _spriteSheetController.ChangeSpriteData(head.SpriteData,HEAD_SPRITE_LAYER_INDEX);
            }
            
        }
    }
}