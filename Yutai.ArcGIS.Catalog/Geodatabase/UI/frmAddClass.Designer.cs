using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmAddClass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddClass));
            this.listBoxControl1 = new ListBoxControl();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            ((ISupportInitialize) this.listBoxControl1).BeginInit();
            base.SuspendLayout();
            this.listBoxControl1.ItemHeight = 17;
            this.listBoxControl1.Location = new Point(8, 8);
            this.listBoxControl1.Name = "listBoxControl1";
            this.listBoxControl1.Size = new Size(160, 152);
            this.listBoxControl1.TabIndex = 0;
            this.listBoxControl1.SelectedIndexChanged += new EventHandler(this.listBoxControl1_SelectedIndexChanged);
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(16, 176);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(88, 176);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(56, 24);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(186, 223);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.listBoxControl1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAddClass";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "增加类";
            base.Load += new EventHandler(this.frmAddClass_Load);
            ((ISupportInitialize) this.listBoxControl1).EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnOK;
        private ListBoxControl listBoxControl1;
        private SimpleButton simpleButton2;
    }
}