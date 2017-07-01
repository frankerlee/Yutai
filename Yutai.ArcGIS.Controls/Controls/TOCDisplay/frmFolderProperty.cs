using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    internal partial class frmFolderProperty : Form
    {
        private string m_FolderName = "";

        public frmFolderProperty()
        {
            this.InitializeComponent();
        }

        private void frmFolderProperty_Load(object sender, EventArgs e)
        {
            this.textEdit1.Text = this.m_FolderName;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.m_FolderName = this.textEdit1.Text;
        }

        public string FolderName
        {
            get { return this.m_FolderName; }
            set { this.m_FolderName = value; }
        }
    }
}