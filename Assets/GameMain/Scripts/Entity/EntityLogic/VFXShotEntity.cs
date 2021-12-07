using UnityEngine;

namespace GameMain
{
    public class VFXShotEntity : Entity
    {
        private ParticleSystem m_Particle;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Particle = GetComponentInChildren<ParticleSystem>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            VFXShotEntityData data = userData as VFXShotEntityData;
            transform.position = data.Position;
            
            m_Particle.Play();
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            
            if (m_Particle.isPlaying == false)
            {
                GameEntry.Entity.HideEntity(this);                
            }
        }
    }
}