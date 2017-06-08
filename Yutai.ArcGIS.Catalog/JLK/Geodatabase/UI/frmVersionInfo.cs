namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmVersionInfo : Form
    {
        private Container container_0 = null;
        private VersionInfoControl versionInfoControl_0 = new VersionInfoControl();

        public frmVersionInfo()
        {
            this.InitializeComponent();
            this.versionInfoControl_0.Dock = DockStyle.Fill;
            base.Controls.Add(this.versionInfoControl_0);
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmVersionInfo));
            base.SuspendLayout();
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x198, 0xf5);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmVersionInfo";
            this.Text = "版本管理器";
            base.ResumeLayout(false);
        }

        public IVersionedWorkspace VersionWorkspace
        {
            set
            {
                this.versionInfoControl_0.VersionWorkspace = value;
            }
        }
    }
}

