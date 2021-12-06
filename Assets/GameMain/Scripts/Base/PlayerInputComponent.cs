using System;
using System.Collections.Generic;
using System.Linq;
using GameFramework;
using GameFramework.Localization;
using GameMain;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = GameMain.GameEntry;

namespace GameMain
{
    public class PlayerInputComponent : GameFrameworkComponent
    {
        /// <summary>
        /// 输入句柄
        /// </summary>
        public List<PlayerInputHandle> playerInputHandleList = new List<PlayerInputHandle>();

        public PlayerInputHandle this[int index]
        {
            get
            {
                if (index < playerInputHandleList.Count)
                {
                    return playerInputHandleList[index];
                }

                return null;
            }
        }

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

        private void Update()
        {
            for (int i = 0; i < playerInputHandleList.Count; i++)
            {
                playerInputHandleList[i].OnUpdate();
            }
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
                context.control.device);
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
            if (playerInputHandleList[playerIndex].IsShoot == true)
                playerInputHandleList[playerIndex].OnShoot?.Invoke();
        }

        /// <summary>
        /// 跳跃
        /// </summary>
        private void OnJump(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsJump = context.ReadValueAsButton();
            if (playerInputHandleList[playerIndex].IsJump == true) playerInputHandleList[playerIndex].OnJump?.Invoke();
        }

        /// <summary>
        /// 近战
        /// </summary>
        private void OnMelee(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsMelee = context.ReadValueAsButton();
            if (playerInputHandleList[playerIndex].IsMelee == true)
                playerInputHandleList[playerIndex].OnMelee?.Invoke();
        }

        /// <summary>
        /// 大招
        /// </summary>
        private void OnBoom(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsBoom = context.ReadValueAsButton();
            if (playerInputHandleList[playerIndex].IsBoom == true) playerInputHandleList[playerIndex].OnBoom?.Invoke();
        }

        /// <summary>
        /// LT
        /// </summary>
        private void OnLT(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsLT = context.ReadValueAsButton();
            if (playerInputHandleList[playerIndex].IsLT == true) playerInputHandleList[playerIndex].OnLT?.Invoke();
        }

        /// <summary>
        /// LB
        /// </summary>
        private void OnLB(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsLB = context.ReadValueAsButton();
            if (playerInputHandleList[playerIndex].IsLB == true) playerInputHandleList[playerIndex].OnLB?.Invoke();
        }

        /// <summary>
        /// RT
        /// </summary>
        private void OnRT(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsRT = context.ReadValueAsButton();
            if (playerInputHandleList[playerIndex].IsRT == true) playerInputHandleList[playerIndex].OnRT?.Invoke();
        }

        /// <summary>
        /// RB
        /// </summary>
        private void OnRB(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsRB = context.ReadValueAsButton();
            if (playerInputHandleList[playerIndex].IsRB == true) playerInputHandleList[playerIndex].OnRB?.Invoke();
        }

        /// <summary>
        /// 开始
        /// </summary>
        private void OnStart(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsStart = context.ReadValueAsButton();
            if (playerInputHandleList[playerIndex].IsStart == true)
                playerInputHandleList[playerIndex].OnStart?.Invoke();
        }

        /// <summary>
        /// 选择按钮
        /// </summary>
        private void OnBack(InputAction.CallbackContext context)
        {
            int playerIndex = TryAddPlayerInputHandle(context);
            playerInputHandleList[playerIndex].IsBack = context.ReadValueAsButton();
            if (playerInputHandleList[playerIndex].IsBack == true) playerInputHandleList[playerIndex].OnBack?.Invoke();
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