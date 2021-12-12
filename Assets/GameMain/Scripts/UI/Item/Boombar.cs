using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMain
{
    [ExecuteInEditMode]
    public class Boombar : MonoBehaviour
    {
        [Header("Config")] public HorizontalGridLayout boomLayout;
        public float boomLayoutOffset = 0f;
        
        private void Update()
        {
            if (boomLayout != null)
            {
                RectTransform rect = transform as RectTransform;
                rect.sizeDelta = new Vector2(boomLayout.transform.ToRect().sizeDelta.x + boomLayoutOffset,
                    rect.sizeDelta.y);
            }
        }
    }
}