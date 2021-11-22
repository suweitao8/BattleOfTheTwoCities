using System;
using GameFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class PlayerInputComponent : GameFrameworkComponent
{
    /// <summary>
    /// Game
    /// </summary>
    public Vector2 Movement { get; set; }
    public bool IsShooting { get; set; }
    public bool IsMelee { get; set; }
    public bool IsBoom { get; set; }
    public bool IsJump { get; set; }

    /// <summary>
    /// UI
    /// </summary>
    public event Action OnLeftAction;
    public event Action OnRightAction;
    public event Action OnUpAction;
    public event Action OnDownAction;
    public event Action OnConfirmAction;
    public event Action OnCancelAction;
    
    private PlayerInputActions m_PlayerInputActions;

    protected override void Awake()
    {
        base.Awake();
        
        // 创建输入映射
        m_PlayerInputActions = new PlayerInputActions();
        
        // Game
        m_PlayerInputActions.Game.Move.performed += OnMove;
        m_PlayerInputActions.Game.Shoot.performed += OnShoot;
        
        // UI
        m_PlayerInputActions.UI.Left.performed += OnLeft;
        m_PlayerInputActions.UI.Right.performed += OnRight;
        m_PlayerInputActions.UI.Up.performed += OnUp;
        m_PlayerInputActions.UI.Down.performed += OnDown;
        m_PlayerInputActions.UI.Confirm.performed += OnConfirm;
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
    private void OnMove(InputAction.CallbackContext context)
    {
        // Log.Info($"OnMove: {context.ReadValue<Vector2>()}");
        Movement = context.ReadValue<Vector2>();
    }
    
    /// <summary>
    /// 射击
    /// </summary>
    private void OnShoot(InputAction.CallbackContext context)
    {
        // Log.Info($"OnShoot: {context.ReadValueAsButton()}");
        IsShooting = context.ReadValueAsButton();
    }

    #endregion

    #region UI

    private void OnLeft(InputAction.CallbackContext context)
    {
        OnLeftAction?.Invoke();
    }
    
    private void OnRight(InputAction.CallbackContext context)
    {
        OnRightAction?.Invoke();
    }
    
    private void OnUp(InputAction.CallbackContext context)
    {
        OnUpAction?.Invoke();
    }
    
    private void OnDown(InputAction.CallbackContext context)
    {
        OnDownAction?.Invoke();
    }
    
    private void OnConfirm(InputAction.CallbackContext context)
    {
        OnConfirmAction?.Invoke();
    }
    
    private void OnCancel(InputAction.CallbackContext context)
    {
        OnCancelAction?.Invoke();
    }

    #endregion
}