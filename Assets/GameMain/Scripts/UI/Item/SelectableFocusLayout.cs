using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameMain
{
    public class FocusElementLayoutData
    {
        public int index;
        public FocusElement element;
        public RectTransform rectTransform;
        public CanvasGroup canvasGroup;
        public Tweener moveTweener;
        public Tweener scaleTweener;
        public Tweener alphaTweener;
    }
    
    public class SelectableFocusLayout : MonoBehaviour
    {
        [Header("Config")] public float powBase = 0.9f;
        public float anchorYSpace = 80f;
        public float scaleSpace = 0.2f;
        public float alphaSpace = 0.2f;
        
        private RectTransform m_RectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (m_RectTransform == null)
                {
                    m_RectTransform = transform as RectTransform;
                }

                return m_RectTransform;
            }
        }

        private FocusElementLayoutData[] m_ElementDatas;
        
        // Selectables
        private FocusElement[] m_FocusElements;

        private void Awake()
        {
            m_FocusElements = GetComponentsInChildren<FocusElement>();
            InitFocusElementData();
        }

        private void OnEnable()
        {
            if (UpdateFocusChildAndReturn() == false)
            {
                FocusByIndex(RectTransform.childCount / 2, true);
            }
        }

        /// <summary>
        /// 初始化焦点元素数据
        /// </summary>
        void InitFocusElementData()
        {
            m_ElementDatas = new FocusElementLayoutData[m_FocusElements.Length];
            for (int i = 0; i < m_ElementDatas.Length; i++)
            {
                m_ElementDatas[i] = new FocusElementLayoutData();
                m_ElementDatas[i].index = i;
                m_ElementDatas[i].element = m_FocusElements[i];
                m_ElementDatas[i].canvasGroup = m_FocusElements[i].GetComponent<CanvasGroup>();
                m_ElementDatas[i].rectTransform = m_FocusElements[i].transform as RectTransform;
                m_ElementDatas[i].element.onSelecte.AddListener(UpdateFocusChild);
            }
        }
        
        /// <summary>
        /// 更新子级的焦点
        /// </summary>
        public bool UpdateFocusChildAndReturn()
        {
            for (int i = 0; i < m_ElementDatas.Length; i++)
            {
                if (m_ElementDatas[i].element.IsSelected)
                {
                    FocusByIndex(i);
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// 更新子级的焦点
        /// </summary>
        public void UpdateFocusChild()
        {
            for (int i = 0; i < m_ElementDatas.Length; i++)
            {
                if (m_ElementDatas[i].element.IsSelected)
                {
                    FocusByIndex(i);
                }
            }
        }

        /// <summary>
        /// 聚焦在指定Sibling Index的对象上
        /// </summary>
        public void FocusByIndex(int index, bool immediate = false)
        {
            for (int i = 0; i < m_ElementDatas.Length; i++)
            {
                RectTransform rectTrans = m_ElementDatas[i].rectTransform;
                int indexDiff = i - index;
                
                // 杀掉之前的动画
                m_ElementDatas[i].moveTweener?.Kill();
                m_ElementDatas[i].scaleTweener?.Kill();
                m_ElementDatas[i].alphaTweener?.Kill();

                // Move
                Vector2 moveTarget = new Vector2(0f, -indexDiff * anchorYSpace * Mathf.Pow(powBase, Mathf.Abs(indexDiff)));
                // Scale
                Vector3 scaleTarget = Vector3.one * Mathf.Max(0f, (1f - scaleSpace) + scaleSpace * Mathf.Pow(powBase, Mathf.Abs(indexDiff) * Mathf.Abs(indexDiff)));
                // Alpha
                float alphaTarget = Mathf.Max(0f, 1f - Mathf.Abs(indexDiff) * alphaSpace);

                if (immediate)
                {
                    rectTrans.anchoredPosition = moveTarget;
                    rectTrans.localScale = scaleTarget;
                    m_ElementDatas[i].canvasGroup.alpha = alphaTarget;
                }
                else
                {
                    rectTrans.DOAnchorPos(moveTarget, 0.3f).SetEase(Ease.InOutCubic);
                    rectTrans.DOScale(scaleTarget, 0.3f).SetEase(Ease.InOutCubic);
                    m_ElementDatas[i].canvasGroup.DOFade(alphaTarget, 0.3f).SetEase(Ease.InOutCubic);
                }
            }
        }
        
        #if UNITY_EDITOR
        
        /// <summary>
        /// 聚焦在指定Sibling Index的对象上
        /// </summary>
        public void FocusByIndexOnEditor(int index)
        {
            for (int i = 0; i < RectTransform.childCount; i++)
            {
                RectTransform rectTrans = RectTransform.GetChild(i).transform as RectTransform;
                int indexDiff = i - index;

                // Move
                Vector2 moveTarget = new Vector2(0f, -indexDiff * anchorYSpace * Mathf.Pow(powBase, Mathf.Abs(indexDiff)));
                // Scale
                Vector3 scaleTarget = Vector3.one * Mathf.Max(0f, (1f - scaleSpace) + scaleSpace * Mathf.Pow(powBase, Mathf.Abs(indexDiff) * Mathf.Abs(indexDiff)));
                // Alpha
                float alphaTarget = Mathf.Max(0f, 1f - Mathf.Abs(indexDiff) * alphaSpace);

                // 应用
                rectTrans.anchoredPosition = moveTarget;
                rectTrans.localScale = scaleTarget;
                rectTrans.GetComponent<CanvasGroup>().alpha = alphaTarget;
            }
        }

        [ContextMenu("Focus To Center")]
        private void FocusByIndexOnEditorMenu()
        {
            FocusByIndexOnEditor(RectTransform.childCount / 2);
        }
        
        #endif
    }
}