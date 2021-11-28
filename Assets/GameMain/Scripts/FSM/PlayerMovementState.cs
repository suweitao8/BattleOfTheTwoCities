using GameFramework.Fsm;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class PlayerMovementState : FsmState<PlayerEntity>
    {
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
            fsm.Owner.GroundMovement();
        }
    }
}