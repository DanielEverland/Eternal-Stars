using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    [AddComponentMenu("Layout/Restricted Content Size Fitter", 142), ExecuteInEditMode, RequireComponent(typeof(RectTransform))]
    public class ConstrainedContentSizeFitter : UIBehaviour, ILayoutSelfController, ILayoutController
    {
        public enum FitMode
        {
            Unconstrained,
            MinSize,
            PreferredSize
        }

        [SerializeField]
        protected FitMode m_HorizontalFit = FitMode.Unconstrained;
        [SerializeField]
        protected bool m_HorizontalUseMinSize = false;
        [SerializeField]
        protected float m_HorizontalMinSize = 0;
        [SerializeField]
        protected bool m_HorizontalUseMaxSize = false;
        [SerializeField]
        protected float m_HorizontalMaxSize = 0;

        [SerializeField]
        protected FitMode m_VerticalFit = FitMode.Unconstrained;
        [SerializeField]
        protected bool m_VerticalUseMinSize = false;
        [SerializeField]
        protected float m_VerticalMinSize = 0;
        [SerializeField]
        protected bool m_VerticalUseMaxSize = false;
        [SerializeField]
        protected float m_VerticalMaxSize = 0;

        [NonSerialized]
        private RectTransform m_Rect;

        private DrivenRectTransformTracker m_Tracker;

        public float horizontalMinSize
        {
            get
            {
                return m_HorizontalMinSize;
            }
            set
            {
                if(m_HorizontalMinSize != value)
                {
                    m_HorizontalMinSize = value;

                    if (m_HorizontalMaxSize < m_HorizontalMinSize)
                        m_HorizontalMaxSize = m_HorizontalMinSize;

                    this.SetDirty();
                }
            }
        }
        public float horizontalMaxSize
        {
            get
            {
                return m_HorizontalMaxSize;
            }
            set
            {
                if (m_HorizontalMaxSize != value)
                {
                    if (m_HorizontalMinSize > m_HorizontalMaxSize)
                        m_HorizontalMinSize = m_HorizontalMaxSize;

                    m_HorizontalMaxSize = value;
                    this.SetDirty();
                }
            }
        }
        public bool horizontalUseMinSize
        {
            get
            {
                return m_HorizontalUseMinSize;
            }
            set
            {
                if(m_HorizontalUseMinSize != value)
                {
                    m_HorizontalUseMinSize = value;
                    this.SetDirty();
                }
            }
        }
        public bool horizontalUseMaxSize
        {
            get
            {
                return m_HorizontalUseMaxSize;
            }
            set
            {
                if (m_HorizontalUseMaxSize != value)
                {
                    m_HorizontalUseMaxSize = value;
                    this.SetDirty();
                }
            }
        }

        public float verticalMinSize
        {
            get
            {
                return m_VerticalMinSize;
            }
            set
            {
                if (m_VerticalMinSize != value)
                {
                    if (m_VerticalMaxSize < m_VerticalMinSize)
                        m_VerticalMaxSize = m_VerticalMinSize;

                    m_VerticalMinSize = value;
                    this.SetDirty();
                }
            }
        }
        public float verticalMaxSize
        {
            get
            {
                return m_VerticalMaxSize;
            }
            set
            {
                if (m_VerticalMaxSize != value)
                {
                    if (m_VerticalMinSize > m_VerticalMaxSize)
                        m_VerticalMinSize = m_VerticalMaxSize;

                    m_VerticalMaxSize = value;
                    this.SetDirty();
                }
            }
        }
        public bool verticalUseMinSize
        {
            get
            {
                return m_VerticalUseMinSize;
            }
            set
            {
                if (m_VerticalUseMinSize != value)
                {
                    m_VerticalUseMinSize = value;
                    this.SetDirty();
                }
            }
        }
        public bool verticalUseMaxSize
        {
            get
            {
                return m_VerticalUseMaxSize;
            }
            set
            {
                if (m_VerticalUseMaxSize != value)
                {
                    m_VerticalUseMaxSize = value;
                    this.SetDirty();
                }
            }
        }

        public FitMode horizontalFit
        {
            get
            {
                return this.m_HorizontalFit;
            }
            set
            {
                if (m_HorizontalFit != value)
                {
                    m_HorizontalFit = value;
                    this.SetDirty();
                }
            }
        }

        public FitMode verticalFit
        {
            get
            {
                return this.m_VerticalFit;
            }
            set
            {
                if (m_VerticalFit != value)
                {
                    m_VerticalFit = value;
                    this.SetDirty();
                }
            }
        }

        private RectTransform rectTransform
        {
            get
            {
                if (this.m_Rect == null)
                {
                    this.m_Rect = base.GetComponent<RectTransform>();
                }
                return this.m_Rect;
            }
        }

        protected virtual float horizontalMinValue
        {
            get
            {
                return (m_HorizontalUseMinSize) ? m_HorizontalMinSize : float.MinValue;
            }
        }
        protected virtual float horizontalMaxValue
        {
            get
            {
                return (m_HorizontalUseMaxSize) ? m_HorizontalMaxSize : float.MaxValue;
            }
        }
        protected virtual float verticalMinValue
        {
            get
            {
                return (m_VerticalUseMinSize) ? m_VerticalMinSize : float.MinValue;
            }
        }
        protected virtual float verticalMaxValue
        {
            get
            {
                return (m_VerticalUseMaxSize) ? m_VerticalMaxSize : float.MaxValue;
            }
        }

        protected ConstrainedContentSizeFitter()
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            this.SetDirty();
        }

        protected override void OnDisable()
        {
            this.m_Tracker.Clear();
            LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
            base.OnDisable();
        }

        protected override void OnRectTransformDimensionsChange()
        {
            this.SetDirty();
        }

        private void HandleSelfFittingAlongAxis(int axis)
        {
            FitMode fitMode = (axis != 0) ? this.verticalFit : this.horizontalFit;
            if (fitMode == FitMode.Unconstrained)
            {
                this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.None);
            }
            else
            {
                this.m_Tracker.Add(this, this.rectTransform, (axis != 0) ? DrivenTransformProperties.SizeDeltaY : DrivenTransformProperties.SizeDeltaX);
                if (fitMode == FitMode.MinSize)
                {
                    this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis,
                        Mathf.Clamp(LayoutUtility.GetMinSize(this.m_Rect, axis), GetMinValue(axis), GetMaxValue(axis)));
                }
                else
                {
                    this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis,
                        Mathf.Clamp(LayoutUtility.GetPreferredSize(this.m_Rect, axis), GetMinValue(axis), GetMaxValue(axis)));
                }
            }
        }

        protected virtual float GetMinValue(int axis)
        {
            if (axis == 0)
                return horizontalMinValue;
            else
                return verticalMinValue;
        }
        protected virtual float GetMaxValue(int axis)
        {
            if (axis == 0)
                return horizontalMaxValue;
            else
                return verticalMaxValue;
        }
        public virtual void SetLayoutHorizontal()
        {
            this.m_Tracker.Clear();
            this.HandleSelfFittingAlongAxis(0);
        }

        public virtual void SetLayoutVertical()
        {
            this.HandleSelfFittingAlongAxis(1);
        }

        protected void SetDirty()
        {
            if (this.IsActive())
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);                
            }
        }

        protected override void OnValidate()
        {
            this.SetDirty();
        }
    }
}