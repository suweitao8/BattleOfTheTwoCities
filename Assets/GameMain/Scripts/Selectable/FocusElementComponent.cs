using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class FocusElementComponent : GameFrameworkComponent
    {
        [Header("Config")] public float updateFocusElementInterval = 0.3f;
        
        [Header("Runtime")]
        [SerializeField] private FocusElement m_CurrentFocusElement;
        public FocusElement CurrentFocusElement
        {
            get => m_CurrentFocusElement;
            private set => m_CurrentFocusElement = value;
        }

        private float m_LastUpdateFocusElementTime;
        private List<FocusElement> m_FocusElementList = new List<FocusElement>();

        private void Start()
        {
            GameEntry.PlayerInput.OnSubmitAction += OnSubmitClicked;
        }

        private void Update()
        {
            UpdateFocusElement();
        }

        /// <summary>
        /// 设置选中对象
        /// </summary>
        public void SetCurrentFocusElement(Selectable selectable)
        {
            // 校验
            if (selectable == null) return;
            if (selectable.gameObject.activeSelf == false) return;
            if (selectable.enabled == false || selectable.interactable == false) return;
            FocusElement focusElement = selectable as FocusElement;
            if (focusElement == null) return;
            
            // 设置当前焦点
            CurrentFocusElement = focusElement;
            CurrentFocusElement.Select();
        }

        /// <summary>
        /// 检查当前焦点的有效性
        /// </summary>
        private void CheckCurrentFocus()
        {
            
        }

        /// <summary>
        /// 更新选中对象
        /// </summary>
        private void UpdateFocusElement()
        {
            if (Time.time - m_LastUpdateFocusElementTime < updateFocusElementInterval) return;
                
            Vector2 movement = GameEntry.PlayerInput.UIMovement;
            float absX = Mathf.Abs(movement.x);
            float absY = Mathf.Abs(movement.y);
            
            // 判断是否会进行选择
            if (Mathf.Max(absX, absY) > 0.5f)
            {
                m_LastUpdateFocusElementTime = Time.time;
            }
            
            // 进行选择
            if (absX > absY)
            {
                if (movement.x > 0.5f)
                {
                    SetCurrentFocusElement(CurrentFocusElement?.FindSelectableOnRight());
                }
                else if (movement.x < -0.5f)
                {
                    SetCurrentFocusElement(CurrentFocusElement?.FindSelectableOnLeft());
                }
            }
            else
            {
                if (movement.y > 0.5f)
                {
                    SetCurrentFocusElement(CurrentFocusElement?.FindSelectableOnUp());
                }
                else if (movement.y < -0.5f)
                {
                    SetCurrentFocusElement(CurrentFocusElement?.FindSelectableOnDown());

                }
            }
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        private void OnSubmitClicked()
        {
            if (CurrentFocusElement == null) return;
            CurrentFocusElement.onClick?.Invoke();
        }
    }
}