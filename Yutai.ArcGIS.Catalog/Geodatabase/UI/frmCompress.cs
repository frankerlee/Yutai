using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmCompress : Form
    {
        private Container container_0 = null;
        private IDatabaseCompact idatabaseCompact_0 = null;
        private int int_0 = 0;
        private IVersionedWorkspace iversionedWorkspace_0 = null;

        public frmCompress()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Exception exception;
            if (this.iversionedWorkspace_0 != null)
            {
                try
                {
                    this.iversionedWorkspace_0.Compress();
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    MessageBox.Show(exception.Message);
                }
            }
            else if (this.idatabaseCompact_0 != null)
            {
                try
                {
                    if (this.idatabaseCompact_0.CanCompact())
                    {
                        this.idatabaseCompact_0.Compact();
                    }
                    else
                    {
                        MessageBox.Show("无法整理" + this.textEdit1.Text);
                    }
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            if (this.int_0 == 0)
            {
                file.AddFilter(new MyGxFilterGeoDatabases(), true);
            }
            else
            {
                file.AddFilter(new MyGxFilterPersonalGeodatabases(), true);
            }
            if ((file.DoModalOpen() == DialogResult.OK) && (file.Items.Count > 0))
            {
                IGxObject obj2 = file.Items.get_Element(0) as IGxObject;
                if (obj2 is IGxDatabase)
                {
                    if (this.int_0 == 0)
                    {
                        this.iversionedWorkspace_0 = (obj2 as IGxDatabase).Workspace as IVersionedWorkspace;
                        if (this.iversionedWorkspace_0 == null)
                        {
                            MessageBox.Show("请选择企业数据库!");
                            this.textEdit1.Text = "";
                            this.btnOK.Enabled = false;
                        }
                        else
                        {
                            this.textEdit1.Text = obj2.FullName;
                            this.btnOK.Enabled = true;
                        }
                    }
                    else
                    {
                        this.textEdit1.Text = obj2.FullName;
                        this.idatabaseCompact_0 = (obj2 as IGxDatabase).Workspace as IDatabaseCompact;
                        this.btnOK.Enabled = this.idatabaseCompact_0 != null;
                    }
                }
            }
        }

        private void frmCompress_Load(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    this.Text = "压缩空间数据库";
                    this.label1.Text = "输入空间数据库";
                    break;

                case 1:
                    this.Text = "整理个人空间数据库";
                    this.label1.Text = "输入个人空间数据库";
                    break;
            }
        }

        public int Type
        {
            get { return this.int_0; }
            set { this.int_0 = value; }
        }
    }
}