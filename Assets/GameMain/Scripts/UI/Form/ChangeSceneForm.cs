using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain
{
    public class ChangeSceneForm : UGuiForm
    {
        [Header("Config")] public Image imgProcess;
        public TextMeshProUGUI txtProcess;

        private Material m_MatProcess;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            m_MatProcess = imgProcess.material;
        }

        /// <summary>
        /// set image and text for process bar 
        /// </summary>
        public void SetProcess(float value)
        {
            value = Mathf.Clamp01(value);
            m_MatProcess.SetFloat("_Process", value);
            txtProcess.text = $"{Mathf.RoundToInt(value * 100f)} %";
        }
    }
}