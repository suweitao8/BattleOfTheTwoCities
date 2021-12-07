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
            CreateTileList();
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

        public void OnAttackTile(Vector3 hitPoint, Vector3 worldPosition)
        {
            Vector3Int cellPos = Tilemap.WorldToCell(worldPosition);
            TileData data = GetTileDataByCellPosition(cellPos);
            if (data == null)
            {
                return;
            }

            data.health--;
            if (data.health <= 0)
            {
                SetTile(cellPos, null);
                StartCoroutine(DropTileBox(hitPoint, CellToWorldGird(cellPos)));
            }
        }

        public void SetTile(Vector3Int pos, TileBase tile)
        {
            // safety
            RemoveTileByCellPosition(pos);

            if (tile != null)
            {
                DRTileInfo tileInfo = GameEntry.Tilemap.GetTileInfoByName(ruleTile.name);
                tileDataList.Add(TileData.Create(ruleTile, pos, tileInfo.Health));
            }
            else
            {
                RemoveTileByCellPosition(pos);
            }
        }

        public Vector3 CellToWorldGird(Vector3Int cellPos)
        {
            return Tilemap.CellToWorld(cellPos).ToVector2XY() + Vector2.one * 0.5f;
        }

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

        private void RemoveTileByCellPosition(Vector3Int position)
        {
            for (int i = 0; i < tileDataList.Count; i++)
            {
                if (tileDataList[i].position == position)
                {
                    Tilemap.SetTile(position, null);
                    tileDataList.RemoveAt(i);
                    return;
                }
            }
        }

        private void CreateTileList()
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