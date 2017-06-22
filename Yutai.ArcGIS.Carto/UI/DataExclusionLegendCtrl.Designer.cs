using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class DataExclusionLegendCtrl
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
            this.chkShowExclusionClass = new CheckEdit();
            this.btnStyle = new StyleButton();
            this.txtLabel = new TextEdit();
            this.txtDescription = new MemoEdit();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.chkShowExclusionClass.Properties.BeginInit();
            this.txtLabel.Properties.BeginInit();
            this.txtDescription.Properties.BeginInit();
            base.SuspendLayout();
            this.chkShowExclusionClass.EditValue = false;
            this.chkShowExclusionClass.Location = new System.Drawing.Point(8, 16);
            this.chkShowExclusionClass.Name = "chkShowExclusionClass";
            this.chkShowExclusionClass.Properties.Caption = "显示排除数据";
            this.chkShowExclusionClass.Size = new Size(104, 19);
            this.chkShowExclusionClass.TabIndex = 0;
            this.chkShowExclusionClass.CheckedChanged += new EventHandler(this.chkShowExclusionClass_CheckedChanged);
            this.btnStyle.Location = new System.Drawing.Point(48, 48);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(88, 32);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 43;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.txtLabel.EditValue = "";
            this.txtLabel.Location = new System.Drawing.Point(48, 88);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new Size(136, 21);
            this.txtLabel.TabIndex = 44;
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new System.Drawing.Point(48, 120);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(160, 88);
            this.txtDescription.TabIndex = 45;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 56);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 46;
            this.label1.Text = "符号";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 90);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 17);
            this.label2.TabIndex = 47;
            this.label2.Text = "标注";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 120);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 17);
            this.label3.TabIndex = 48;
            this.label3.Text = "说明";
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.txtLabel);
            base.Controls.Add(this.btnStyle);
            base.Controls.Add(this.chkShowExclusionClass);
            base.Name = "DataExclusionLegendCtrl";
            base.Size = new Size(296, 264);
            base.Load += new EventHandler(this.DataExclusionLegendCtrl_Load);
            this.chkShowExclusionClass.Properties.EndInit();
            this.txtLabel.Properties.EndInit();
            this.txtDescription.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private StyleButton btnStyle;
        private CheckEdit chkShowExclusionClass;
        private Label label1;
        private Label label2;
        private Label label3;
        private MemoEdit txtDescription;
        private TextEdit txtLabel;
    }
}