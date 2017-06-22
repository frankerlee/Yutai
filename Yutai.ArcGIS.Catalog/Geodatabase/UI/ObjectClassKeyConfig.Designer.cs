using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class ObjectClassKeyConfig
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
            this.radioGroup1 = new RadioGroup();
            this.label1 = new Label();
            this.groupBox1.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new Point(24, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(296, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "配置关键字";
            this.radioGroup1.Location = new Point(16, 32);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Enabled = false;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "默认"), new RadioGroupItem(null, "使用配置关键字") });
            this.radioGroup1.Size = new Size(264, 48);
            this.radioGroup1.TabIndex = 0;
            this.label1.Location = new Point(32, 32);
            this.label1.Name = "label1";
            this.label1.Size = new Size(136, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "指定数据库的存储配置";
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox1);
            base.Name = "ObjectClassKeyConfig";
            base.Size = new Size(336, 296);
            this.groupBox1.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private GroupBox groupBox1;
        private Label label1;
        private RadioGroup radioGroup1;
    }
}