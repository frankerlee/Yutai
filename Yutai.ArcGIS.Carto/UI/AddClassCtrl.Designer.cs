using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class AddClassCtrl
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
            this.groupBox1 = new GroupBox();
            this.label1 = new Label();
            this.txtClassName = new TextEdit();
            this.btnAdd = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.txtClassName.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txtClassName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(288, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "添加标注类";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "类名";
            this.txtClassName.EditValue = "";
            this.txtClassName.Location = new Point(56, 24);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new Size(128, 23);
            this.txtClassName.TabIndex = 1;
            this.btnAdd.Location = new Point(208, 24);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(64, 24);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            base.Controls.Add(this.groupBox1);
            base.Name = "AddClassCtrl";
            base.Size = new Size(360, 232);
            this.groupBox1.ResumeLayout(false);
            this.txtClassName.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAdd;
        private GroupBox groupBox1;
        private Label label1;
        private TextEdit txtClassName;
    }
}