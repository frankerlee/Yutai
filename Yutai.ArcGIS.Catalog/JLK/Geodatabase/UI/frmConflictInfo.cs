namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.Geodatabase;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmConflictInfo : Form
    {
        private ConflictInfoControl conflictInfoControl_0 = new ConflictInfoControl();
        private Container container_0 = null;

        public frmConflictInfo()
        {
            this.InitializeComponent();
            this.conflictInfoControl_0.Dock = DockStyle.Fill;
            base.Controls.Add(this.conflictInfoControl_0);
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmConflictInfo));
            base.SuspendLayout();
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x192, 0x10f);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmConflictInfo";
            this.Text = "冲突";
            base.ResumeLayout(false);
        }

        public IWorkspace EditWorkspace
        {
            set
            {
                this.conflictInfoControl_0.EditWorkspace = value;
            }
        }

        public IMap FocusMap
        {
            set
            {
                this.conflictInfoControl_0.FocusMap = value;
            }
        }
    }
}

