using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class LegendItemPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private ILegend ilegend_0 = null;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private string string_0 = "项";

        public event OnValueChangeEventHandler OnValueChange;

        public LegendItemPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.ilegend_0.ClearItems();
                for (int i = 0; i < this.listLegendLayers.ItemCount; i++)
                {
                    ILegendItem item =
                        ((this.listLegendLayers.Items[i] as LegendItemObject).LegendItem as IClone).Clone() as
                            ILegendItem;
                    this.ilegend_0.AddItem(item);
                }
                this.ilegend_0.AutoReorder = this.chkAutoReorder.Checked;
                this.ilegend_0.AutoAdd = this.chkAutoAdd.Checked;
                this.ilegend_0.AutoVisibility = this.chkAutoVisibility.Checked;
            }
        }

        private void btnLegendItemsSelector_Click(object sender, EventArgs e)
        {
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.method_2();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.method_2();
        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            this.method_2();
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            this.method_2();
        }

        public void Cancel()
        {
        }

        private void chkAutoAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_2();
            }
        }

        private void chkAutoAdd_Click(object sender, EventArgs e)
        {
        }

        private void chkAutoReorder_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_2();
            }
        }

        private void chkAutoReorder_Click(object sender, EventArgs e)
        {
        }

        private void chkAutoVisibility_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_2();
            }
        }

        private void chkAutoVisibility_Click(object sender, EventArgs e)
        {
        }

        private void chkNewColumn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                (this.listLegendLayers.SelectedItem as LegendItemObject).LegendItem.NewColumn =
                    this.chkNewColumn.Checked;
                this.method_2();
            }
        }

        private void chkNewColumn_Click(object sender, EventArgs e)
        {
        }

        private void LegendItemPropertyPage_Load(object sender, EventArgs e)
        {
            IMap map = this.ilegend_0.Map;
            for (int i = 0; i < map.LayerCount; i++)
            {
                ILayer layer = map.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_1(layer as ICompositeLayer);
                }
                else
                {
                    this.listMapLayers.Items.Add(new LayerObject(layer));
                }
            }
            this.method_0();
            this.bool_0 = true;
        }

        private void listLegendLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listLegendLayers.SelectedIndices.Count > 0)
            {
                this.btnLegendItemsSelector.Enabled = true;
                this.chkNewColumn.Enabled = true;
                this.txtColumns.Enabled = true;
                this.btnUnSelect.Enabled = true;
                if (this.listLegendLayers.SelectedIndex == 0)
                {
                    this.btnMoveUp.Enabled = false;
                }
                else
                {
                    this.btnMoveUp.Enabled = true;
                }
                if (this.listLegendLayers.SelectedIndex == (this.listLegendLayers.ItemCount - 1))
                {
                    this.btnMoveDown.Enabled = false;
                }
                else
                {
                    this.btnMoveDown.Enabled = true;
                }
                ILegendItem legendItem = (this.listLegendLayers.SelectedItem as LegendItemObject).LegendItem;
                this.bool_0 = false;
                this.txtColumns.Text = legendItem.Columns.ToString();
                this.chkNewColumn.Checked = legendItem.NewColumn;
                this.bool_0 = true;
            }
            else
            {
                this.btnLegendItemsSelector.Enabled = false;
                this.chkNewColumn.Enabled = false;
                this.txtColumns.Enabled = false;
                this.btnUnSelect.Enabled = false;
                this.btnMoveDown.Enabled = false;
                this.btnMoveUp.Enabled = false;
            }
        }

        private void listMapLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listMapLayers.SelectedIndex > 0)
            {
                this.btnSelect.Enabled = true;
            }
        }

        private void method_0()
        {
            ILegendItem item = null;
            for (int i = 0; i < this.ilegend_0.ItemCount; i++)
            {
                item = (this.ilegend_0.get_Item(i) as IClone).Clone() as ILegendItem;
                this.listLegendLayers.Items.Add(new LegendItemObject(item));
            }
            this.chkAutoReorder.Checked = this.ilegend_0.AutoReorder;
            this.chkAutoAdd.Checked = this.ilegend_0.AutoAdd;
            this.chkAutoVisibility.Checked = this.ilegend_0.AutoVisibility;
        }

        private void method_1(ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_1(layer as ICompositeLayer);
                }
                else
                {
                    this.listMapLayers.Items.Add(new ObjectWrap(layer));
                }
            }
        }

        private void method_2()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.imapSurroundFrame_0 = object_0 as IMapSurroundFrame;
            this.ilegend_0 = this.imapSurroundFrame_0.MapSurround as ILegend;
        }

        private void txtColumns_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    (this.listLegendLayers.SelectedItem as LegendItemObject).LegendItem.Columns =
                        short.Parse(this.txtColumns.Text);
                    this.method_2();
                }
                catch
                {
                }
            }
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}