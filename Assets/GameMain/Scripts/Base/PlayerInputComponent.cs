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
        public bool IsShoot { get; set; }
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

        public bool IsLT { get; set; }
        public bool IsLB { get; set; }
        public bool IsRT { get; set; }
        public bool IsRB { get; set; }
        
        public bool IsStart { get; set; }
        public bool IsBack { get; set; }

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
            m_PlayerInputActions.Game.Jump.performed += OnJump;
            m_PlayerInputActions.Game.Melee.performed += OnMelee;
            m_PlayerInputActions.Game.Boom.performed += OnBoom;
            
            m_PlayerInputActions.Game.LT.performed += OnLT;
            m_PlayerInputActions.Game.LB.performed += OnLB;
            m_PlayerInputActions.Game.RT.performed += OnRT;
            m_PlayerInputActions.Game.RB.performed += OnRB;
            
            m_PlayerInputActions.Game.Start.performed += OnStart;
            m_PlayerInputActions.Game.Back.performed += OnBack;

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
            playerInputHandleList[playerIndex].IsShoot = context.ReadValueAsButton();
        }

        /// <summary>
        /// 跳跃
        /// </summary>
        private void OnJump(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsJump = context.ReadValueAsButton();
        }
        
        /// <summary>
        /// 近战
        /// </summary>
        private void OnMelee(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsMelee = context.ReadValueAsButton();
        }
        
        /// <summary>
        /// 大招
        /// </summary>
        private void OnBoom(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsBoom = context.ReadValueAsButton();
        }
        
        /// <summary>
        /// LT
        /// </summary>
        private void OnLT(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsLT = context.ReadValueAsButton();
        }
        
        /// <summary>
        /// LB
        /// </summary>
        private void OnLB(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsLB = context.ReadValueAsButton();
        }
        
        /// <summary>
        /// RT
        /// </summary>
        private void OnRT(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsRT = context.ReadValueAsButton();
        }
        
        /// <summary>
        /// RB
        /// </summary>
        private void OnRB(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsRB = context.ReadValueAsButton();
        }
        
        /// <summary>
        /// 开始
        /// </summary>
        private void OnStart(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsStart = context.ReadValueAsButton();
        }
        
        /// <summary>
        /// 选择按钮
        /// </summary>
        private void OnBack(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsBack = context.ReadValueAsButton();
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