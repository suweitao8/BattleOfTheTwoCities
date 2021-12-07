using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameMain
{
    public class TileBoxEntityData : EntityData
    {
        public TileBase Tile { get; set; }
        public TilemapController TilemapController { get; set; }
        public Vector3 velocity;
        
        public TileBoxEntityData(int entityId, int typeId, TileBase tile, TilemapController tilemapController, Vector3 generatePosition, Vector3 velocity)
            : base(entityId, typeId)
        {
            this.Tile = tile;
            this.TilemapController = tilemapController;
            this.Position = generatePosition;
            this.velocity = velocity;
        }
    }
}