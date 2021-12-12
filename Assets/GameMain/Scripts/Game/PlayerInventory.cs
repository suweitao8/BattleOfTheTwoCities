using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameMain
{
    public class PlayerInventory : MonoBehaviour
    {
        [Header("Runtime")] public int playerIndex = -1;
        public TileInventoryData leftTileInventory;
        public TileInventoryData rightTileInventory;

        private LevelForm m_LevelForm;
        public LevelForm LevelForm
        {
            get
            {
                if (m_LevelForm == null)
                {
                    m_LevelForm = GameEntry.UI.GetUIForm(UIFormId.LevelForm) as LevelForm;
                }

                return m_LevelForm;
            }
        }

        public void Init(int playerIndex)
        {
            this.playerIndex = playerIndex;
            leftTileInventory = new TileInventoryData();
            rightTileInventory = new TileInventoryData();
        }
        
        /// <summary>
        /// 捡起一个TileBox放到背包里面
        /// </summary>
        public void TryPickTileBox(TileBase tile)
        {
            if (leftTileInventory.tile == tile || leftTileInventory.tile == null)
            {
                leftTileInventory.tile = tile;
                leftTileInventory.count += 1;

                UpdateLeftInventoryUI();
            }
            else if (rightTileInventory.tile == tile || rightTileInventory.tile == null)
            {
                rightTileInventory.tile = tile;
                rightTileInventory.count += 1;
                
                UpdateRightInventoryUI();
            }
        }
        
        /// <summary>
        /// 生成左手的方块
        /// </summary>
        public void GenerateTileLeft(Vector3 faceDirection)
        {
            if (leftTileInventory.IsEnough())
            {
                GenerateTile(leftTileInventory, faceDirection);
                UpdateLeftInventoryUI();
            }
        }

        /// <summary>
        /// 生成右手的方块
        /// </summary>
        public void GenerateTileRight(Vector3 faceDirection)
        {
            if (rightTileInventory.IsEnough())
            {
                GenerateTile(rightTileInventory, faceDirection);
                UpdateRightInventoryUI();
            }
        }

        /// <summary>
        /// 生成Tile
        /// </summary>
        private void GenerateTile(TileInventoryData tileInventory, Vector3 faceDirection)
        {
            if (GameEntry.Tilemap.GenerateTile(transform.position + Vector3.up * 0.5f + faceDirection * 1.5f, tileInventory.tile))
            {
                tileInventory.count -= 1;
            }
        }

        /// <summary>
        /// 更新左手仓库UI
        /// </summary>
        private void UpdateLeftInventoryUI()
        {
            LevelForm.SetLeftInventory(playerIndex, leftTileInventory.tile.GetTileSprite(), leftTileInventory.count);
        }

        /// <summary>
        /// 更新右手仓库UI
        /// </summary>
        private void UpdateRightInventoryUI()
        {
            LevelForm.SetRightInventory(playerIndex, rightTileInventory.tile.GetTileSprite(), rightTileInventory.count);
        }
    }
}