using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameMain
{
    public class FocusBorderButton : FocusElement
    {
        [Header("Config")] public GameObject borderGameObject;

        private Tweener m_BorderScaleTweener;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (IsSelected)
            {
                ShowBorder();
            }
            else
            {
                HideBorder();
            }
        }
        
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            ShowBorder();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            HideBorder();
        }

        private void ShowBorder()
        {
            borderGameObject.SetActive(true);
            borderGameObject.transform.localScale = Vector3.one * 0.6f;
            m_BorderScaleTweener?.Kill();
            m_BorderScaleTweener = borderGameObject.transform.DOScale(1f, 0.3f)
                .SetEase(Ease.OutBounce);
        }

        private void HideBorder()
        {
            borderGameObject.SetActive(false);
        }
    }
}