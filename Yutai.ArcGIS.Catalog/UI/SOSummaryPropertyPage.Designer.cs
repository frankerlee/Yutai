using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class SOSummaryPropertyPage
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
            this.label1 = new Label();
            this.memoEdit1 = new MemoEdit();
            this.radioGroup1 = new RadioGroup();
            this.label2 = new Label();
            this.memoEdit1.Properties.BeginInit();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(153, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "将创建以下server objects";
            this.memoEdit1.EditValue = "";
            this.memoEdit1.Location = new Point(24, 48);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new Size(232, 128);
            this.memoEdit1.TabIndex = 1;
            this.radioGroup1.Location = new Point(32, 216);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "否，以后再启动server object"), new RadioGroupItem(null, "是，现在启动server object") });
            this.radioGroup1.Size = new Size(184, 56);
            this.radioGroup1.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(24, 192);
            this.label2.Name = "label2";
            this.label2.Size = new Size(171, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "是否现在启动server objects?";
            base.Controls.Add(this.label2);
            base.Controls.Add(this.radioGroup1);
            base.Controls.Add(this.memoEdit1);
            base.Controls.Add(this.label1);
            base.Name = "SOSummaryPropertyPage";
            base.Size = new Size(304, 288);
            base.Load += new EventHandler(this.SOSummaryPropertyPage_Load);
            this.memoEdit1.Properties.EndInit();
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Label label1;
        private Label label2;
        private MemoEdit memoEdit1;
        private RadioGroup radioGroup1;
    }
}