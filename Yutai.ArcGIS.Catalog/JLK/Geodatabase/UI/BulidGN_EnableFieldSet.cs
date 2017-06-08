namespace JLK.Geodatabase.UI
{
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BulidGN_EnableFieldSet : UserControl
    {
        private Container container_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private RadioGroup radioGroup1;

        public BulidGN_EnableFieldSet()
        {
            this.InitializeComponent();
        }

        private void BulidGN_EnableFieldSet_Load(object sender, EventArgs e)
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
            this.radioGroup1 = new RadioGroup();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.radioGroup1.Location = new Point(0x10, 0x10);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "否"), new RadioGroupItem(null, "是") });
            this.radioGroup1.Size = new Size(0xe0, 0x88);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xd0, 0x10);
            this.label1.TabIndex = 2;
            this.label1.Text = "是否保存已存在的Enable值";
            this.label2.Location = new Point(0x18, 0x41);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x108, 0x20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enable所有网络要素，这将忽略\"Enable\"字段中的任何属性值";
            this.label3.Location = new Point(0x18, 0x88);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x108, 0x20);
            this.label3.TabIndex = 4;
            this.label3.Text = "保留\"Enable\"字段中已存在的属性值。在字段中无效的属性值将被重置为enable状态";
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.radioGroup1);
            base.Name = "BulidGN_EnableFieldSet";
            base.Size = new Size(0x148, 0xe8);
            base.Load += new EventHandler(this.BulidGN_EnableFieldSet_Load);
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BulidGeometryNetworkHelper.BulidGNHelper.PreserveEnabledValues = this.radioGroup1.SelectedIndex == 1;
        }
    }
}

