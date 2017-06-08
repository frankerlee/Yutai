using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class frmFilterLayerSelect : Form
    {
        private Button btnCancel;
        private Button btnClearAll;
        private Button btnOK;
        private Button btnSelectAll;
        private CheckedListBox checkedListBox1;
        private IContainer components = null;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmFilterLayerSelect_Load(object sender, EventArgs e)
        {
            foreach (IFeatureLayer layer in this.Layers)
            {
                bool isChecked = this.FilterLayers.IndexOf(layer) == -1;
                this.checkedListBox1.Items.Add(new LayerObject(layer), isChecked);
            }
        }

        private void InitializeComponent()
        {
            this.btnSelectAll = new Button();
            this.btnClearAll = new Button();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.checkedListBox1 = new CheckedListBox();
            base.SuspendLayout();
            this.btnSelectAll.Location = new Point(0xd9, 13);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(0x4b, 0x17);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.btnClearAll.Location = new Point(0xd9, 0x2a);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new Size(0x4b, 0x17);
            this.btnClearAll.TabIndex = 2;
            this.btnClearAll.Text = "全部清除";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new EventHandler(this.btnClearAll_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xd9, 0x99);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0xd9, 0x7c);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new Point(2, 12);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(200, 0xa4);
            this.checkedListBox1.TabIndex = 5;
            this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x129, 0xb7);
            base.Controls.Add(this.checkedListBox1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnClearAll);
            base.Controls.Add(this.btnSelectAll);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmFilterLayerSelect";
            this.Text = "模板过滤";
            base.Load += new EventHandler(this.frmFilterLayerSelect_Load);
            base.ResumeLayout(false);
        }

        internal List<IFeatureLayer> FilterLayers { get; set; }

        internal List<IFeatureLayer> Layers { get; set; }
    }
}

