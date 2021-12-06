using System;
using UnityEngine;

namespace GameMain
{
    public class CameraFollowTarget :  MonoBehaviour
    {
        [Header("Config")]
        public Transform transform;
        public float radius;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}