using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class BulidGN_SnapSet
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
            this.groupBox1 = new GroupBox();
            this.radioGroup1 = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.btnClear = new SimpleButton();
            this.btnSelectAll = new SimpleButton();
            this.chkChangeFC = new CheckedListBox();
            this.label2 = new Label();
            this.label1 = new Label();
            this.txtSnaptol = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtSnaptol.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(224, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "是否捕捉要素";
            this.radioGroup1.Location = new Point(16, 16);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "否"), new RadioGroupItem(null, "是") });
            this.radioGroup1.Size = new Size(152, 24);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnSelectAll);
            this.groupBox2.Controls.Add(this.chkChangeFC);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtSnaptol);
            this.groupBox2.Location = new Point(8, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(224, 192);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "捕捉设置";
            this.btnClear.Location = new Point(176, 128);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(40, 24);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "取消";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.btnSelectAll.Location = new Point(176, 96);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(40, 24);
            this.btnSelectAll.TabIndex = 6;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.chkChangeFC.Location = new Point(8, 96);
            this.chkChangeFC.Name = "chkChangeFC";
            this.chkChangeFC.Size = new Size(160, 84);
            this.chkChangeFC.TabIndex = 3;
            this.chkChangeFC.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkChangeFC_ItemCheck);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 72);
            this.label2.Name = "label2";
            this.label2.Size = new Size(128, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "选择可以移动的要素类";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(54, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "捕捉容差";
            this.txtSnaptol.Location = new Point(8, 40);
            this.txtSnaptol.Name = "txtSnaptol";
            this.txtSnaptol.Size = new Size(152, 23);
            this.txtSnaptol.TabIndex = 1;
            this.txtSnaptol.EditValueChanged += new EventHandler(this.txtSnaptol_EditValueChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "BulidGN_SnapSet";
            base.Size = new Size(248, 296);
            base.Load += new EventHandler(this.BulidGN_SnapSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtSnaptol.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnClear;
        private SimpleButton btnSelectAll;
        private CheckedListBox chkChangeFC;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private RadioGroup radioGroup1;
        private TextEdit txtSnaptol;
    }
}