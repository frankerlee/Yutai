using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Carto;

namespace Yutai.Plugins.Printing.Forms
{
    internal partial class frmDir : System.Windows.Forms.Form
    {
        private IGeometry igeometry_0 = null;

        private IMap imap_0 = null;


        private Container container_0 = null;

        public IMap FocusMap
        {
            set { this.imap_0 = value; }
        }

        public IGeometry ClipGeometry
        {
            set { this.igeometry_0 = value; }
        }

        public frmDir()
        {
            this.InitializeComponent();
        }

        private void frmDir_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != null && this.textBox1.Text.Length >= 2)
            {
                try
                {
                    IWorkspaceName workspaceName = new WorkspaceName() as IWorkspaceName;
                    workspaceName.WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory";
                    workspaceName.PathName = this.textBox1.Text;
                    if (!Directory.Exists(this.textBox1.Text))
                    {
                        Directory.CreateDirectory(this.textBox1.Text);
                    }
                    Clip.ExtractSpecifyHRegFeatures(workspaceName, this.imap_0, this.igeometry_0);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                base.Close();
            }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = dialog.SelectedPath;
            }
        }
    }
}