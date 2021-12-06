using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using TMPro;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class LocalizationText : MonoBehaviour
    {
        [Header("字典中的Key")] public string key = "";

        private TextMeshProUGUI m_TextMeshProUGUI;

        private void Awake()
        {
            m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            // TODO ReEnable 
            // UpdateUI();
        }

        /// <summary>
        /// 更新UI
        /// </summary>
        public void UpdateUI()
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (m_TextMeshProUGUI != null)
                {
                    m_TextMeshProUGUI.text = GameEntry.Localization.GetString(key);
                }
            }
        }
    }
}
