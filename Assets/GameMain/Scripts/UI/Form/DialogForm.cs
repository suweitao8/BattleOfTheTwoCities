//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class DialogForm : UGuiForm
    {
        [SerializeField]
        private Text m_TitleText = null;

        [SerializeField]
        private Text m_MessageText = null;

        [SerializeField] private FocusElement m_FocusConfirm;
        [SerializeField] private TextMeshProUGUI m_TextConfirm;
        [SerializeField] private FocusElement m_FocusCancel;
        [SerializeField] private TextMeshProUGUI m_TextCancel;
        [SerializeField] private FocusElement m_FocusOther;
        [SerializeField] private TextMeshProUGUI m_TextOther;
        
        private int m_DialogMode = 1;
        private bool m_PauseGame = false;
        private object m_UserData = null;
        private GameFrameworkAction<object> m_OnClickConfirm = null;
        private GameFrameworkAction<object> m_OnClickCancel = null;
        private GameFrameworkAction<object> m_OnClickOther = null;

        public int DialogMode
        {
            get
            {
                return m_DialogMode;
            }
        }

        public bool PauseGame
        {
            get
            {
                return m_PauseGame;
            }
        }

        public object UserData
        {
            get
            {
                return m_UserData;
            }
        }

        public void OnConfirmButtonClick()
        {
            Close();

            if (m_OnClickConfirm != null)
            {
                m_OnClickConfirm(m_UserData);
            }
        }

        public void OnCancelButtonClick()
        {
            Close();

            if (m_OnClickCancel != null)
            {
                m_OnClickCancel(m_UserData);
            }
        }

        public void OnOtherButtonClick()
        {
            Close();

            if (m_OnClickOther != null)
            {
                m_OnClickOther(m_UserData);
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

            DialogParams dialogParams = (DialogParams)userData;
            if (dialogParams == null)
            {
                Log.Warning("DialogParams is invalid.");
                return;
            }

            // 刷星焦点数量
            m_DialogMode = dialogParams.Mode;
            RefreshFocusElements();

            // 更新文本和消息
            m_TitleText.text = dialogParams.Title;
            m_MessageText.text = dialogParams.Message;

            // 弹窗时，可以暂停游戏
            m_PauseGame = dialogParams.PauseGame;
            RefreshPauseGame();

            // 甚至还能传数据过来，虽然它也是被传过来的数据
            m_UserData = dialogParams.UserData;

            // 设置焦点的文本和事件
            RefreshConfirmText(dialogParams.ConfirmText);
            m_OnClickConfirm = dialogParams.OnClickConfirm;
            RefreshCancelText(dialogParams.CancelText);
            m_OnClickCancel = dialogParams.OnClickCancel;
            RefreshOtherText(dialogParams.OtherText);
            m_OnClickOther = dialogParams.OnClickOther;
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            if (m_PauseGame)
            {
                GameEntry.Base.ResumeGame();
            }

            m_DialogMode = 1;
            m_TitleText.text = string.Empty;
            m_MessageText.text = string.Empty;
            m_PauseGame = false;
            m_UserData = null;

            RefreshConfirmText(string.Empty);
            m_OnClickConfirm = null;

            RefreshCancelText(string.Empty);
            m_OnClickCancel = null;

            RefreshOtherText(string.Empty);
            m_OnClickOther = null;

            base.OnClose(isShutdown, userData);
        }

        /// <summary>
        /// 刷新焦点数量
        /// </summary>
        private void RefreshFocusElements()
        {
            m_FocusConfirm.gameObject.SetActive(true);
            m_FocusCancel.gameObject.SetActive(true);
            m_FocusOther.gameObject.SetActive(true);

            if (m_DialogMode <= 2)
            {
                m_FocusOther.gameObject.SetActive(false);
            }
            else if (m_DialogMode <= 1)
            {
                m_FocusCancel.gameObject.SetActive(false);
            }
        }

        private void RefreshPauseGame()
        {
            if (m_PauseGame)
            {
                GameEntry.Base.PauseGame();
            }
        }

        private void RefreshConfirmText(string confirmText)
        {
            if (string.IsNullOrEmpty(confirmText))
            {
                confirmText = GameEntry.Localization.GetString("Dialog.ConfirmButton");
            }

            m_TextConfirm.text = confirmText;
        }

        private void RefreshCancelText(string cancelText)
        {
            if (string.IsNullOrEmpty(cancelText))
            {
                cancelText = GameEntry.Localization.GetString("Dialog.CancelButton");
            }

            m_TextCancel.text = cancelText;
        }

        private void RefreshOtherText(string otherText)
        {
            if (string.IsNullOrEmpty(otherText))
            {
                otherText = GameEntry.Localization.GetString("Dialog.OtherButton");
            }

            m_TextOther.text = otherText;
        }
    }
}
