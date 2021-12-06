using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

namespace GameMain
{
    [RequireComponent(typeof(Tilemap))]
    public class TilemapController : MonoBehaviour
    {
        public Tilemap Tilemap { get; private set; }
        
        private void Awake()
        {
            Tilemap = GetComponent<Tilemap>();
        }

        private void OnEnable()
        {
            GameEntry.Tilemap?.RegisterTilemapController(this);
        }

        private void OnDisable()
        {
            GameEntry.Tilemap?.DeregisterTilemapController(this);
            
            StopAllCoroutines();
        }
        
        /// <summary>
        /// 攻击一个 Tile
        /// </summary>
        public void OnAttackTile(Vector3 worldPosition)
        {
            Log.Info($"被攻击：{gameObject.name} => {worldPosition}");
            Vector3Int cellPos = Tilemap.WorldToCell(worldPosition);
            TileBase tile = Tilemap.GetTile(cellPos);
            Tilemap.SetTile(cellPos, null);
            StartCoroutine(DropTileBox(cellPos, tile));
        }

        /// <summary>
        /// 扔方块
        /// </summary>
        private IEnumerator DropTileBox(Vector3Int cellPos, TileBase tile)
        {
            yield return null;
            // TODO 播放特效
            yield return null;
            Vector2 worldPosition = Tilemap.CellToWorld(cellPos).ToVector2XY() + Vector2.one * 0.5f;
            // TODO 生成方块
            GameEntry.Entity.ShowTileBoxEntity(new TileBoxEntityData(GameEntry.Entity.GenerateSerialId()
                , 1000
                , tile
                , this
                , worldPosition));
        }
    }
}