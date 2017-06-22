using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    partial class NAGeneralPropertyPage
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
            this.groupBox1 = new GroupBox();
            this.checkEdit = new CheckEdit();
            this.rdoTrackFeatureSet = new RadioGroup();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtSnapTol = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.checkEdit.Properties.BeginInit();
            this.rdoTrackFeatureSet.Properties.BeginInit();
            this.txtSnapTol.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.checkEdit);
            this.groupBox1.Controls.Add(this.rdoTrackFeatureSet);
            this.groupBox1.Location = new System.Drawing.Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(288, 192);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "追踪要素";
            this.checkEdit.Location = new System.Drawing.Point(8, 112);
            this.checkEdit.Name = "checkEdit";
            this.checkEdit.Properties.Caption = "定向追踪任务包含未确定和未初始化流向的边";
            this.checkEdit.Size = new Size(264, 19);
            this.checkEdit.TabIndex = 1;
            this.checkEdit.CheckedChanged += new EventHandler(this.checkEdit_CheckedChanged);
            this.rdoTrackFeatureSet.Location = new System.Drawing.Point(24, 24);
            this.rdoTrackFeatureSet.Name = "rdoTrackFeatureSet";
            this.rdoTrackFeatureSet.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoTrackFeatureSet.Properties.Appearance.Options.UseBackColor = true;
            this.rdoTrackFeatureSet.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoTrackFeatureSet.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "全部要素"), new RadioGroupItem(null, "选中要素"), new RadioGroupItem(null, "未选中要素") });
            this.rdoTrackFeatureSet.Size = new Size(192, 72);
            this.rdoTrackFeatureSet.TabIndex = 0;
            this.rdoTrackFeatureSet.SelectedIndexChanged += new EventHandler(this.rdoTrackFeatureSet_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 232);
            this.label1.Name = "label1";
            this.label1.Size = new Size(128, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "标记和障碍的捕捉容差";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 232);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "像素";
            this.txtSnapTol.EditValue = "10";
            this.txtSnapTol.Location = new System.Drawing.Point(160, 232);
            this.txtSnapTol.Name = "txtSnapTol";
            this.txtSnapTol.Size = new Size(88, 23);
            this.txtSnapTol.TabIndex = 3;
            this.txtSnapTol.EditValueChanged += new EventHandler(this.txtSnapTol_EditValueChanged);
            base.Controls.Add(this.txtSnapTol);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox1);
            base.Name = "NAGeneralPropertyPage";
            base.Size = new Size(312, 336);
            base.Load += new EventHandler(this.NAGeneralPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.checkEdit.Properties.EndInit();
            this.rdoTrackFeatureSet.Properties.EndInit();
            this.txtSnapTol.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private CheckEdit checkEdit;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private RadioGroup rdoTrackFeatureSet;
        private TextEdit txtSnapTol;
    }
}