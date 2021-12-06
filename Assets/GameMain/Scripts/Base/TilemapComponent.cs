using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class TilemapComponent : GameFrameworkComponent
    {
        public List<TilemapController> tilemapControllerList = new List<TilemapController>();
        
        /// <summary>
        /// 注册一个 Tilemap Controller
        /// </summary>
        public void RegisterTilemapController(TilemapController tilemapController)
        {
            if (tilemapControllerList.Contains(tilemapController) == false)
            {
                tilemapControllerList.Add(tilemapController);
            }
        }

        /// <summary>
        /// 注销一个
        /// </summary>
        public void DeregisterTilemapController(TilemapController tilemapController)
        {
            tilemapControllerList.Remove(tilemapController);
        }

        /// <summary>
        /// 攻击一个 Tile
        /// </summary>
        public void AttackTile(GameObject gameObject, Vector3 worldPosition)
        {
            for (int i = 0; i < tilemapControllerList.Count; i++)
            {
                if (tilemapControllerList[i].gameObject == gameObject)
                {
                    tilemapControllerList[i].OnAttackTile(worldPosition);
                }
            }
        }
    }
}
