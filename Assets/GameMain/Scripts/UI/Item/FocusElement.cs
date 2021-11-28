using System;
using System.Collections;
using System.Collections.Generic;
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
        // 音效
        public int selectSoundId = -1;
        public int clickSoundId = -1;
        
        // 事件
        public UnityEvent onSelecte;
        public UnityEvent onDeselect;
        public UnityEvent onClick;

        public bool IsSelected => currentSelectionState == SelectionState.Selected;
        
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            onSelecte?.Invoke();
            
            if (selectSoundId != -1)
            {
                GameEntry.Sound.PlayUISound(selectSoundId);
            }
        }
        
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            onDeselect?.Invoke();
        }

        public virtual void OnSubmit(InputAction.CallbackContext context)
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
    }
}