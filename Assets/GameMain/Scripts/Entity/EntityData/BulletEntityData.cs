//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using UnityEngine;

namespace GameMain
{
    [Serializable]
    public class BulletEntityData : EntityData
    {
        public float speed;
        public int attack;
        public Vector3 direction;
        
        public BulletEntityData(int entityId, int typeId, Vector3 position, Vector3 direction, float speed, int attack)
            : base(entityId, typeId)
        {
            this.Position = position;
            this.direction = direction;
            this.speed = speed;
            this.attack = attack;
        }
    }
}
