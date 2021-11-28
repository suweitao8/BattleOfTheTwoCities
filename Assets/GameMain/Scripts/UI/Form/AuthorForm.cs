//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class AuthorForm : UGuiForm
    {
        [Header("Config")] [SerializeField] private RectTransform m_Content;
        [SerializeField] private float m_TargetAnchorY = 800f;
        [SerializeField] private float m_TweenSpeed = 80f;

        private Tweener m_ShowTweener;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_ShowTweener?.Kill();
            m_Content.anchoredPosition = Vector2.zero;
            m_ShowTweener = m_Content.DOAnchorPosY(m_TargetAnchorY, m_TargetAnchorY / m_TweenSpeed)
                .SetEase(Ease.InOutSine);
        }
    }
}
