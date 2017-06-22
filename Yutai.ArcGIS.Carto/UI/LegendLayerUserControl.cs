using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class LegendLayerUserControl : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private ILegend ilegend_0 = null;
        private IMap imap_0 = null;

        public LegendLayerUserControl()
        {
            this.InitializeComponent();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.listLegendLayers.SelectedIndex;
            object selectedItem = this.listLegendLayers.SelectedItem;
            this.listLegendLayers.Items.RemoveAt(selectedIndex);
            this.ilegend_0.RemoveItem(selectedIndex);
            selectedIndex++;
            if (selectedIndex == this.ilegend_0.ItemCount)
            {
                this.ilegend_0.AddItem((selectedItem as LegendItemWrap).LegendItem);
                this.listLegendLayers.Items.Add(selectedItem);
            }
            else
            {
                this.ilegend_0.InsertItem(selectedIndex, (selectedItem as LegendItemWrap).LegendItem);
                this.listLegendLayers.Items.Insert(selectedIndex, selectedItem);
            }
            this.listLegendLayers.SelectedItem = selectedItem;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.listLegendLayers.SelectedIndex;
            object selectedItem = this.listLegendLayers.SelectedItem;
            this.listMapLayers.Items.RemoveAt(selectedIndex);
            this.ilegend_0.RemoveItem(selectedIndex);
            selectedIndex--;
            this.ilegend_0.InsertItem(selectedIndex, (selectedItem as LegendItemWrap).LegendItem);
            this.listLegendLayers.Items.Insert(selectedIndex, selectedItem);
            this.listLegendLayers.SelectedItem = selectedItem;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listMapLayers.SelectedIndices.Count; i++)
            {
                ILayer layer = (this.listMapLayers.Items[this.listMapLayers.SelectedIndices[i]] as ObjectWrap).Object as ILayer;
                ILegendItem item = this.method_2(layer);
                this.listLegendLayers.Items.Add(new LegendItemWrap(item));
                this.ilegend_0.AddItem(item);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listMapLayers.Items.Count; i++)
            {
                ILayer layer = (this.listMapLayers.Items[i] as ObjectWrap).Object as ILayer;
                ILegendItem item = this.method_2(layer);
                this.listLegendLayers.Items.Add(new LegendItemWrap(item));
                this.ilegend_0.AddItem(item);
            }
        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            for (int i = this.listLegendLayers.SelectedIndices.Count - 1; i >= 0; i--)
            {
                this.ilegend_0.RemoveItem(this.listLegendLayers.SelectedIndices[i]);
                this.listLegendLayers.Items.RemoveAt(this.listLegendLayers.SelectedIndices[i]);
            }
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            this.ilegend_0.ClearItems();
            this.listLegendLayers.Items.Clear();
        }

 public IArray GetSelectLegendItem()
        {
            IArray array = new ArrayClass();
            for (int i = 0; i < this.listLegendLayers.Items.Count; i++)
            {
                array.Add(this.listLegendLayers.Items[i]);
            }
            return array;
        }

 private void LegendLayerUserControl_Load(object sender, EventArgs e)
        {
            this.method_1();
            this.bool_0 = true;
        }

        private void listLegendLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listMapLayers.SelectedIndex >= 0)
            {
                this.btnUnSelect.Enabled = true;
            }
            else
            {
                this.btnUnSelect.Enabled = false;
            }
            if (this.listLegendLayers.Items.Count <= 1)
            {
                this.btnMoveDown.Enabled = false;
                this.btnMoveUp.Enabled = false;
            }
            else if (this.listLegendLayers.SelectedIndices.Count == 1)
            {
                if (this.listLegendLayers.SelectedIndex > 0)
                {
                    this.btnMoveDown.Enabled = true;
                }
                else if (this.listLegendLayers.SelectedIndex < (this.listLegendLayers.Items.Count - 1))
                {
                    this.btnMoveUp.Enabled = true;
                }
            }
            else
            {
                this.btnMoveDown.Enabled = false;
                this.btnMoveUp.Enabled = false;
            }
        }

        private void listMapLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listMapLayers.SelectedIndex >= 0)
            {
                this.btnSelect.Enabled = true;
            }
            else
            {
                this.btnSelect.Enabled = false;
            }
        }

        private void method_0(ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_0(layer as ICompositeLayer);
                }
                else
                {
                    this.listMapLayers.Items.Add(new ObjectWrap(layer));
                }
            }
        }

        private void method_1()
        {
            int num;
            for (num = 0; num < this.imap_0.LayerCount; num++)
            {
                ILayer layer = this.imap_0.get_Layer(num);
                if (layer is IGroupLayer)
                {
                    this.method_0(layer as ICompositeLayer);
                }
                else
                {
                    this.listMapLayers.Items.Add(new ObjectWrap(layer));
                }
            }
            for (num = 0; num < this.ilegend_0.ItemCount; num++)
            {
                this.listLegendLayers.Items.Add(new LegendItemWrap(this.ilegend_0.get_Item(num)));
            }
        }

        private ILegendItem method_2(ILayer ilayer_0)
        {
            return new HorizontalLegendItemClass { Layer = ilayer_0 };
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
        }

        public IMap FocusMap
        {
            set
            {
                this.imap_0 = value;
            }
        }

        public ILegend Legend
        {
            set
            {
                this.ilegend_0 = value;
            }
        }
    }
}

