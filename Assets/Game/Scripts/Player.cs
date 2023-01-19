using System;
using System.Collections.Generic;
using Game.Scripts.StateMachines.Player;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
    [RequireComponent(typeof(Animator),typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        public float speed;

        [SerializeField]
        private LayerMask groundLayer;
        
        private MovementStateMachine movementStateMachine;

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


        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            AudioSource = GetComponent<AudioSource>();
            movementStateMachine = gameObject.AddComponent<MovementStateMachine>();
            movementStateMachine.Owner = this;
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
