using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class GDBGeneralCtrl : UserControl
    {
        private Container container_0 = null;
        private IWorkspace iworkspace_0 = null;

        public GDBGeneralCtrl()
        {
            this.InitializeComponent();
        }

        private void btnConfigKey_Click(object sender, EventArgs e)
        {
            new frmConfigKey {Configuration = this.iworkspace_0 as IWorkspaceConfiguration}.ShowDialog();
        }

        private void btnProperty_Click(object sender, EventArgs e)
        {
        }

        private void btnUnRegister_Click(object sender, EventArgs e)
        {
        }

        private void btnUpdatePersonGDB_Click(object sender, EventArgs e)
        {
            try
            {
                IGeodatabaseRelease release = this.iworkspace_0 as IGeodatabaseRelease;
                if (release.CanUpgrade)
                {
                    release.Upgrade();
                }
                this.lblGDBRelease.Text = "此数据库与你所使用的ArcGIS版本匹配";
                this.btnUpdatePersonGDB.Enabled = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void GDBGeneralCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void method_0()
        {
            this.textEditName.Text = this.iworkspace_0.PathName;
            switch (this.iworkspace_0.Type)
            {
                case esriWorkspaceType.esriLocalDatabaseWorkspace:
                    if (!(Path.GetExtension(this.iworkspace_0.PathName).ToLower() == ".mdb"))
                    {
                        this.lblType.Text = "文件型空间数据库";
                        this.lblConfigKey.Text = "点击按钮列出数据库定义的所有关键字";
                        this.btnConfigKey.Enabled = true;
                        break;
                    }
                    this.lblType.Text = "个人空间数据库";
                    this.lblConfigKey.Text = "个人空间数据库不支持配置关键字";
                    this.btnConfigKey.Enabled = false;
                    break;

                case esriWorkspaceType.esriRemoteDatabaseWorkspace:
                    this.lblType.Text = "ArcSDE Geodatebase连接";
                    this.lblConfigKey.Text = "点击按钮列出数据库定义的所有关键字";
                    this.btnConfigKey.Enabled = true;
                    break;
            }
            if (this.iworkspace_0 is IWorkspaceReplicas)
            {
                IEnumReplica replicas = (this.iworkspace_0 as IWorkspaceReplicas).Replicas;
                replicas.Reset();
                IReplica replica2 = replicas.Next();
                string name = "";
                if (replica2 != null)
                {
                    this.lblCheckOutInfo.Text = "这是个检出数据库。该数据库包含从另外的数据库导出的数据。";
                    this.textEditCheckOutName.Enabled = true;
                    name = replica2.Name;
                    this.textEditCheckOutName.Text = name;
                    this.btnProperty.Enabled = true;
                    this.btnUnRegister.Enabled = true;
                }
                else
                {
                    this.lblCheckOutInfo.Text = "该数据库不包含从另外的数据库导出的数据。";
                    this.textEditCheckOutName.Enabled = false;
                    this.btnProperty.Enabled = false;
                    this.btnUnRegister.Enabled = false;
                }
            }
            else
            {
                this.lblCheckOutInfo.Text = "该数据库不包含从另外的数据库导出的数据。";
                this.textEditCheckOutName.Enabled = false;
                this.btnProperty.Enabled = false;
                this.btnUnRegister.Enabled = false;
            }
            try
            {
                IGeodatabaseRelease release = this.iworkspace_0 as IGeodatabaseRelease;
                if (release.CurrentRelease)
                {
                    this.lblGDBRelease.Text = "此数据库与你所使用的ArcGIS版本匹配";
                    this.btnUpdatePersonGDB.Enabled = false;
                }
                else if (release.CanUpgrade)
                {
                    this.lblGDBRelease.Text = "此数据库与你所使用的ArcGIS版本不匹配";
                    this.btnUpdatePersonGDB.Enabled = true;
                }
                else
                {
                    this.btnUpdatePersonGDB.Enabled = false;
                }
            }
            catch
            {
                this.btnUpdatePersonGDB.Enabled = false;
            }
        }

        public IWorkspace Workspace
        {
            set { this.iworkspace_0 = value; }
        }
    }
}