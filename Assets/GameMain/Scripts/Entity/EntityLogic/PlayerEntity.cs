using GameFramework.DataTable;
using GameFramework.Fsm;
using UnityEngine;

namespace GameMain
{
    public class PlayerEntity : Entity
    {
        /// <summary>
        /// Config
        /// </summary>
        private float walkSpeed;
        private float crouchSpeed;
        
        private PlayerEntityData m_PlayerEntityData;
        private PlayerInputHandle m_PlayerInputHandle;
        private IFsm<PlayerEntity> m_FSM;

        private Rigidbody2D m_Rigidbody;
        private CapsuleCollider2D m_CapsuleCollider;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            // 装备数据和输入句柄
            m_PlayerEntityData = userData as PlayerEntityData;
            m_PlayerInputHandle = GameEntry
                .PlayerInput
                .playerInputHandleList[m_PlayerEntityData.PlayerIndex];
            
            // 获取自身组件
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_CapsuleCollider = GetComponent<CapsuleCollider2D>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_PlayerEntityData = userData as PlayerEntityData;
            
            // 参数
            IDataTable<DRCharacter> dtCharacter = GameEntry.DataTable.GetDataTable<DRCharacter>();
            DRCharacter drCharacter = dtCharacter.GetDataRow(m_PlayerEntityData.CharacterId);
            walkSpeed = drCharacter.WalkSpeed;
            crouchSpeed = drCharacter.CrouchSpeed;
            
            // 状态机
            m_FSM = GameEntry.Fsm.CreateFsm(this
                , new PlayerMovementState()
                , new PlayerShootState());
            m_FSM.Start<PlayerMovementState>();
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            
            GameEntry.Fsm.DestroyFsm(m_FSM);
        }

        /// <summary>
        /// 地面上面的移动
        /// </summary>
        public void GroundMovement()
        {
            float moveX = m_PlayerInputHandle.Movement.x;
            m_Rigidbody.velocity = new Vector2(moveX * walkSpeed, m_Rigidbody.velocity.y);
        }
    }
}