using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class frmFilterLayerSelect : Form
    {

        public frmFilterLayerSelect()
        {
            this.InitializeComponent();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (this.checkedListBox1.GetItemChecked(i))
                {
                    this.checkedListBox1.SetItemChecked(i, false);
                }
            }
            this.btnOK.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.FilterLayers.Clear();
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (!this.checkedListBox1.GetItemChecked(i))
                {
                    this.FilterLayers.Add((this.checkedListBox1.Items[i] as LayerObject).Layer as IFeatureLayer);
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (!this.checkedListBox1.GetItemChecked(i))
                {
                    this.checkedListBox1.SetItemChecked(i, true);
                }
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue != CheckState.Checked)
            {
                for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                {
                    if ((i != e.Index) && this.checkedListBox1.GetItemChecked(i))
                    {
                        this.btnOK.Enabled = true;
                        return;
                    }
                }
                this.btnOK.Enabled = false;
            }
            else
            {
                this.btnOK.Enabled = true;
            }
        }

 private void frmFilterLayerSelect_Load(object sender, EventArgs e)
        {
            foreach (IFeatureLayer layer in this.Layers)
            {
                bool isChecked = this.FilterLayers.IndexOf(layer) == -1;
                this.checkedListBox1.Items.Add(new LayerObject(layer), isChecked);
            }
        }

 internal List<IFeatureLayer> FilterLayers { get; set; }

        internal List<IFeatureLayer> Layers { get; set; }
    }
}

