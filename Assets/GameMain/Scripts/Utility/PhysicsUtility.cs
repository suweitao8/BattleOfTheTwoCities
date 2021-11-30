using UnityEngine;

namespace GameMain
{
    public static class PhysicsUtility
    {
        /// <summary>
        /// 射线
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="rayDirection"></param>
        /// <param name="length"></param>
        /// <param name="layerMask"></param>
        /// <returns></returns>
        public static RaycastHit2D Raycast2D(Vector2 original
            , Vector2 direction
            , float length
            , LayerMask layerMask)
        {
            RaycastHit2D hit = Physics2D.Raycast(original, direction, length, layerMask);
            Debug.DrawRay(original, direction * length, hit? Color.red : Color.green);
            return hit;
        }
    }
}