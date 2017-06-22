using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmLayerRender
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayerRender));
            this.btnOK = new SimpleButton();
            this.panel1 = new Panel();
            this.cboRenderType = new ComboBoxEdit();
            this.label2 = new Label();
            this.panel2 = new Panel();
            this.btnCancel = new SimpleButton();
            this.panel1.SuspendLayout();
            this.cboRenderType.Properties.BeginInit();
            base.SuspendLayout();
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(344, 344);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel1.Controls.Add(this.cboRenderType);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(488, 32);
            this.panel1.TabIndex = 7;
            this.cboRenderType.EditValue = "";
            this.cboRenderType.Location = new System.Drawing.Point(77, 4);
            this.cboRenderType.Name = "cboRenderType";
            this.cboRenderType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboRenderType.Size = new Size(354, 21);
            this.cboRenderType.TabIndex = 5;
            this.cboRenderType.SelectedIndexChanged += new EventHandler(this.cboRenderType_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(59, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "渲染类型:";
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(488, 304);
            this.panel2.TabIndex = 8;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(416, 344);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(488, 373);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnOK);
            base.Icon = (Icon)resources.GetObject("$this.Icon");
            base.Name = "frmLayerRender";
            this.Text = "图层渲染";
            base.Load += new EventHandler(this.frmLayerRender_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cboRenderType.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private ComboBoxEdit cboRenderType;
        private Label label2;
        private Panel panel1;
        private Panel panel2;
    }
}