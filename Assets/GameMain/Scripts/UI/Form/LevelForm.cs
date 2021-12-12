using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain {
    public class LevelForm : UGuiForm
    {
        [Header("Config")]
        public GameObject playerBoxContent;

        private PlayerBox[] m_PlayerBoxs;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            // 初始化 PlayerBox
            m_PlayerBoxs = playerBoxContent.GetComponentsInChildren<PlayerBox>(true);
            for (int i = 0; i < m_PlayerBoxs.Length; i++)
            {
                m_PlayerBoxs[i].Init();
            }
        }

        /// <summary>
        /// 设置玩家数量
        /// </summary>
        public void SetPlayerCount(int count)
        {
            for (int i = 0; i < m_PlayerBoxs.Length; i++)
            {
                if (i < count)
                {
                    m_PlayerBoxs[i].gameObject.SetActive(true);
                }
                else
                {
                    m_PlayerBoxs[i].gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// 设置左仓库的图片
        /// </summary>
        public void SetLeftInventory(int playerIndex, Sprite sprite, int count = 0)
        {
            if (playerIndex < 0 || playerIndex > m_PlayerBoxs.Length - 1)
            {
                return;
            }
            
            m_PlayerBoxs[playerIndex].SetLeftInventory(sprite, count);
        }

        /// <summary>
        /// 设置右仓库的图片
        /// </summary>
        public void SetRightInventory(int playerIndex, Sprite sprite, int count = 0)
        {
            if (playerIndex < 0 || playerIndex > m_PlayerBoxs.Length - 1)
            {
                return;
            }
            
            m_PlayerBoxs[playerIndex].SetRightInventory(sprite, count);
        }

        /// <summary>
        /// 设置血条
        /// </summary>
        public void SetHeathbar(int playerIndex, float health)
        {
            if (playerIndex < 0 || playerIndex > m_PlayerBoxs.Length - 1)
            {
                return;
            }
            
            m_PlayerBoxs[playerIndex].SetHeathbar(health);
        }

        /// <summary>
        /// 设置头像
        /// </summary>
        public void SetHeadIcon(int playerIndex, Sprite sprite)
        {
            if (playerIndex < 0 || playerIndex > m_PlayerBoxs.Length - 1)
            {
                return;
            }
            
            m_PlayerBoxs[playerIndex].SetHeadIcon(sprite);
        }
    }
}