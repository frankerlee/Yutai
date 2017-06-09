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
    public class SnappingPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private CheckedListBox checkedListBox1;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private Label lblUnit;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IMap m_pMap = null;
        private IRealTimeFeedSnap m_pRealTimeFeedSnap = null;
        private string m_Title = "捕捉";
        private TextBox txtSnapDistance;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.checkedListBox1 = new CheckedListBox();
            this.label2 = new Label();
            this.lblUnit = new Label();
            this.txtSnapDistance = new TextBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "捕捉到以下图层的要素上";
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new Point(15, 0x23);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(0xee, 0x84);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(20, 0xb9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "容差";
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new Point(0xd4, 0xb9);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new Size(0, 12);
            this.lblUnit.TabIndex = 3;
            this.txtSnapDistance.Location = new Point(0x4e, 0xb5);
            this.txtSnapDistance.Name = "txtSnapDistance";
            this.txtSnapDistance.Size = new Size(0x6f, 0x15);
            this.txtSnapDistance.TabIndex = 4;
            this.txtSnapDistance.TextChanged += new EventHandler(this.txtSnapDistance_TextChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.txtSnapDistance);
            base.Controls.Add(this.lblUnit);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.checkedListBox1);
            base.Controls.Add(this.label1);
            base.Name = "SnappingPage";
            base.Size = new Size(0x112, 0xf1);
            base.Load += new EventHandler(this.SnappingPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
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

