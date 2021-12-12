//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameMain
{
    public class ProcedureLevelSelect : ProcedureBase
    {
        // private const float GameOverDelayedSeconds = 2f;
        //
        // private readonly Dictionary<GameMode, GameBase> m_Games = new Dictionary<GameMode, GameBase>();
        // private GameBase m_CurrentGame = null;
        
        public LevelForm levelForm = null;
        
        // private float m_GotoMenuDelaySeconds = 0f;
        //
        // public void GotoMenu()
        // {
        //     m_GotoMenu = true;
        // }
        //
        // protected override void OnInit(ProcedureOwner procedureOwner)
        // {
        //     base.OnInit(procedureOwner);
        //
        //     m_Games.Add(GameMode.Survival, new SurvivalGame());
        // }
        //
        // protected override void OnDestroy(ProcedureOwner procedureOwner)
        // {
        //     base.OnDestroy(procedureOwner);
        //
        //     m_Games.Clear();
        // }
        //
        
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            
            // 打开面板
            GameEntry.UI.OpenUIForm(UIFormId.LevelForm, this);

            for (int i = 0; i < GameEntry.PlayerInput.playerInputHandleList.Count; i++)
            {
                GameEntry.Entity.ShowPlayerEntity(new PlayerEntityData(GameEntry.Entity.GenerateSerialId(), 1001, i, 1));
            }
        }
        
        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            
            if (levelForm != null)
            {
                GameEntry.UI.CloseUIForm(levelForm);
            }
        }
        
        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            // 设置参数
            levelForm = (LevelForm)ne.UIForm.Logic;
            levelForm.SetPlayerCount(GameEntry.PlayerInput.playerInputHandleList.Count);
        }
        
        //
        // protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        // {
        //     base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        //
        //     if (m_CurrentGame != null && !m_CurrentGame.GameOver)
        //     {
        //         m_CurrentGame.Update(elapseSeconds, realElapseSeconds);
        //         return;
        //     }
        //
        //     if (!m_GotoMenu)
        //     {
        //         m_GotoMenu = true;
        //         m_GotoMenuDelaySeconds = 0;
        //     }
        //
        //     m_GotoMenuDelaySeconds += elapseSeconds;
        //     if (m_GotoMenuDelaySeconds >= GameOverDelayedSeconds)
        //     {
        //         procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
        //         ChangeState<ProcedureChangeScene>(procedureOwner);
        //     }
        // }
    }
}
