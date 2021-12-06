using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class PlayerMovementState : FsmState<PlayerEntity>
    {
        private PlayerEntity m_Player;

        protected override void OnInit(IFsm<PlayerEntity> fsm)
        {
            base.OnInit(fsm);
            m_Player = fsm.Owner;
        }

        protected override void OnEnter(IFsm<PlayerEntity> fsm)
        {
            base.OnEnter(fsm);
            Log.Info($"当前状态：Player Movement");
        }

        protected override void OnLeave(IFsm<PlayerEntity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
        }

        protected override void OnUpdate(IFsm<PlayerEntity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            
            // 地面移动
            m_Player.GroundMovement();
            // 更新朝向
            m_Player.UpdateFace();
            // 跳跃
            m_Player.Jump();
            // 悬挂
            m_Player.Hang();
            // 射击
            m_Player.Shoot();
        }
    }
}