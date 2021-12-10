using System;
using UnityEngine;

namespace GameMain
{
    public class PlayerInventory : MonoBehaviour
    {
        [Header("Runtime")]
        public InventoryTileData inventoryTileLeft;
        public InventoryTileData inventoryTileRight;

        private void OnEnable()
        {
            InitData();
        }

        public void InitData()
        {
            inventoryTileLeft = new InventoryTileData();
            inventoryTileRight = new InventoryTileData();
        }
        
        public void TryPickTileBox() 
        {
        }
    }
}