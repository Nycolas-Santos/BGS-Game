using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.ScriptableObjects.Tiles
{
    [CreateAssetMenu(fileName = "NewTileData",menuName = "Scriptable Objects/New Tile Data")]
    public class TileData : ScriptableObject
    {
        public TileBase[] tiles;
        public AudioClip[] footstepSound;
    }
}
