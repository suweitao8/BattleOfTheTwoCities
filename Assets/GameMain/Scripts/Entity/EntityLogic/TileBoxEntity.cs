using UnityEngine;

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
        }
    }
}