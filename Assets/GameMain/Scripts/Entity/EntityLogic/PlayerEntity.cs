using System;
using GameFramework.DataTable;
using GameFramework.Fsm;
using UnityEngine;
using UnityEngine.Timeline;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class PlayerEntity : Entity
    {
        [Header("Runtime")] public bool isGround = false;
        public bool isHeadBlock = false;
        public bool isFacePlatform = false;
        public bool isHandPlatform = false;
        public bool isHanging = false;

        /// <summary>
        /// 所有角色统一的参数
        /// </summary>
        public float jumpForce = 10f;
        public float jumpInterval = 0.5f;
        public float shootInterval = 0.5f;
        public float meleeInterval = 0.5f;
        public float boomInterval = 0.5f;

        /// <summary>
        /// Config
        /// </summary>
        private float walkSpeed;

        private float crouchSpeed; // 下蹲速度，可能不做

        private PlayerEntityData m_PlayerEntityData;
        private PlayerInputHandle m_PlayerInputHandle;
        private IFsm<PlayerEntity> m_FSM;

        [HideInInspector] public Rigidbody2D rigid;
        [HideInInspector] public CapsuleCollider2D capsuleCollider;

        private float m_LastJumpTime = 0f;
        private float m_LastShootTime = 0f;
        private float m_LastMeleeTime = 0f;
        private float m_LastBoomTime = 0f;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            // 装备数据和输入句柄
            m_PlayerEntityData = userData as PlayerEntityData;
            m_PlayerInputHandle = GameEntry
                .PlayerInput
                .playerInputHandleList[m_PlayerEntityData.PlayerIndex];

            // 获取自身组件
            rigid = GetComponent<Rigidbody2D>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();
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
            transform.position = Vector3.zero;

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

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            CheckPhysics();
            UpdateIsHandPlatform();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // 拾取地图方块
            if (other.gameObject.CompareTag(Constant.Tag.TileBox))
            {
                TileBoxEntity tileBoxEntity = other.gameObject.GetComponent<TileBoxEntity>();
                Log.Debug($"拾取到了：{tileBoxEntity.Data.Tile.name}");
                GameEntry.Entity.HideEntity(tileBoxEntity);
                // TODO 放置到仓库
            }
        }

        /// <summary>
        /// 射击
        /// </summary>
        public void Shoot()
        {
            if (Time.time - m_LastShootTime < shootInterval) return;
            if (m_PlayerInputHandle.IsShoot == false) return;
            m_LastShootTime = Time.time;
            
            Vector2 pos = transform.position;
            float face = transform.localScale.x;
            Vector2 faceDir = Vector2.right * face;
            RaycastHit2D shootHit =
                PhysicsUtility.Raycast2D(pos + faceDir * 0.2f + Vector2.up * (capsuleCollider.size.y / 2f)
                    , faceDir
                    , 10f
                    , GameEntry.Layer.groundLayer);
            if (shootHit)
            {
                // Log.Info($"目标是：{shootHit.collider.name}, 打在：{shootHit.point}");
                if (shootHit.collider.CompareTag(Constant.Tag.Tilemap))
                {
                    GameEntry.Tilemap.AttackTile(shootHit.collider.gameObject, shootHit.point - shootHit.normal * 0.5f);
                }
            }
        }

        /// <summary>
        /// 地面上面的移动
        /// </summary>
        public void GroundMovement()
        {
            float moveX = m_PlayerInputHandle.Movement.x;
            rigid.velocity = new Vector2(moveX * walkSpeed, rigid.velocity.y);
        }

        /// <summary>
        /// 跳跃
        /// </summary>
        public void Jump()
        {
            if (Time.time - m_LastJumpTime < jumpInterval) return;
            if (m_PlayerInputHandle.IsJump == false) return;
            // 在地面上和悬挂状态可以使用
            if ((isGround && isHeadBlock == false)
                || (isGround == false && isHanging == true))
            {
                m_LastJumpTime = Time.time;
                rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
        }

        /// <summary>
        /// 更新朝向
        /// </summary>
        public void UpdateFace()
        {
            float moveX = m_PlayerInputHandle.Movement.x;
            if (moveX < -0.1f)
            {
                transform.localScale = new Vector2(-1f, 1f);
            }
            else if (moveX > 0.1f)
            {
                transform.localScale = new Vector2(1f, 1f);
            }
        }

        /// <summary>
        /// 悬挂
        /// </summary>
        public void Hang()
        {
            // 手抓，不在地面，没有上升速度
            if (isHandPlatform && isGround == false && rigid.velocity.y < 0.5f)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0f);
                rigid.gravityScale = Mathf.Lerp(0.2f, 1f, Mathf.Clamp(rigid.velocity.y, 0f, 0.5f));
                // 如果重力达到了最小值，开始悬挂
                if (rigid.gravityScale <= 0.21f)
                {
                    // 开始悬挂，可以跳跃
                    if (isHanging == false)
                    {
                        m_LastJumpTime = 0f;
                    }
                    isHanging = true;
                }
            }
            // 不在悬挂，恢复重力和悬挂状态
            else
            {
                rigid.gravityScale = 1f;
                isHanging = false;
            }
        }
        
        /// <summary>
        /// 是否手抓着平台，手抓平台滑行一段时间后会进入悬挂
        /// </summary>
        private void UpdateIsHandPlatform()
        {
            if (isFacePlatform == false)
            {
                isHandPlatform = false;
                return;
            }
            float moveX = m_PlayerInputHandle.Movement.x;

            // 手抓判断
            if (moveX > 0.1f && transform.localScale.x > 0.1f)
            {
                isHandPlatform = true;
            }
            else if (moveX < -0.1f && transform.localScale.x < -0.1f)
            {
                isHandPlatform = true;
            }
            else
            {
                isHandPlatform = false;
            }
        }

        /// <summary>
        /// 更新是否在地面
        /// </summary>
        private void CheckPhysics()
        {
            Vector2 pos = transform.position;
            float height = capsuleCollider.size.y;

            // 地面判断
            RaycastHit2D leftFoot = PhysicsUtility.Raycast2D(pos + Vector2.left * 0.2f, Vector2.down, 0.1f,
                GameEntry.Layer.groundLayer);
            RaycastHit2D rightFoot = PhysicsUtility.Raycast2D(pos + Vector2.right * 0.2f, Vector2.down, 0.1f,
                GameEntry.Layer.groundLayer);
            if (leftFoot || rightFoot)
            {
                isGround = true;
            }
            else
            {
                isGround = false;
            }

            // 头顶判断
            RaycastHit2D upHead = PhysicsUtility.Raycast2D(pos + Vector2.up * height, Vector2.up, 0.4f,
                GameEntry.Layer.groundLayer);
            if (upHead)
            {
                isHeadBlock = true;
            }
            else
            {
                isHeadBlock = false;
            }

            // 悬挂判断
            float face = transform.localScale.x;
            Vector2 faceDir = Vector2.right * face;
            RaycastHit2D hangUpHit =
                PhysicsUtility.Raycast2D(pos + faceDir * 0.2f + Vector2.up * (height / 2f + 0.2f)
                    , faceDir
                    , 0.1f
                    , GameEntry.Layer.groundLayer);
            RaycastHit2D hangDownHit =
                PhysicsUtility.Raycast2D(pos + faceDir * 0.2f + Vector2.up * (height / 2f - 0.2f)
                    , faceDir
                    , 0.1f
                    , GameEntry.Layer.groundLayer);
            if (hangUpHit || hangDownHit)
            {
                isFacePlatform = true;
            }
            else
            {
                isFacePlatform = false;
            }
        }
    }
}