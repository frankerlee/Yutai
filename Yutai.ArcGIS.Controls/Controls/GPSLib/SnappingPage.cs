using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    public partial class SnappingPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IMap m_pMap = null;
        private IRealTimeFeedSnap m_pRealTimeFeedSnap = null;
        private string m_Title = "捕捉";

        public event OnValueChangeEventHandler OnValueChange;

        public SnappingPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                try
                {
                    this.m_pRealTimeFeedSnap.SnapDistance = double.Parse(this.txtSnapDistance.Text);
                }
                catch
                {
                }
                IArray snapLayers = this.m_pRealTimeFeedSnap.SnapLayers;
                if (snapLayers == null)
                {
                    snapLayers = new ArrayClass();
                }
                snapLayers.RemoveAll();
                for (int i = 0; i < this.checkedListBox1.CheckedItems.Count; i++)
                {
                    snapLayers.Add((this.checkedListBox1.CheckedItems[i] as LayerObject).Layer);
                }
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

 private bool FindLayerIsSnap(ILayer pLayer)
        {
            IArray snapLayers = this.m_pRealTimeFeedSnap.SnapLayers;
            if (snapLayers != null)
            {
                for (int i = 0; i < snapLayers.Count; i++)
                {
                    if (snapLayers.get_Element(i) == pLayer)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

 public void ResetControl()
        {
        }

        public void SetObjects(object @object)
        {
            this.m_pRealTimeFeedSnap = @object as IRealTimeFeedSnap;
        }

        private void SnappingPage_Load(object sender, EventArgs e)
        {
            if (this.m_pRealTimeFeedSnap != null)
            {
                for (int i = 0; i < this.m_pMap.LayerCount; i++)
                {
                    ILayer layer = this.m_pMap.get_Layer(i);
                    if (!(layer is IGroupLayer) && (layer is IFeatureLayer))
                    {
                        this.checkedListBox1.Items.Add(new LayerObject(layer), this.FindLayerIsSnap(layer));
                    }
                }
                this.txtSnapDistance.Text = this.m_pRealTimeFeedSnap.SnapDistance.ToString();
                this.m_CanDo = true;
            }
        }

        private void txtSnapDistance_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void ValueChange()
        {
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public IMap Map
        {
            set
            {
                this.m_pMap = value;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

