using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace GameMain
{
    public class CameraConfiner : CinemachineExtension
    {
        public BoxCollider2D collider;
        
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam
            , CinemachineCore.Stage stage
            , ref CameraState state
            , float deltaTime)
        {
            if (collider != null)
            {
                vcam.Follow.position = ConfineScreenEdge(vcam.Follow.position, ref state);
            }
        }

        private Vector3 ConfineScreenEdge(Vector3 pos, ref CameraState state)
        {
            float dy = state.Lens.OrthographicSize;
            float dx = dy * state.Lens.Aspect;

            Vector2 colliderSizeHalf = collider.size / 2f;
            colliderSizeHalf.x -= dx;
            colliderSizeHalf.y -= dy;
            Vector2 colliderPos = collider.transform.position.ToVector2XY() + collider.offset;
            Vector2 min = colliderPos - colliderSizeHalf;
            Vector2 max = colliderPos + colliderSizeHalf;
            
            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);
            
            return pos;
        }
    }
}
