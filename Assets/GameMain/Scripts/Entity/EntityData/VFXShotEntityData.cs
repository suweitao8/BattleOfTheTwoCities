using UnityEngine;

namespace GameMain
{
    public class VFXShotEntityData : EntityData
    {
        public VFXShotEntityData(int entityId, int typeId, Vector3 position) : base(entityId, typeId)
        {
            this.Position = position;
        }
    }
}