//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using System;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public static class EntityExtension
    {
        // 关于 EntityId 的约定：
        // 0 为无效
        // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
        // 负值用于本地生成的临时实体（如特效、FakeObject等）
        private static int s_SerialId = 0;

        public static Entity GetGameEntity(this EntityComponent entityComponent, int entityId)
        {
            UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
            if (entity == null)
            {
                return null;
            }

            return (Entity)entity.Logic;
        }

        public static void HideEntity(this EntityComponent entityComponent, Entity entity)
        {
            entityComponent.HideEntity(entity.Entity);
        }
        
        public static void AttachEntity(this EntityComponent entityComponent, Entity entity, int ownerId, string parentTransformPath = null, object userData = null)
        {
            entityComponent.AttachEntity(entity.Entity, ownerId, parentTransformPath, userData);
        }

        #region Show Entity

        /// <summary>
        /// 子弹
        /// </summary>
        public static void ShowBulletEntity(this EntityComponent entityComponent, BulletEntityData data)
        {
            entityComponent.ShowEntity(typeof(BulletEntity), Constant.EntityGroup.Bullet, Constant.AssetPriority.Middle, data);
        }
        
        /// <summary>
        /// 一次性特效
        /// </summary>
        public static void ShowVFXShotEntity(this EntityComponent entityComponent, VFXShotEntityData data)
        {
            entityComponent.ShowEntity(typeof(VFXShotEntity), Constant.EntityGroup.VFXShot, Constant.AssetPriority.Low, data);
        }

        /// <summary>
        /// 方块
        /// </summary>
        public static void ShowTileBoxEntity(this EntityComponent entityComponent, TileBoxEntityData data)
        {
            entityComponent.ShowEntity(typeof(TileBoxEntity), Constant.EntityGroup.TileBox, Constant.AssetPriority.Low, data);
        }

        /// <summary>
        /// 玩家
        /// </summary>
        public static void ShowPlayerEntity(this EntityComponent entityComponent, PlayerEntityData data)
        {
            entityComponent.ShowEntity(typeof(PlayerEntity), Constant.EntityGroup.Player, Constant.AssetPriority.Most, data);
        }

        // public static void ShowMyAircraft(this EntityComponent entityComponent, MyAircraftData data)
        // {
        //     entityComponent.ShowEntity(typeof(MyAircraft), "Aircraft", Constant.AssetPriority.MyAircraftAsset, data);
        // }
        //
        // public static void ShowAircraft(this EntityComponent entityComponent, AircraftData data)
        // {
        //     entityComponent.ShowEntity(typeof(Aircraft), "Aircraft", Constant.AssetPriority.AircraftAsset, data);
        // }
        //
        // public static void ShowThruster(this EntityComponent entityComponent, ThrusterData data)
        // {
        //     entityComponent.ShowEntity(typeof(Thruster), "Thruster", Constant.AssetPriority.ThrusterAsset, data);
        // }
        //
        // public static void ShowWeapon(this EntityComponent entityComponent, WeaponData data)
        // {
        //     entityComponent.ShowEntity(typeof(Weapon), "Weapon", Constant.AssetPriority.WeaponAsset, data);
        // }
        //
        // public static void ShowArmor(this EntityComponent entityComponent, ArmorData data)
        // {
        //     entityComponent.ShowEntity(typeof(Armor), "Armor", Constant.AssetPriority.ArmorAsset, data);
        // }
        //
        // public static void ShowBullet(this EntityComponent entityCompoennt, BulletEntityData entityData)
        // {
        //     entityCompoennt.ShowEntity(typeof(BulletEntity), "Bullet", Constant.AssetPriority.BulletAsset, entityData);
        // }
        //
        // public static void ShowAsteroid(this EntityComponent entityCompoennt, AsteroidData data)
        // {
        //     entityCompoennt.ShowEntity(typeof(Asteroid), "Asteroid", Constant.AssetPriority.AsteroiAsset, data);
        // }
        //
        // public static void ShowEffect(this EntityComponent entityComponent, EffectData data)
        // {
        //     entityComponent.ShowEntity(typeof(Effect), "Effect", Constant.AssetPriority.EffectAsset, data);
        // }
        
        #endregion

        private static void ShowEntity(this EntityComponent entityComponent, Type logicType, string entityGroup, int priority, EntityData data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            IDataTable<DREntity> dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity drEntity = dtEntity.GetDataRow(data.TypeId);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                return;
            }

            entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(drEntity.AssetName), entityGroup, priority, data);
        }

        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return --s_SerialId;
        }
    }
}
