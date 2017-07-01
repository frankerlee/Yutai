using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.Library
{
    public partial class frmClipOutSet : Form
    {
        private IContainer icontainer_0 = null;
        private int int_0 = 0;
        private int int_1 = 0;
        private IWorkspace iworkspace_0 = null;

        public frmClipOutSet()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                this.int_1 = 0;
                Directory.CreateDirectory(this.textBox1.Text);
                IWorkspaceFactory factory = new ShapefileWorkspaceFactoryClass();
                this.iworkspace_0 = factory.OpenFromFile(this.textBox1.Text, 0);
            }
            else if (this.radioButton2.Checked)
            {
                this.int_1 = 1;
            }
            else if (this.radioButton3.Checked)
            {
                this.int_1 = 2;
            }
            base.DialogResult = DialogResult.OK;
        }

        private void btnOutSet_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog;
            if (this.radioButton1.Checked)
            {
                dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.textBox1.Text = dialog.SelectedPath;
                }
            }
            else if (this.radioButton2.Checked)
            {
                dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.textBox1.Text = dialog.SelectedPath;
                }
            }
            else if (this.radioButton3.Checked)
            {
                SaveFileDialog dialog2 = new SaveFileDialog
                {
                    Filter = "*.vct|*.vct"
                };
                if (dialog2.ShowDialog() == DialogResult.OK)
                {
                    this.textBox1.Text = dialog2.FileName;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox1.Tag = null;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox1.Tag = null;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox1.Tag = null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = this.textBox1.Text.Length > 0;
        }

        public string OutPath
        {
            get { return this.textBox1.Text; }
        }

        public IWorkspace OutWorspace
        {
            get { return this.iworkspace_0; }
        }

        public int Type
        {
            get { return this.int_1; }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }
    }
}