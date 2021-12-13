using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

namespace GameMain
{
    [RequireComponent(typeof(Tilemap))]
    public class TilemapController : MonoBehaviour
    {
        [Header("Config")] public TileBase ruleTile = null;

        [Header("Runtime")] public List<TileData> tileDataList = new List<TileData>();

        public Tilemap Tilemap { get; private set; }

        private void Awake()
        {
            Tilemap = GetComponent<Tilemap>();
        }

        private void OnEnable()
        {
            if (GameEntry.Tilemap == null)
            {
                return;
            }

            GameEntry.Tilemap.RegisterTilemapController(this);
            RefreshTileList();
        }

        private void OnDisable()
        {
            if (GameEntry.Tilemap == null)
            {
                return;
            }

            GameEntry.Tilemap.DeregisterTilemapController(this);
            StopAllCoroutines();
        }

        /// <summary>
        /// 攻击一个 Tile
        /// </summary>
        public void OnAttackTile(Vector3 hitWorldPoint, Vector3 targetWorldPosition, int attack)
        {
            Vector3Int targetCellPosition = Tilemap.WorldToCell(targetWorldPosition);
            TileData data = GetTileDataByCellPosition(targetCellPosition);
            Log.Info($"攻击到Tilemap：{hitWorldPoint} => {targetCellPosition}");
            if (data == null)
            {
                return;
            }

            data.health -= attack;
            if (data.health <= 0)
            {
                SetTileByCellPosition(targetCellPosition, null);
                StartCoroutine(DropTileBox(hitWorldPoint, CellToWorldGird(targetCellPosition)));
            }
        }

        /// <summary>
        /// 生成一个 Tile
        /// </summary>
        public bool GenerateTile(Vector3 generateWorldPosition, TileBase tile)
        {
            if (tile == null)
            {
                Log.Error($"【GenerateTile】：Tile为空");
                return false;
            }

            return SetTileByCellPosition(WorldToCell(generateWorldPosition), tile);
        }
        
        /// <summary>
        /// 获取生成 Tile 的位置
        /// </summary>
        public Vector3 GetGenerateTilePosition(Vector3 generateWorldPosition)
        {
            return CellToWorldGird(WorldToCell(generateWorldPosition));
        }
        
        /// <summary>
        /// 设置一个 Tile 到指定 CellPosition 上
        /// <returns>是否设置成功</returns>
        /// </summary>
        public bool SetTileByCellPosition(Vector3Int cellPosition, TileBase tile)
        {
            // safety
            if (tile == null)
            {
                RemoveTileByCellPosition(cellPosition);
                return true;
            }
            else
            {
                TileData tilleData = GetTileDataByCellPosition(cellPosition);
                if (tilleData != null)
                {
                    return false;
                }
                
                DRTileInfo tileInfo = GameEntry.Tilemap.GetTileInfoByName(ruleTile.name);
                Tilemap.SetTile(cellPosition, tile);
                tileDataList.Add(TileData.Create(ruleTile, cellPosition, tileInfo.Health));
                return true;
            }
        }

        /// <summary>
        /// CellPosition 转换到 WorldPosition，并且对齐到格子中心
        /// </summary>
        public Vector3 CellToWorldGird(Vector3Int cellPos)
        {
            return Tilemap.CellToWorld(cellPos).ToVector2XY() + Vector2.one * 0.5f;
        }

        /// <summary>
        /// World 转 Cell
        /// </summary>
        public Vector3Int WorldToCell(Vector3 worldPosition)
        {
            return Tilemap.WorldToCell(worldPosition);
        }
        
        /// <summary>
        /// 放置一个 TileBox
        /// </summary>
        private IEnumerator DropTileBox(Vector3 original, Vector3 target)
        {
            Vector3 velocity = (target - original) * GameEntry.Tilemap.dropTileBoxSpeed;
            Log.Info($"Drop Box Velocity：{velocity}");
            yield return null;
            GameEntry.Entity.ShowVFXShotEntity(new VFXShotEntityData(GameEntry.Entity.GenerateSerialId(),
                3001,
                target));
            yield return null;
            GameEntry.Entity.ShowTileBoxEntity(new TileBoxEntityData(GameEntry.Entity.GenerateSerialId(),
                2001,
                ruleTile,
                this,
                target,
                velocity));
        }

        /// <summary>
        /// 拿到指定 CellPosition 身上的 TileData
        /// </summary>
        private TileData GetTileDataByCellPosition(Vector3Int position)
        {
            for (int i = 0; i < tileDataList.Count; i++)
            {
                if (tileDataList[i].position == position)
                {
                    return tileDataList[i];
                }
            }

            return null;
        }

        /// <summary>
        /// 移除指定 CellPosition 上面的Tile
        /// </summary>
        private void RemoveTileByCellPosition(Vector3Int cellPosition)
        {
            for (int i = 0; i < tileDataList.Count; i++)
            {
                if (tileDataList[i].position == cellPosition)
                {
                    Tilemap.SetTile(cellPosition, null);
                    tileDataList.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// 更新 TileList
        /// </summary>
        private void RefreshTileList() 
        {
            if (ruleTile == null)
            {
                return;
            }

            BoundsInt bounds = Tilemap.cellBounds;
            TileBase[] tiles = Tilemap.GetTilesBlock(bounds);

            for (int i = 0; i < tileDataList.Count; i++)
            {
                ReferencePool.Release(tileDataList[i]);
            }

            tileDataList.Clear();

            DRTileInfo tileInfo = GameEntry.Tilemap.GetTileInfoByName(ruleTile.name);
            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    TileBase tile = tiles[x + y * bounds.size.x];
                    if (tile != null)
                    {
                        tileDataList.Add(TileData.Create(ruleTile, 
                            bounds.min  + new Vector3Int(x, y, 0), 
                            tileInfo.Health));
                    }
                }
            }
        }
    }
}