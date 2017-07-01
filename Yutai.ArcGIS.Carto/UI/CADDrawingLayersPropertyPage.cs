using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ItemCheckEventHandler = DevExpress.XtraEditors.Controls.ItemCheckEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class CADDrawingLayersPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private ICadDrawingLayers icadDrawingLayers_0 = null;

        public CADDrawingLayersPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                for (int i = 0; i < this.checkedListBoxControl1.Items.Count; i++)
                {
                    CheckedListBoxItem item = this.checkedListBoxControl1.Items[i];
                    this.icadDrawingLayers_0.set_DrawingLayerVisible(i, item.CheckState == CheckState.Checked);
                }
            }
            return true;
        }

        private void btnAllUnVisible_Click(object sender, EventArgs e)
        {
            this.bool_0 = true;
            for (int i = 0; i < this.checkedListBoxControl1.Items.Count; i++)
            {
                CheckedListBoxItem item = this.checkedListBoxControl1.Items[i];
                item.CheckState = CheckState.Unchecked;
            }
        }

        private void btnAllVisible_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBoxControl1.Items.Count; i++)
            {
                CheckedListBoxItem item = this.checkedListBoxControl1.Items[i];
                item.CheckState = CheckState.Checked;
            }
            this.bool_0 = true;
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            this.bool_0 = true;
            for (int i = 0; i < this.checkedListBoxControl1.Items.Count; i++)
            {
                if (this.icadDrawingLayers_0.get_OriginalDrawingLayerVisible(i))
                {
                    CheckedListBoxItem item = this.checkedListBoxControl1.Items[i];
                    item.CheckState = CheckState.Checked;
                }
            }
        }

        private void CADDrawingLayersPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void checkedListBoxControl1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.bool_0 = true;
        }

        private void method_0()
        {
            this.checkedListBoxControl1.Items.Clear();
            for (int i = 0; i < this.icadDrawingLayers_0.DrawingLayerCount; i++)
            {
                this.checkedListBoxControl1.Items.Add(this.icadDrawingLayers_0.get_DrawingLayerName(i),
                    this.icadDrawingLayers_0.get_DrawingLayerVisible(i));
            }
        }

        public IBasicMap FocusMap
        {
            set { }
        }

        public bool IsPageDirty
        {
            get { return this.bool_0; }
        }

        public object SelectItem
        {
            set { this.icadDrawingLayers_0 = value as ICadDrawingLayers; }
        }
    }
}