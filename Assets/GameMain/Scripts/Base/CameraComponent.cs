using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class CameraComponent : GameFrameworkComponent
    {
        [Header("Config")] public Volume globalVolume;
        public Camera mainCamera;
        public CinemachineVirtualCamera cinemachineVirtualCamera;
        public CameraTargetGroup cameraTargetGroup;

        /// <summary>
        /// 设置相机可动范围
        /// </summary>
        public void SetCameraMovableRange(float minX, float maxX, float minY, float borderSize)
        {
            cameraTargetGroup.SetCameraMovableRange(minX, maxX, minY, borderSize);
        }
        
        /// <summary>
        /// 清空目标组
        /// </summary>
        public void ClearTargetGroup()
        {
            cameraTargetGroup.ClearTargetGroup();
        }
        
        /// <summary>
        /// 加入相机跟随的目标
        /// </summary>
        public void AddCameraFollowTarget(CameraFollowTarget target)
        {
            cameraTargetGroup.AddCameraFollowTarget(target);
        }
    }
}