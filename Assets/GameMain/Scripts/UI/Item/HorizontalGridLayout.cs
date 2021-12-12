using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMain
{
    [ExecuteInEditMode]
    public class HorizontalGridLayout : MonoBehaviour
    {
        public Vector2 size = new Vector2(50f, 50f);
        public float space = 5f;
        
        private void Update()
        {
            RectTransform rect = transform as RectTransform;
            int childCount = transform.childCount;
            rect.sizeDelta = new Vector2(size.x * childCount + space * (childCount + 1), rect.sizeDelta.y);
            for (int i = 0; i < childCount; i++)
            {
                RectTransform child = transform.GetChild(i) as RectTransform;
                child.anchorMin = child.anchorMax = new Vector2(0f, 0.5f);
                child.sizeDelta = size;
                child.anchoredPosition = new Vector2(size.x * (i + 0.5f) + space * (i + 1f), 0f);
            }
        }
    }
}