using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmLayerProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayerProperty));
            this.panel1 = new Panel();
            this.tabControl1 = new TabControl();
            this.tabPageGeneral = new TabPage();
            this.tabPageDefinitionExpression = new TabPage();
            this.tabPageDisplay = new TabPage();
            this.tabPageLayerLabel = new TabPage();
            this.panel2 = new Panel();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(440, 248);
            this.panel1.TabIndex = 0;
            this.tabControl1.Controls.Add(this.tabPageGeneral);
            this.tabControl1.Controls.Add(this.tabPageDefinitionExpression);
            this.tabControl1.Controls.Add(this.tabPageDisplay);
            this.tabControl1.Controls.Add(this.tabPageLayerLabel);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(440, 248);
            this.tabControl1.TabIndex = 0;
            this.tabPageGeneral.Location = new Point(4, 21);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Size = new Size(432, 223);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "常规";
            this.tabPageDefinitionExpression.Location = new Point(4, 21);
            this.tabPageDefinitionExpression.Name = "tabPageDefinitionExpression";
            this.tabPageDefinitionExpression.Size = new Size(432, 223);
            this.tabPageDefinitionExpression.TabIndex = 1;
            this.tabPageDefinitionExpression.Text = "定义查询";
            this.tabPageDisplay.Location = new Point(4, 21);
            this.tabPageDisplay.Name = "tabPageDisplay";
            this.tabPageDisplay.Size = new Size(432, 223);
            this.tabPageDisplay.TabIndex = 2;
            this.tabPageDisplay.Text = "显示";
            this.tabPageLayerLabel.Location = new Point(4, 21);
            this.tabPageLayerLabel.Name = "tabPageLayerLabel";
            this.tabPageLayerLabel.Size = new Size(432, 223);
            this.tabPageLayerLabel.TabIndex = 3;
            this.tabPageLayerLabel.Text = "标注";
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 248);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(440, 29);
            this.panel2.TabIndex = 1;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(304, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(48, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(240, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(48, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(440, 277);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            
            base.Name = "frmLayerProperty";
            this.Text = "图层属性";
            base.Load += new EventHandler(this.frmLayerProperty_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Panel panel1;
        private Panel panel2;
        private TabControl tabControl1;
        private TabPage tabPageDefinitionExpression;
        private TabPage tabPageDisplay;
        private TabPage tabPageGeneral;
        private TabPage tabPageLayerLabel;
    }
}