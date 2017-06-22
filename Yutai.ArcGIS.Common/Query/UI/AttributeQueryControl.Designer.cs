using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Common.Query.UI
{
    partial class AttributeQueryControl
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
 private void InitializeComponent()
        {
            this.panel2 = new Panel();
            this.btnClear = new SimpleButton();
            this.panel1 = new Panel();
            this.cboSelectType = new ComboBoxEdit();
            this.chkShowSelectbaleLayer = new CheckEdit();
            this.label3 = new Label();
            this.comboBoxLayer = new ComboBoxEdit();
            this.label1 = new Label();
            this.btnClose = new SimpleButton();
            this.btnApply = new SimpleButton();
            this.panel1.SuspendLayout();
            this.cboSelectType.Properties.BeginInit();
            this.chkShowSelectbaleLayer.Properties.BeginInit();
            this.comboBoxLayer.Properties.BeginInit();
            base.SuspendLayout();
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 88);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(376, 360);
            this.panel2.TabIndex = 57;
            this.btnClear.Location = new Point(8, 456);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(56, 24);
            this.btnClear.TabIndex = 56;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.panel1.Controls.Add(this.cboSelectType);
            this.panel1.Controls.Add(this.chkShowSelectbaleLayer);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBoxLayer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(376, 88);
            this.panel1.TabIndex = 55;
            this.cboSelectType.EditValue = "创建一个新的选择集";
            this.cboSelectType.Location = new Point(56, 56);
            this.cboSelectType.Name = "cboSelectType";
            this.cboSelectType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSelectType.Properties.Items.AddRange(new object[] { "创建一个新的选择集", "添加到现有选择集", "从现有选择集中删除", "从现有选择集中选择" });
            this.cboSelectType.Size = new Size(296, 23);
            this.cboSelectType.TabIndex = 51;
            this.cboSelectType.SelectedIndexChanged += new EventHandler(this.cboSelectType_SelectedIndexChanged);
            this.chkShowSelectbaleLayer.Location = new Point(56, 32);
            this.chkShowSelectbaleLayer.Name = "chkShowSelectbaleLayer";
            this.chkShowSelectbaleLayer.Properties.Caption = "只显示可选择图层";
            this.chkShowSelectbaleLayer.Size = new Size(168, 19);
            this.chkShowSelectbaleLayer.TabIndex = 50;
            this.chkShowSelectbaleLayer.CheckedChanged += new EventHandler(this.chkShowSelectbaleLayer_CheckedChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 17);
            this.label3.TabIndex = 49;
            this.label3.Text = "方法:";
            this.comboBoxLayer.EditValue = "";
            this.comboBoxLayer.Location = new Point(56, 7);
            this.comboBoxLayer.Name = "comboBoxLayer";
            this.comboBoxLayer.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxLayer.Size = new Size(288, 23);
            this.comboBoxLayer.TabIndex = 48;
            this.comboBoxLayer.SelectedIndexChanged += new EventHandler(this.comboBoxLayer_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 47;
            this.label1.Text = "图层:";
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new Point(296, 456);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(56, 24);
            this.btnClose.TabIndex = 54;
            this.btnClose.Text = "关闭";
            this.btnApply.Location = new Point(232, 456);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(56, 24);
            this.btnApply.TabIndex = 53;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnApply);
            base.Name = "AttributeQueryControl";
            base.Size = new Size(376, 488);
            base.Load += new EventHandler(this.AttributeQueryControl_Load);
            this.panel1.ResumeLayout(false);
            this.cboSelectType.Properties.EndInit();
            this.chkShowSelectbaleLayer.Properties.EndInit();
            this.comboBoxLayer.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnApply;
        private SimpleButton btnClear;
        private SimpleButton btnClose;
        private ComboBoxEdit cboSelectType;
        private CheckEdit chkShowSelectbaleLayer;
        private ComboBoxEdit comboBoxLayer;
        private IBasicMap ibasicMap_0;
        private Label label1;
        private Label label3;
        private Panel panel1;
        private Panel panel2;
    }
}