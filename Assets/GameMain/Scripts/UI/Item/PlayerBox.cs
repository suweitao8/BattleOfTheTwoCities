using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain
{
    public class PlayerBox : MonoBehaviour
    {
        [Header("Config")] public Image leftInventoryImage;
        public TextMeshProUGUI leftInventoryText;
        public Image rightInventoryImage;
        public TextMeshProUGUI rightInventoryText;
        public Healthbar healthbar;
        public Image headIcon;

        public void Init()
        {
            SetLeftInventory(null);
            SetRightInventory(null);
            SetHeadIcon(null);
            SetHeathbar(1f);
        }

        /// <summary>
        /// 设置左仓库的图片
        /// </summary>
        public void SetLeftInventory(Sprite sprite, int count = 0)
        {
            leftInventoryImage.sprite = sprite;
            if (sprite == null || count == 0)
            {
                leftInventoryImage.enabled = false;
                leftInventoryText.enabled = false;
                leftInventoryText.text = "";
            }
            else
            {
                leftInventoryImage.enabled = true;
                leftInventoryText.enabled = true;
                leftInventoryText.text = count.ToString();
            }
        }

        /// <summary>
        /// 设置右仓库的图片
        /// </summary>
        public void SetRightInventory(Sprite sprite, int count = 0)
        {
            rightInventoryImage.sprite = sprite;
            if (sprite == null || count == 0)
            {
                rightInventoryImage.enabled = false;
                rightInventoryText.enabled = false;
                rightInventoryText.text = "";
            }
            else
            {
                rightInventoryImage.enabled = true;
                rightInventoryText.enabled = true;
                rightInventoryText.text = count.ToString();
            }
        }

        /// <summary>
        /// 设置血条
        /// </summary>
        public void SetHeathbar(float health)
        {
            healthbar.health = health;
        }

        /// <summary>
        /// 设置头像
        /// </summary>
        public void SetHeadIcon(Sprite sprite)
        {
            headIcon.sprite = sprite;
            if (sprite == null)
            {
                headIcon.enabled = false;
            }
            else
            {
                headIcon.enabled = true;
            }
        }
    }
}