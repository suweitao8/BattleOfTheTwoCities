using System;
using GameFramework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameMain
{
    [Serializable]
    public class TileData : IReference
    {
        public TileBase tile;
        public int health;
        public Vector3Int position;
        
        public static TileData Create(TileBase tile, Vector3Int position, int health)
        {
            TileData data = ReferencePool.Acquire<TileData>();
            data.tile = tile;
            data.health = health;
            data.position = position;
            return data;
        }
        
        public void Clear()
        {
        }
    }
}