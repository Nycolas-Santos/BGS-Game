using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TileData = Game.ScriptableObjects.Tiles.TileData;

namespace Game.Scripts
{
    public class TilemapManager : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private List<TileData> tileData;

        private Dictionary<TileBase, TileData> dataFromTiles;
        
        public TileData GetDataFromTile(Vector3Int tilePosition)
        {
            var tile = tilemap.GetTile(tilePosition);
            return dataFromTiles.ContainsKey(tile) ? dataFromTiles[tile] : null;
        }
        
        public TileData GetDataFromTile(TileBase tile)
        {
            return dataFromTiles.ContainsKey(tile) ? dataFromTiles[tile] : null;
        }

        private void Awake()
        {
            dataFromTiles = new Dictionary<TileBase, TileData>();

            foreach (var data in tileData)
            {
                foreach (var tile in data.tiles)
                {
                    dataFromTiles.Add(tile, data);
                }
            }
        }

        private void Start()
        {
            if (tilemap == null) tilemap = GetComponent<Tilemap>();
        }
    }
}
