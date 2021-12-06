using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameMain
{
    public class TileBoxEntityData : EntityData
    {
        public TileBase Tile { get; set; }
        public TilemapController TilemapController { get; set; }
        
        public TileBoxEntityData(int entityId, int typeId, TileBase tile, TilemapController tilemapController, Vector3 generatePosition)
            : base(entityId, typeId)
        {
            this.Tile = tile;
            this.TilemapController = tilemapController;
            this.Position = generatePosition;
        }
    }
}