using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;

namespace Yutai.ArcGIS.Catalog
{
    public partial class frmAddDatabaseServer : Form
    {
        private IContainer icontainer_0 = null;

        public frmAddDatabaseServer()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IDataServerManager manager = new DataServerManagerClass {
                    ServerName = this.textBox1.Text.Trim()
                };
                manager.Connect();
                string pathName = Environment.SystemDirectory.Substring(0, 2) + @"\Documents and Settings\Administrator\Application Data\ESRI\ArcCatalog\";
                manager.CreateConnectionFile(pathName, manager.ServerName);
                base.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

 private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.button1.Enabled = this.textBox1.Text.Trim().Length > 0;
        }
    }
}

