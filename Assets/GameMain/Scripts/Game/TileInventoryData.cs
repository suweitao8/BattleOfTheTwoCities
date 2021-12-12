using UnityEngine.Tilemaps;

namespace GameMain
{
    [System.Serializable]
    public class TileInventoryData
    {
        public TileBase tile;
        public int count = 0;

        public TileInventoryData()
        {
            tile = null;
            count = 0;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEnough()
        {
            return tile != null && count > 0;
        }
    }
}