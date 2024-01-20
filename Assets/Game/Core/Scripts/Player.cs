using System;
using Game.Core.Scripts;
using UnityEngine;

namespace Game.Core.Scripts
{
    public enum PlayerState
    {
        Idle,
        Moving
    }

    [RequireComponent(typeof(SpriteSheetController))]
    public class Player : MonoBehaviour
    {
        public float moveSpeed = 5f; // Adjust the speed as needed
        private Rigidbody2D _rigidBody2d;
        private PlayerState _currentState = PlayerState.Idle;
        private Animator _animator;
        private float _horizontalInput;
        private float _verticalInput;

        private static readonly int HorizontalInput = Animator.StringToHash("HorizontalInput");
        private static readonly int VerticalInput = Animator.StringToHash("VerticalInput");

        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        private const float INPUT_THRESHOLD = 0f;

        void Start()
        {
            if (_rigidBody2d == null)_rigidBody2d = GetComponent<Rigidbody2D>();
            if (_animator == null) _animator = GetComponent<Animator>();
        }

        void Update()
        {
            UpdatePlayerState();
            HandleInput();
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            _animator.SetFloat(HorizontalInput, _horizontalInput);
            _animator.SetFloat(VerticalInput,_verticalInput);
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
            }
        }

        void HandleInput()
        {
            _horizontalInput = Input.GetAxis(HORIZONTAL_AXIS);
            _verticalInput = Input.GetAxis(VERTICAL_AXIS);

            if (Mathf.Approximately(_horizontalInput, INPUT_THRESHOLD) && Mathf.Approximately(_verticalInput, INPUT_THRESHOLD))
            {
                SetState(PlayerState.Idle);
            }
            else
            {
                SetState(PlayerState.Moving);
            }
        }

        void SetState(PlayerState newState)
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
    }
}