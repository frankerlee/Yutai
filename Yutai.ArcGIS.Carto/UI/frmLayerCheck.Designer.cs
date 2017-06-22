using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmLayerCheck
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.checkedListBox1 = new CheckedListBox();
            this.label1 = new Label();
            this.btnSwitchSelect = new Button();
            this.btnClose = new Button();
            this.btnClear = new Button();
            this.btnSelectAll = new Button();
            base.SuspendLayout();
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new Point(6, 45);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(192, 180);
            this.checkedListBox1.TabIndex = 0;
            this.label1.Location = new Point(4, 3);
            this.label1.Name = "label1";
            this.label1.Size = new Size(268, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择从裁剪中排除的图层。这些图层在裁剪区外的要素也将显示。";
            this.btnSwitchSelect.Location = new Point(224, 75);
            this.btnSwitchSelect.Name = "btnSwitchSelect";
            this.btnSwitchSelect.Size = new Size(48, 24);
            this.btnSwitchSelect.TabIndex = 8;
            this.btnSwitchSelect.Text = "反选";
            this.btnSwitchSelect.Click += new EventHandler(this.btnSwitchSelect_Click);
            this.btnClose.Location = new Point(224, 198);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(48, 24);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.btnClear.Location = new Point(224, 105);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(48, 24);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.btnSelectAll.Location = new Point(224, 45);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(48, 24);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(284, 262);
            base.Controls.Add(this.btnSwitchSelect);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.btnSelectAll);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.checkedListBox1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmLayerCheck";
            this.Text = "图层";
            base.Load += new EventHandler(this.frmLayerCheck_Load);
            base.ResumeLayout(false);
        }

       
        private Button btnClear;
        private Button btnClose;
        private Button btnSelectAll;
        private Button btnSwitchSelect;
        private CheckedListBox checkedListBox1;
        private IMap imap_0;
        private Label label1;
    }
}