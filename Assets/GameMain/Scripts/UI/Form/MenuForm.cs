//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class MenuForm : UGuiForm
    {
        private ProcedureMenu m_ProcedureMenu = null;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_ProcedureMenu = (ProcedureMenu)userData;
            if (m_ProcedureMenu == null)
            {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }
        }

        public void OnBtnStartClicked()
        {
            // Load Level Select
            m_ProcedureMenu.StartGame();
        }

        public void OnBtnSettingClicked()
        {
            GameEntry.UI.OpenUIForm(UIFormId.SettingForm);
        }

        public void OnBtnAuthorClicked()
        {
            GameEntry.UI.OpenUIForm(UIFormId.AuthorForm);
            
            // TODO Test
            if (GameEntry.PlayerInput[0] != null)
            {
                GameEntry.PlayerInput[0].OnShoot = () =>
                {
                    GameEntry.PlayerInput[0].TriggerShake(GamepadShakeType.Low);
                };
                GameEntry.PlayerInput[0].OnMelee = () =>
                {
                    GameEntry.PlayerInput[0].TriggerShake(GamepadShakeType.Middle);
                };
                GameEntry.PlayerInput[0].OnBoom = () =>
                {
                    GameEntry.PlayerInput[0].TriggerShake(GamepadShakeType.High);
                };
            }
        }

        public void OnBtnQuitGameClicked()
        {
            GameEntry.UI.OpenDialog(new DialogParams()
            {
                Mode = 2,
                Title = GameEntry.Localization.GetString("AskQuitGame.Title"),
                Message = GameEntry.Localization.GetString("AskQuitGame.Message"),
                OnClickConfirm = delegate(object userData)
                {
                    UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit);
                },
            });
        }
        
//         [SerializeField]
//         private GameObject m_QuitButton = null;
//
//         private ProcedureMenu m_ProcedureMenu = null;
//
//         public void OnStartButtonClick()
//         {
//             m_ProcedureMenu.StartGame();
//         }
//
//         public void OnSettingButtonClick()
//         {
//             GameEntry.UI.OpenUIForm(UIFormId.SettingForm);
//         }
//
//         public void OnAboutButtonClick()
//         {
//             GameEntry.UI.OpenUIForm(UIFormId.AboutForm);
//         }
//
//         public void OnQuitButtonClick()
//         {
//             GameEntry.UI.OpenDialog(new DialogParams()
//             {
//                 Mode = 2,
//                 Title = GameEntry.Localization.GetString("AskQuitGame.Title"),
//                 Message = GameEntry.Localization.GetString("AskQuitGame.Message"),
//                 OnClickConfirm = delegate (object userData) { UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit); },
//             });
//         }
//
// #if UNITY_2017_3_OR_NEWER
//         protected override void OnOpen(object userData)
// #else
//         protected internal override void OnOpen(object userData)
// #endif
//         {
//             base.OnOpen(userData);
//
//             m_ProcedureMenu = (ProcedureMenu)userData;
//             if (m_ProcedureMenu == null)
//             {
//                 Log.Warning("ProcedureMenu is invalid when open MenuForm.");
//                 return;
//             }
//
//             m_QuitButton.SetActive(Application.platform != RuntimePlatform.IPhonePlayer);
//         }
//
// #if UNITY_2017_3_OR_NEWER
//         protected override void OnClose(bool isShutdown, object userData)
// #else
//         protected internal override void OnClose(bool isShutdown, object userData)
// #endif
//         {
//             m_ProcedureMenu = null;
//
//             base.OnClose(isShutdown, userData);
//         }
    }
}
