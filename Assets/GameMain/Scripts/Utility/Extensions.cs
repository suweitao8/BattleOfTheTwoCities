using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameMain
{
    public static class Extensions
    {
        public static RectTransform ToRect(this Transform transform)
        {
            return transform as RectTransform;
        }

        public static Sprite GetTileSprite(this TileBase tile)
        {
            RuleTile ruleTile = tile as RuleTile;
            if (ruleTile == null)
            {
                return null;
            }

            return ruleTile.m_DefaultSprite;
        }

        public static void Clear<T>(this T[] array) where T : class
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = null;
            }
        }
    }
}