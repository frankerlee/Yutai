namespace JLK.Catalog.UI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class ServerObjectSummaryPropertyPage : UserControl
    {
        private Container container_0 = null;

        public ServerObjectSummaryPropertyPage()
        {
            this.InitializeComponent();
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
            base.Name = "ServerObjectSummaryPropertyPage";
            base.Size = new Size(0x130, 0x138);
        }
    }
}

