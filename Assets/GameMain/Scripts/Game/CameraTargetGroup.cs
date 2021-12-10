using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace GameMain
{
    [ExecuteInEditMode]
    public class CameraTargetGroup : MonoBehaviour
    {
        private const float MAX_Y = 1000f;
        
        [Header("限制范围")] public float minX = 0f;
        public float maxX = 100f;
        public float minY = 0f;

        [Header("虚拟相机")]
        public CinemachineVirtualCamera vcam;

        [Header("跟随目标")] 
        public float borderSize = 10f;
        public float MinWidth = 10f;
        public float MinHeight = 5f;
        public List<CameraFollowTarget> targetList = new List<CameraFollowTarget>();

        private float MaxHeight => MAX_Y - minY;
        private float MaxWidth => maxX - minX;

        private void Update()
        {
            if (vcam != null)
            {
                FollowTarget();
                ConfineBox();
            }
        }

        /// <summary>
        /// 设置相机可动范围
        /// </summary>
        public void SetCameraMovableRange(float minX, float maxX, float minY, float borderSize)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.borderSize = borderSize;
            transform.position = Vector3.zero;
        }
        
        /// <summary>
        /// 加入相机跟随的目标
        /// </summary>
        public void AddCameraFollowTarget(CameraFollowTarget target)
        {
            if (targetList.Contains(target)) return;
            
            targetList.Add(target);
        }
        
        /// <summary>
        /// 清空
        /// </summary>
        public void ClearTargetGroup()
        {
            targetList.Clear();
        }
        
        /// <summary>
        /// 跟随目标
        /// </summary>
        private void FollowTarget()
        {
            if (targetList == null || targetList.Count == 0) return;
            
            // 获取边界
            float left = maxX;
            float right = minX;
            float down = MAX_Y;
            float up = minY;
            for (int i = targetList.Count - 1; i >= 0; i--)
            {
                CameraFollowTarget target = targetList[i];
                if (target == null)
                {
                    targetList.RemoveAt(i);
                    continue;
                }
                left = Mathf.Min(left, target.transform.position.x - target.radius);
                right = Mathf.Max(right, target.transform.position.x + target.radius);
                down = Mathf.Min(down, target.transform.position.y - target.radius);
                up = Mathf.Max(up, target.transform.position.y + target.radius);
            }
            left -= borderSize;
            right += borderSize;
            down -= borderSize;
            up += borderSize;
            left = Mathf.Max(left, minX);
            right = Mathf.Min(right, maxX);
            down = Mathf.Max(down, minY);
            up = Mathf.Min(up, MAX_Y);
            
            
            // TODO 计算最大 Orthographisc，计算出错
            float width = right - left;
            float height = up - down;
            float targetHeight = Mathf.Max(width / vcam.m_Lens.Aspect, height);
            targetHeight = Mathf.Min(Mathf.Clamp(targetHeight * vcam.m_Lens.Aspect, MinWidth, MaxWidth) / vcam.m_Lens.Aspect
                            , Mathf.Clamp(targetHeight, MinHeight, MaxHeight));
            vcam.m_Lens.OrthographicSize = targetHeight / 2f;
            
            // 更新位置到中心店
            transform.position = new Vector3((right - left) / 2f + left, (up - down) / 2f + down, 0f);
        }

        /// <summary>
        /// 限制在范围内
        /// </summary>
        private void ConfineBox()
        {
            Vector3 pos = transform.position;
            float dy = vcam.m_Lens.OrthographicSize;
            float dx = dy * vcam.m_Lens.Aspect;

            Vector2 min = new Vector2(minX + dx, minY + dy);
            Vector2 max = new Vector2(maxX - dx, MAX_Y - dy);

            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);

            transform.position =  pos;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3 center = new Vector3((minX + maxX) / 2f, (minY + MAX_Y) / 2f, 0f);
            Vector3 size = new Vector3(maxX - minX, MAX_Y - minY, 0f);
            Gizmos.DrawWireCube(center, size);
        }
    }
}
