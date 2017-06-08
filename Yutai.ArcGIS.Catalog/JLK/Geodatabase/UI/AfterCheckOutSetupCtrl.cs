namespace JLK.Geodatabase.UI
{
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Windows.Forms;

    internal class AfterCheckOutSetupCtrl : UserControl
    {
        private SimpleButton btnSave;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private Panel panel1;
        private RadioGroup radioGroup1;
        private TextEdit txtMXD;

        public AfterCheckOutSetupCtrl()
        {
            this.InitializeComponent();
        }

        private void AfterCheckOutSetupCtrl_Load(object sender, EventArgs e)
        {
        }

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
            ResourceManager manager = new ResourceManager(typeof(AfterCheckOutSetupCtrl));
            this.groupBox1 = new GroupBox();
            this.panel1 = new Panel();
            this.btnSave = new SimpleButton();
            this.txtMXD = new TextEdit();
            this.radioGroup1 = new RadioGroup();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.txtMXD.Properties.BeginInit();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x100, 0xb8);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据检出后的操作";
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.txtMXD);
            this.panel1.Enabled = false;
            this.panel1.Location = new Point(3, 0x68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xf8, 0x30);
            this.panel1.TabIndex = 1;
            this.btnSave.Image = (Image) manager.GetObject("btnSave.Image");
            this.btnSave.Location = new Point(0xd8, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(0x18, 0x18);
            this.btnSave.TabIndex = 10;
            this.txtMXD.EditValue = "";
            this.txtMXD.Location = new Point(0x10, 8);
            this.txtMXD.Name = "txtMXD";
            this.txtMXD.Size = new Size(0xb8, 0x17);
            this.txtMXD.TabIndex = 0;
            this.radioGroup1.Location = new Point(8, 0x18);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Enabled = false;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "不进行更多操作"), new RadioGroupItem(null, "更改层和表指向检出数据"), new RadioGroupItem(null, "另存一份包含指向检出数据的地图文档") });
            this.radioGroup1.Size = new Size(240, 0x58);
            this.radioGroup1.TabIndex = 0;
            base.Controls.Add(this.groupBox1);
            base.Name = "AfterCheckOutSetupCtrl";
            base.Size = new Size(0x130, 0x108);
            base.Load += new EventHandler(this.AfterCheckOutSetupCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.txtMXD.Properties.EndInit();
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
        }
    }
}

