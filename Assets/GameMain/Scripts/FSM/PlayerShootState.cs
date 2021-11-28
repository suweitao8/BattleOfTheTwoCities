using GameFramework.Fsm;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class PlayerShootState : FsmState<PlayerEntity>
    {
        protected override void OnInit(IFsm<PlayerEntity> fsm)
        {
            base.OnInit(fsm);
        }

        protected override void OnEnter(IFsm<PlayerEntity> fsm)
        {
            base.OnEnter(fsm);
            Log.Info($"当前状态：Player Shoot");
        }

        protected override void OnLeave(IFsm<PlayerEntity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
        }
    }
}