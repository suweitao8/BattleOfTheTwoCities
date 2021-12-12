using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class TilemapComponent : GameFrameworkComponent
    {
        [Header("Config")] public float dropTileBoxSpeed = 5f;
        
        [Header("Runtime")]
        public List<TilemapController> tilemapControllerList = new List<TilemapController>();
        
        private DRTileInfo[] m_TileInfos;
        public DRTileInfo[] TileInfos
        {
            get
            {
                IDataTable<DRTileInfo> dtTileInfo = GameEntry.DataTable.GetDataTable<DRTileInfo>();
                m_TileInfos = dtTileInfo.GetAllDataRows();
                return m_TileInfos;
            }
        }

        /// <summary>
        /// 注册一个Tilemap
        /// </summary>
        public void RegisterTilemapController(TilemapController tilemapController)
        {
            if (tilemapControllerList.Contains(tilemapController) == false)
            {
                tilemapControllerList.Add(tilemapController);
            }
        }

        /// <summary>
        /// 注销一个Tilemap
        /// </summary>
        public void DeregisterTilemapController(TilemapController tilemapController)
        {
            tilemapControllerList.Remove(tilemapController);
        }

        /// <summary>
        /// 生成一个Tile
        /// </summary>
        public bool GenerateTile(Vector3 generatePosition, TileBase tile)
        {
            TilemapController tilemapController = GetTilemapControllerByTile(tile);
            if (tilemapController == null)
            {
                return false;
            }

            return tilemapController.GenerateTile(generatePosition, tile);
        }
        
        /// <summary>
        /// 攻击一个Tilemap
        /// </summary>
        public void AttackTile(GameObject gameObject, Vector3 hitPoint, Vector3 attackPoint, int attack)
        {
            TilemapController tilemapController =  GetTilemapControllerByGameObject(gameObject);
            if (tilemapController == null)
            {
                Log.Info($"无法获取该GameObject({gameObject.name})对应的TilemapController");
                return;
            }
            
            tilemapController.OnAttackTile(hitPoint, attackPoint, attack);
        }
        
        /// <summary>
        /// 拿到Tile对应的的TilemapControoler
        /// </summary>
        public TilemapController GetTilemapControllerByTile(TileBase tile)
        {
            for (int i = 0; i < tilemapControllerList.Count; i++)
            {
                if (tilemapControllerList[i].ruleTile == tile)
                {
                    return tilemapControllerList[i];
                }
            }

            return null;
        }

        /// <summary>
        /// 拿到GameObject身上的TilemapControoler
        /// </summary>
        public TilemapController GetTilemapControllerByGameObject(GameObject go)
        {
            for (int i = 0; i < tilemapControllerList.Count; i++)
            {
                if (tilemapControllerList[i].gameObject == go)
                {
                    return tilemapControllerList[i];
                }
            }

            return null;
        }

        /// <summary>
        /// 使用名字获取到对应的TileInfo
        /// </summary>
        public DRTileInfo GetTileInfoByName(string tileName)
        {
            for (int i = 0; i < TileInfos.Length; i++)
            {
                if (TileInfos[i].AssetName == tileName)
                {
                    return TileInfos[i];
                }
            }
            
            Log.Warning($"Can't find tile name equal {tileName}");
            return null;
        }
        
        /// <summary>
        /// 使用TileBase获取到对应的TileInfo
        /// </summary>
        public DRTileInfo GetTileInfoByTile(TileBase tile)
        {
            return GetTileInfoByName(tile.name);
        }
    }
}
