using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ItemCheckEventHandler = DevExpress.XtraEditors.Controls.ItemCheckEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class TopologyErrorSelectionCtrl
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.label1 = new Label();
            this.chkSelectErrors = new CheckEdit();
            this.chkSelectExceptions = new CheckEdit();
            this.label2 = new Label();
            this.checkedListBoxControl1 = new CheckedListBoxControl();
            this.btnSelectAll = new SimpleButton();
            this.btnClearAll = new SimpleButton();
            this.chkSelectErrors.Properties.BeginInit();
            this.chkSelectExceptions.Properties.BeginInit();
            ((ISupportInitialize) this.checkedListBoxControl1).BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(215, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "设置拓扑错误选择工具可以选择的类型";
            this.chkSelectErrors.Location = new Point(32, 48);
            this.chkSelectErrors.Name = "chkSelectErrors";
            this.chkSelectErrors.Properties.Caption = "选择错误";
            this.chkSelectErrors.Size = new Size(112, 19);
            this.chkSelectErrors.TabIndex = 1;
            this.chkSelectErrors.CheckedChanged += new EventHandler(this.chkSelectErrors_CheckedChanged);
            this.chkSelectExceptions.Location = new Point(32, 80);
            this.chkSelectExceptions.Name = "chkSelectExceptions";
            this.chkSelectExceptions.Properties.Caption = "选择例外";
            this.chkSelectExceptions.Size = new Size(112, 19);
            this.chkSelectExceptions.TabIndex = 2;
            this.chkSelectExceptions.CheckedChanged += new EventHandler(this.chkSelectExceptions_CheckedChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 112);
            this.label2.Name = "label2";
            this.label2.Size = new Size(153, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "从下列规则选择错误和例外";
            this.checkedListBoxControl1.ItemHeight = 17;
            this.checkedListBoxControl1.Location = new Point(16, 136);
            this.checkedListBoxControl1.Name = "checkedListBoxControl1";
            this.checkedListBoxControl1.Size = new Size(248, 104);
            this.checkedListBoxControl1.TabIndex = 4;
            this.checkedListBoxControl1.ItemCheck += new ItemCheckEventHandler(this.checkedListBoxControl1_ItemCheck);
            this.btnSelectAll.Location = new Point(272, 136);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(64, 24);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.btnClearAll.Location = new Point(272, 176);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new Size(64, 24);
            this.btnClearAll.TabIndex = 6;
            this.btnClearAll.Text = "全部清除";
            this.btnClearAll.Click += new EventHandler(this.btnClearAll_Click);
            base.Controls.Add(this.btnClearAll);
            base.Controls.Add(this.btnSelectAll);
            base.Controls.Add(this.checkedListBoxControl1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.chkSelectExceptions);
            base.Controls.Add(this.chkSelectErrors);
            base.Controls.Add(this.label1);
            base.Name = "TopologyErrorSelectionCtrl";
            base.Size = new Size(352, 256);
            base.Load += new EventHandler(this.TopologyErrorSelectionCtrl_Load);
            this.chkSelectErrors.Properties.EndInit();
            this.chkSelectExceptions.Properties.EndInit();
            ((ISupportInitialize) this.checkedListBoxControl1).EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnClearAll;
        private SimpleButton btnSelectAll;
        private CheckedListBoxControl checkedListBoxControl1;
        private CheckEdit chkSelectErrors;
        private CheckEdit chkSelectExceptions;
        private Label label1;
        private Label label2;
    }
}