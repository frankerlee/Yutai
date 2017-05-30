namespace Yutai.Catalog.UI
{
    
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewNetworkDatasetConnectivityPage : UserControl
    {
        private Button btnConnectivity;
        private IContainer icontainer_0 = null;

        public NewNetworkDatasetConnectivityPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            return true;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.btnConnectivity = new Button();
            base.SuspendLayout();
            this.btnConnectivity.Location = new Point(0x2b, 0x40);
            this.btnConnectivity.Name = "btnConnectivity";
            this.btnConnectivity.Size = new Size(0x6c, 0x17);
            this.btnConnectivity.TabIndex = 0;
            this.btnConnectivity.Text = "连通性设置...";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnConnectivity);
            base.Name = "NewNetworkDatasetConnectivityPage";
            base.Size = new Size(0x112, 0xc1);
            base.ResumeLayout(false);
        }
    }
}

