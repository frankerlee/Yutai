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
    partial class CADDrawingLayersPropertyPage
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.label1 = new Label();
            this.checkedListBoxControl1 = new CheckedListBoxControl();
            this.btnAllVisible = new SimpleButton();
            this.btnAllUnVisible = new SimpleButton();
            this.btnRestore = new SimpleButton();
            ((ISupportInitialize) this.checkedListBoxControl1).BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(140, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "设置图层的可见或不可见";
            this.checkedListBoxControl1.ItemHeight = 17;
            this.checkedListBoxControl1.Location = new Point(8, 48);
            this.checkedListBoxControl1.Name = "checkedListBoxControl1";
            this.checkedListBoxControl1.Size = new Size(248, 136);
            this.checkedListBoxControl1.TabIndex = 1;
            this.checkedListBoxControl1.ItemCheck += new ItemCheckEventHandler(this.checkedListBoxControl1_ItemCheck);
            this.btnAllVisible.Location = new Point(16, 200);
            this.btnAllVisible.Name = "btnAllVisible";
            this.btnAllVisible.Size = new Size(64, 24);
            this.btnAllVisible.TabIndex = 2;
            this.btnAllVisible.Text = "全部可见";
            this.btnAllVisible.Click += new EventHandler(this.btnAllVisible_Click);
            this.btnAllUnVisible.Location = new Point(88, 200);
            this.btnAllUnVisible.Name = "btnAllUnVisible";
            this.btnAllUnVisible.Size = new Size(72, 24);
            this.btnAllUnVisible.TabIndex = 3;
            this.btnAllUnVisible.Text = "全部不可见";
            this.btnAllUnVisible.Click += new EventHandler(this.btnAllUnVisible_Click);
            this.btnRestore.Location = new Point(168, 200);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new Size(88, 24);
            this.btnRestore.TabIndex = 4;
            this.btnRestore.Text = "恢复初始设置";
            this.btnRestore.Click += new EventHandler(this.btnRestore_Click);
            base.Controls.Add(this.btnRestore);
            base.Controls.Add(this.btnAllUnVisible);
            base.Controls.Add(this.btnAllVisible);
            base.Controls.Add(this.checkedListBoxControl1);
            base.Controls.Add(this.label1);
            base.Name = "CADDrawingLayersPropertyPage";
            base.Size = new Size(320, 296);
            base.Load += new EventHandler(this.CADDrawingLayersPropertyPage_Load);
            ((ISupportInitialize) this.checkedListBoxControl1).EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAllUnVisible;
        private SimpleButton btnAllVisible;
        private SimpleButton btnRestore;
        private CheckedListBoxControl checkedListBoxControl1;
        private Label label1;
    }
}