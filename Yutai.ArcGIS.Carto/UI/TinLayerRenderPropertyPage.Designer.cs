using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class TinLayerRenderPropertyPage
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.checkedListBox1 = new CheckedListBox();
            this.label1 = new Label();
            this.btnAdd = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.panel = new Panel();
            base.SuspendLayout();
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new Point(14, 24);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(107, 132);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.SelectedIndexChanged += new EventHandler(this.checkedListBox1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(14, 6);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "显示";
            this.btnAdd.Location = new Point(27, 172);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Location = new Point(27, 201);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.panel.Dock = DockStyle.Right;
            this.panel.Location = new Point(127, 0);
            this.panel.Name = "panel";
            this.panel.Size = new Size(401, 268);
            this.panel.TabIndex = 4;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.panel);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.checkedListBox1);
            base.Name = "TinLayerRenderPropertyPage";
            base.Size = new Size(528, 268);
            base.Load += new EventHandler(this.TinLayerRenderPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private CheckedListBox checkedListBox1;
        private Label label1;
        private Panel panel;
        private TinColorRampRenderPropertyPage tinColorRampRenderPropertyPage_0;
        private TinSimpleRenderCtrl tinSimpleRenderCtrl_0;
        private TinUniqueRenderPropertyPage tinUniqueRenderPropertyPage_0;
    }
}