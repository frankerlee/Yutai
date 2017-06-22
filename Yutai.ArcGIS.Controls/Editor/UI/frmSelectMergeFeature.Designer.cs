using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmSelectMergeFeature
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectMergeFeature));
            this.listBox1 = new ListBox();
            this.label1 = new Label();
            this.btnOK = new Button();
            this.button2 = new Button();
            base.SuspendLayout();
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(8, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(152, 160);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(125, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择将错误并入的要素";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.FlatStyle = FlatStyle.Flat;
            this.btnOK.Location = new Point(8, 208);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.FlatStyle = FlatStyle.Flat;
            this.button2.Location = new Point(88, 208);
            this.button2.Name = "button2";
            this.button2.Size = new Size(64, 24);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(192, 245);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listBox1);
            
            base.Name = "frmSelectMergeFeature";
            this.Text = "合并";
            base.Load += new EventHandler(this.frmSelectMergeFeature_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private Button btnOK;
        private Button button2;
        private Label label1;
        private ListBox listBox1;
    }
}