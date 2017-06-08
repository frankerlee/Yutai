namespace JLK.Geodatabase.UI
{
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BulidGeometryNetworkType : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private RadioGroup rdoCreateType;

        public BulidGeometryNetworkType()
        {
            this.InitializeComponent();
        }

        private void BulidGeometryNetworkType_Load(object sender, EventArgs e)
        {
            if (BulidGeometryNetworkHelper.BulidGNHelper.IsEmpty)
            {
                this.rdoCreateType.SelectedIndex = 1;
            }
            else
            {
                this.rdoCreateType.SelectedIndex = 0;
            }
            this.bool_0 = true;
        }

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
            this.rdoCreateType = new RadioGroup();
            this.rdoCreateType.Properties.BeginInit();
            base.SuspendLayout();
            this.rdoCreateType.Location = new Point(0x18, 0x20);
            this.rdoCreateType.Name = "rdoCreateType";
            this.rdoCreateType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoCreateType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoCreateType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoCreateType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "利用已有几何要素创建几何网络"), new RadioGroupItem(null, "创建空几何网络") });
            this.rdoCreateType.Size = new Size(200, 0x70);
            this.rdoCreateType.TabIndex = 0;
            this.rdoCreateType.SelectedIndexChanged += new EventHandler(this.rdoCreateType_SelectedIndexChanged);
            base.Controls.Add(this.rdoCreateType);
            base.Name = "BulidGeometryNetworkType";
            base.Size = new Size(0x110, 200);
            base.Load += new EventHandler(this.BulidGeometryNetworkType_Load);
            this.rdoCreateType.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void rdoCreateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.rdoCreateType.SelectedIndex == 1)
                {
                    BulidGeometryNetworkHelper.BulidGNHelper.IsEmpty = true;
                }
                else
                {
                    BulidGeometryNetworkHelper.BulidGNHelper.IsEmpty = false;
                }
            }
        }
    }
}

