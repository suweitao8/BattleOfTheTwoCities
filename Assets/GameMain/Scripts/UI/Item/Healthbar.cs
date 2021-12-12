using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain
{
    public class Healthbar : MonoBehaviour
    {
        [Header("Config")] public Image healthDelayVolume;
        public Image healthVolume;
        public float tweenTime = 1f;

        [Header("Runtime")] public float health = 1f;
        public float targetHealth = 1f;
        
        private Tweener m_HealthDelayTweener;

        private void Update()
        {
            if (targetHealth != health)
            {
                float diff = targetHealth - health;
                targetHealth = health;
                m_HealthDelayTweener?.Kill();
                healthVolume.fillAmount = targetHealth;
                m_HealthDelayTweener = DOTween.To(() => healthDelayVolume.fillAmount,
                    v => healthDelayVolume.fillAmount = v,
                    targetHealth,
                    Mathf.Abs(diff) * tweenTime).SetEase(Ease.InCubic);
            }   
        }
    }
}