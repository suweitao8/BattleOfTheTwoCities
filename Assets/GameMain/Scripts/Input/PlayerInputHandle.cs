using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class PlayerInputHandle
    {
        /// <summary>
        /// 手柄索引和手柄的设备Id
        /// </summary>
        public int index;
        public int deviceId;
        public Gamepad gamepad;

        /// <summary>
        /// 移动量
        /// </summary>
        public Vector2 Movement { get; set; }

        /// <summary>
        /// Right Pad
        /// </summary>
        public bool IsShoot { get; set; }
        public bool IsMelee { get; set; }
        public bool IsBoom { get; set; }
        public bool IsJump { get; set; }
        public Action OnShoot;
        public Action OnMelee;
        public Action OnBoom;
        public Action OnJump;

        /// <summary>
        /// Backward
        /// </summary>
        public bool IsLT { get; set; }
        public bool IsLB { get; set; }
        public bool IsRT { get; set; }
        public bool IsRB { get; set; }
        public Action OnLT;
        public Action OnLB;
        public Action OnRT;
        public Action OnRB;


        /// <summary>
        /// Middle Pad
        /// </summary>
        public bool IsStart { get; set; }
        public bool IsBack { get; set; }
        public Action OnStart;
        public Action OnBack;

        private float m_LowFrequency = 0f;

        public float LowFrequency
        {
            get { return m_LowFrequency; }
            set
            {
                if (m_LowFrequency == value)
                {
                    return;
                }

                m_LowFrequency = value;
                gamepad.SetMotorSpeeds(LowFrequency, Highrequency);
            }
        }

        private float m_HighFrequency = 0f;

        public float Highrequency
        {
            get { return m_HighFrequency; }
            set
            {
                if (m_HighFrequency == value)
                {
                    return;
                }

                m_HighFrequency = value;
                gamepad.SetMotorSpeeds(LowFrequency, Highrequency);
            }
        }

        // Shake
        private GamepadShakeType m_ShakeType = GamepadShakeType.Low;
        private float m_StopShakeTime = -1f;

        public PlayerInputHandle(int index, InputDevice device)
        {
            this.index = index;
            this.gamepad = device as Gamepad;
            this.deviceId = device.deviceId;
            Log.Info($"新玩家加入, 索引({index}, 设备({deviceId})");
        }

        public void OnUpdate()
        {
            if (Time.time > m_StopShakeTime)
            {
                LowFrequency = 0f;
                Highrequency = 0f;
                return;
            }

            switch (m_ShakeType)
            {
                case GamepadShakeType.Low:
                    LowFrequency = 0f;
                    Highrequency = 0.2f;
                    break;
                case GamepadShakeType.Middle:
                    LowFrequency = 0.2f;
                    Highrequency = 0.4f;
                    break;
                case GamepadShakeType.High:
                    LowFrequency = 0.4f;
                    Highrequency = 0.6f;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 设置手柄震动信息
        /// </summary>
        public void TriggerShake(GamepadShakeType shakeType, float duration)
        {
            m_ShakeType = shakeType;
            m_StopShakeTime = Time.time + duration;
        }
        
        /// <summary>
        /// 使用默认的震动参数
        /// </summary>
        public void TriggerShake(GamepadShakeType shakeType)
        {
            switch (m_ShakeType)
            {
                case GamepadShakeType.Low:
                    TriggerShake(shakeType, 0.2f);
                    break;
                case GamepadShakeType.Middle:
                    TriggerShake(shakeType, 0.4f);
                    break;
                case GamepadShakeType.High:
                    TriggerShake(shakeType, 0.6f);
                    break;
                default:
                    break;
            } 
        }
    }
}