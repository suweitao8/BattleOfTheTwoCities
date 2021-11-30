using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMain
{
    public class BoxColliderGizmos : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + collider.offset.ToVector3XY(), collider.size);
        }
    }
}