using System;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Localization;
using GameMain;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = GameMain.GameEntry;

namespace GameMain
{
    public class PlayerInputHandle
    {
        /// <summary>
        /// 手柄索引和手柄的设备Id
        /// </summary>
        public int index;
        public int deviceId;
        
        /// <summary>
        /// 移动量
        /// </summary>
        public Vector2 Movement { get; set; }
        /// <summary>
        /// 按下射击
        /// </summary>
        public bool IsShooting { get; set; }
        /// <summary>
        /// 按下近战
        /// </summary>
        public bool IsMelee { get; set; }
        /// <summary>
        /// 按下炸弹
        /// </summary>
        public bool IsBoom { get; set; }
        /// <summary>
        /// 按下跳起
        /// </summary>
        public bool IsJump { get; set; }

        public PlayerInputHandle(int index, int deviceId)
        {
            this.index = index;
            this.deviceId = deviceId;
            Log.Info($"新玩家加入, 索引({index}, 设备({deviceId})");
        }
    }
    
    public class PlayerInputComponent : GameFrameworkComponent
    {
        /// <summary>
        /// 输入句柄
        /// </summary>
        public List<PlayerInputHandle> playerInputHandleList = new List<PlayerInputHandle>();

        /// <summary>
        /// UI
        /// </summary>
        public Vector2 UIMovement { get; set; }
        public event Action OnSubmitAction;
        public event Action OnCancelAction;

        private PlayerInputActions m_PlayerInputActions;

        protected override void Awake()
        {
            base.Awake();

            // 创建输入映射
            m_PlayerInputActions = new PlayerInputActions();

            // Game
            m_PlayerInputActions.Game.Move.performed += OnGameMove;
            m_PlayerInputActions.Game.Shoot.performed += OnShoot;

            // UI
            m_PlayerInputActions.UI.Move.performed += OnUIMove;
            m_PlayerInputActions.UI.Submit.performed += OnSubmit;
            m_PlayerInputActions.UI.Cancel.performed += OnCancel;
        }

        private void OnEnable()
        {
            m_PlayerInputActions.Enable();
        }

        private void OnDisable()
        {
            m_PlayerInputActions.Disable();
        }

        #region Game

        /// <summary>
        /// 移动
        /// </summary>
        private void OnGameMove(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].Movement = context.ReadValue<Vector2>();
        }

        /// <summary>
        /// 射击
        /// </summary>
        private void OnShoot(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsShooting = context.ReadValueAsButton();
        }

        /// <summary>
        /// 尝试加入一个玩家输入句柄
        /// </summary>
        private int TryAddPlayerInputHandle(InputAction.CallbackContext context)
        {
            for (int i = 0; i < playerInputHandleList.Count; i++)
            {
                if (playerInputHandleList[i].deviceId == context.control.device.deviceId)
                {
                    return i;
                }
            }

            PlayerInputHandle handle = new PlayerInputHandle(playerInputHandleList.Count,
                context.control.device.deviceId);
            playerInputHandleList.Add(handle);
            return handle.index;
        }

        #endregion

        #region UI

        /// <summary>
        /// 移动
        /// </summary>
        private void OnUIMove(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            if (playerIndex != 0) return;
            UIMovement = context.ReadValue<Vector2>();
        }
        
        private void OnSubmit(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            if (playerIndex != 0) return;
            if (context.ReadValueAsButton() == false) return;
            Log.Info($"OnConfirm");
            OnSubmitAction?.Invoke();
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            if (playerIndex != 0) return;
            if (context.ReadValueAsButton() == false) return;
            Log.Info($"OnCancel");
            OnCancelAction?.Invoke();
        }

        #endregion
    }
}