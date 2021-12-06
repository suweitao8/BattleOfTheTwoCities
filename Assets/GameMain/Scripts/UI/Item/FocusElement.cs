using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityGameFramework.Runtime;

namespace GameMain
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FocusElement : Selectable
    {
        [Header("权重")]
        public int priority = 0;
        
        [Header("音效")]
        public int selectSoundId = -1;
        public int clickSoundId = -1;

        [Header("事件")]
        public UnityEvent onSelecte;
        public UnityEvent onDeselect;
        public UnityEvent onClick;

        public bool IsSelected => currentSelectionState == SelectionState.Selected;

        private Animator m_Animator;
        
        protected override void Awake()
        {
            base.Awake();
            m_Animator = GetComponent<Animator>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            GameEntry.FocusElement?.RegisterFocusElement(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GameEntry.FocusElement?.DeregisterFocusElement(this);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            // 播放动画
            AnimatorTrigger("Select");
            
            // 调用方法
            onSelecte?.Invoke();
            
            // 播放音效
            if (selectSoundId != -1)
            {
                GameEntry.Sound.PlayUISound(selectSoundId);
            }
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            
            // 调用方法
            onDeselect?.Invoke();

            // 播放动画
            AnimatorTrigger("Deselect");
        }

        public void OnSubmit()
        {
            if (IsSelected)
            {
                onClick?.Invoke();
                if (clickSoundId != -1)
                {
                    GameEntry.Sound.PlayUISound(clickSoundId);
                }
            }
        }

        private void AnimatorTrigger(string trigger)
        {
            if (m_Animator != null && m_Animator.IsDestroyed() == false)
            {
                m_Animator.SetTrigger(trigger);
            }
        }
    }
}