using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class TileBoxEntity : Entity
    {
        public TileBoxEntityData Data { get; private set; }

        private Rigidbody2D m_Rigid;
        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Rigid = GetComponent<Rigidbody2D>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            // 数据
            Data = userData as TileBoxEntityData;
            
            // 应用数据
            transform.position = Data.Position;
            m_Rigid.velocity = Data.velocity;
            // 如果Y轴上的有足够的偏移，应用角速度
            if (Data.velocity.x > 0f)
            {
                m_Rigid.angularVelocity = Data.velocity.magnitude * 60f;
                Log.Info($"当前生成盒子速度（angular）： {m_Rigid.velocity} => {m_Rigid.angularVelocity}");
            }
            else
            {
                m_Rigid.angularVelocity = -Data.velocity.magnitude * 60f;
                Log.Info($"当前生成盒子速度（no angular）： {m_Rigid.velocity} => {m_Rigid.angularVelocity}");
            }
        }
    }
}