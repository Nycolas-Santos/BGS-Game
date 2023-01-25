using System;
using System.Collections.Generic;
using Game.Scripts.StateMachines;
using Game.Scripts.StateMachines.Player;
using Game.Scripts.StateMachines.Player.States;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        public float speed;
        public float interactRadius;
        public LayerMask interactLayer;

        [SerializeField]
        private LayerMask groundLayer;

        private MovementStateMachine movementStateMachine;
        private InteractionStateMachine interactionStateMachine;

        public Rigidbody2D Rigidbody { get; set; }
        public Animator Animator { get; set; }
        public SpriteRenderer SpriteRenderer { get; set; }
        public AudioSource AudioSource { get; set; }

        public Tilemap GetCurrentGroundTilemap()
        {
            Collider2D hit = Physics2D.OverlapPoint(transform.position,groundLayer);
            if (hit.TryGetComponent(out Tilemap tilemap)) return tilemap;
            return null;
        }
        
        public void SetMovementState(string state)
        {
            movementStateMachine.ChangeState(state);
        }

        public void SetInteractionState(string state)
        {
            interactionStateMachine.ChangeState(state);
        }


        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            AudioSource = GetComponent<AudioSource>();
            
            movementStateMachine = gameObject.AddComponent<MovementStateMachine>();
            interactionStateMachine = gameObject.AddComponent<InteractionStateMachine>();
            movementStateMachine.Owner = this;
            interactionStateMachine.Owner = this;
        }

        private void Start()
        {
            GameManager.Instance.Player = this;
        }
        public void Footstep()
        {
            var tilemap = GetCurrentGroundTilemap();
            if (tilemap.gameObject.TryGetComponent(out TilemapManager tilemapManager))
            {
                Vector3Int position = tilemap.WorldToCell(gameObject.transform.position);
                var tile = tilemap.GetTile(position);
                var tileData = tilemapManager.GetDataFromTile(tile);
                AudioSource.pitch = Random.Range(.95f, 1.05f);
                AudioSource.PlayOneShot(tileData.footstepSound[Random.Range(0,tileData.footstepSound.Length - 1)],.65f);
            }
        }
        
    }
}
