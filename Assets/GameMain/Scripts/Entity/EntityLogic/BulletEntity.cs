//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    /// <summary>
    /// 子弹类。
    /// </summary>
    public class BulletEntity : Entity
    {
        public BulletEntityData data = null;

        private float m_DestroyTime; 
        
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            data = userData as BulletEntityData;
            transform.position = data.Position;
            transform.right = data.direction;
            
            // TODO 在子弹一开始生成的时候就判断是否触发

            StartCoroutine(AutoDestroy());
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            
            StopAllCoroutines();
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            
            transform.Translate(transform.right * Time.deltaTime * data.speed, Space.World);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constant.Tag.Tilemap))
            {
                GameEntry.Tilemap.AttackTile(other.gameObject, 
                    transform.position, 
                    transform.position + transform.right * 0.5f,
                    data.attack);
                GameEntry.Entity.HideEntity(this);
            }
        }

        /// <summary>
        /// 自动销毁
        /// </summary>
        private IEnumerator AutoDestroy(float delay = 3f)
        {
            yield return new WaitForSeconds(delay);
            GameEntry.Entity.HideEntity(this);
        }
    }
}
