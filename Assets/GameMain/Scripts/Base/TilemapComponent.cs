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

        public void RegisterTilemapController(TilemapController tilemapController)
        {
            if (tilemapControllerList.Contains(tilemapController) == false)
            {
                tilemapControllerList.Add(tilemapController);
            }
        }

        public void DeregisterTilemapController(TilemapController tilemapController)
        {
            tilemapControllerList.Remove(tilemapController);
        }

        public void AttackTile(GameObject gameObject, Vector3 hitPoint, Vector3 attackPoint)
        {
            for (int i = 0; i < tilemapControllerList.Count; i++)
            {
                if (tilemapControllerList[i].gameObject == gameObject)
                {
                    tilemapControllerList[i].OnAttackTile(hitPoint, attackPoint);
                }
            }
        }

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
    }
}
