using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityEngine;

namespace GameMain
{
    public class PlayerSkillController : MonoBehaviour
    {
        [Header("Config")]
        public Vector3 meleeOffset = new Vector3(0.8f, 0.5f, 0f);
        public Vector3 shootOffset = new Vector3(0.8f, 0.5f, 0f);
        
        public float Face => transform.localScale.x;
        public Vector3 FaceDirection => Vector3.right * Face;

        /// <summary>
        /// 射击
        /// </summary>
        public void Shoot(int attack)
        {
            IDataTable<DRBullet> dtBullet = GameEntry.DataTable.GetDataTable<DRBullet>();
            DRBullet drBullet = dtBullet.GetDataRow(4001);
            GameEntry.Entity.ShowBulletEntity(new BulletEntityData(GameEntry.Entity.GenerateSerialId(),
                4001,
                GetWorldPositionByOffset(shootOffset),
                FaceDirection,
                drBullet.Speed,
                attack));
        }

        /// <summary>
        /// 近战
        /// </summary>
        public void Melee()
        {
            // TODO Generate Melee Attack
        }
        
        /// <summary>
        /// 获取偏移对应的世界位置
        /// </summary>
        public Vector3 GetWorldPositionByOffset(Vector3 offset)
        {
            offset.x *= Face;
            return offset + transform.position;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + shootOffset, 0.1f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + meleeOffset, 0.1f);
        }
    }
}
