using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseDistributed;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmExportChangeWizard : Form
    {
        private Container container_0 = null;
        private ExportChangeSetupCtrl exportChangeSetupCtrl_0 = new ExportChangeSetupCtrl();
        private IArray iarray_0 = null;
        private int int_0 = 0;
        private SelectCheckOutWorkspaceCtrl selectCheckOutWorkspaceCtrl_0 = null;

        public frmExportChangeWizard()
        {
            this.InitializeComponent();
            this.selectCheckOutWorkspaceCtrl_0 = new SelectCheckOutWorkspaceCtrl(3);
            this.panel2.Controls.Add(this.selectCheckOutWorkspaceCtrl_0);
            this.selectCheckOutWorkspaceCtrl_0.Dock = DockStyle.Fill;
            this.selectCheckOutWorkspaceCtrl_0.Visible = false;
            this.panel2.Controls.Add(this.exportChangeSetupCtrl_0);
            this.exportChangeSetupCtrl_0.Dock = DockStyle.Fill;
            this.exportChangeSetupCtrl_0.Visible = false;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    return;

                case 1:
                    this.btnLast.Enabled = false;
                    this.btnNext.Text = "下一步";
                    this.selectCheckOutWorkspaceCtrl_0.Visible = true;
                    this.exportChangeSetupCtrl_0.Visible = false;
                    break;
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    if (this.iarray_0.Count != 1)
                    {
                        if (!this.selectCheckOutWorkspaceCtrl_0.Do())
                        {
                            return;
                        }
                        this.selectCheckOutWorkspaceCtrl_0.Visible = false;
                        this.exportChangeSetupCtrl_0.Visible = true;
                        this.btnLast.Enabled = true;
                        this.btnNext.Text = "完成";
                        break;
                    }
                    if (this.exportChangeSetupCtrl_0.Do())
                    {
                        this.method_0();
                        base.Close();
                    }
                    return;

                case 1:
                    if (this.exportChangeSetupCtrl_0.Do())
                    {
                        this.method_0();
                        base.Close();
                    }
                    return;
            }
            this.int_0++;
        }

 private void frmExportChangeWizard_Load(object sender, EventArgs e)
        {
            if (this.iarray_0.Count == 0)
            {
                MessageBox.Show("没有检出的数据!");
                base.Close();
            }
            else if (this.iarray_0.Count == 1)
            {
                CheckInOutWorkspaceInfo info = this.iarray_0.get_Element(0) as CheckInOutWorkspaceInfo;
                ExportChangesHelper.m_pHelper = new ExportChangesHelper();
                ExportChangesHelper.m_pHelper.CheckoutWorkspaceName = (info.Workspace as IDataset).FullName as IWorkspaceName;
                this.exportChangeSetupCtrl_0.Visible = true;
                this.btnLast.Enabled = false;
                this.btnNext.Text = "完成";
            }
            else
            {
                ExportChangesHelper.m_pHelper = new ExportChangesHelper();
                this.selectCheckOutWorkspaceCtrl_0.WorkspaceArray = this.iarray_0;
                this.selectCheckOutWorkspaceCtrl_0.Visible = true;
            }
        }

 private void method_0()
        {
            ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_Event pHelper = ExportChangesHelper.m_pHelper;
            pHelper.Step+=(new ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_StepEventHandler(this.method_7));
            (pHelper as IReplicaProgress_Event).Startup+=(new IReplicaProgress_StartupEventHandler(this.method_11));
            ExportChangesHelper.m_pHelper.Do();
        }

        private bool method_1()
        {
            return false;
        }

        private void method_10(int int_1)
        {
        }

        private void method_11(esriReplicaProgress esriReplicaProgress_0)
        {
        }

        private void method_12(IArray iarray_1, ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_12(iarray_1, layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    IFeatureLayer layer2 = layer as IFeatureLayer;
                    IDataset featureClass = layer2.FeatureClass as IDataset;
                    if ((featureClass != null) && !(featureClass is ICoverageFeatureClass))
                    {
                        IWorkspace workspace = featureClass.Workspace;
                        if (workspace is IWorkspaceReplicas)
                        {
                            IEnumReplica replicas = (workspace as IWorkspaceReplicas).Replicas;
                            replicas.Reset();
                            if (replicas.Next() != null)
                            {
                                (this.method_14(this.iarray_0, workspace).EnumName as IEnumNameEdit).Add(featureClass.FullName);
                            }
                        }
                    }
                }
            }
        }

        private void method_13(IMap imap_0)
        {
            int num;
            IDataset featureClass;
            IWorkspace workspace;
            IEnumReplica replicas;
            this.iarray_0 = new ArrayClass();
            new PropertySetClass();
            IFeatureLayer layer = null;
            for (num = 0; num < imap_0.LayerCount; num++)
            {
                ILayer layer2 = imap_0.get_Layer(num);
                if (layer2 is IGroupLayer)
                {
                    this.method_12(this.iarray_0, layer2 as ICompositeLayer);
                }
                else if (layer2 is IFeatureLayer)
                {
                    layer = layer2 as IFeatureLayer;
                    featureClass = layer.FeatureClass as IDataset;
                    if ((featureClass != null) && !(featureClass is ICoverageFeatureClass))
                    {
                        workspace = featureClass.Workspace;
                        if (workspace is IWorkspaceReplicas)
                        {
                            replicas = (workspace as IWorkspaceReplicas).Replicas;
                            replicas.Reset();
                            if (replicas.Next() != null)
                            {
                                (this.method_14(this.iarray_0, workspace).EnumName as IEnumNameEdit).Add(featureClass.FullName);
                            }
                        }
                    }
                }
            }
            IStandaloneTableCollection tables = imap_0 as IStandaloneTableCollection;
            for (num = 0; num < tables.StandaloneTableCount; num++)
            {
                ITable table = tables.get_StandaloneTable(num) as ITable;
                if (table is IDataset)
                {
                    featureClass = table as IDataset;
                    workspace = featureClass.Workspace;
                    if (workspace is IWorkspaceReplicas)
                    {
                        replicas = (workspace as IWorkspaceReplicas).Replicas;
                        replicas.Reset();
                        if (replicas.Next() != null)
                        {
                            (this.method_14(this.iarray_0, workspace).EnumName as IEnumNameEdit).Add(featureClass.FullName);
                        }
                    }
                }
            }
        }

        private CheckInOutWorkspaceInfo method_14(IArray iarray_1, IWorkspace iworkspace_0)
        {
            CheckInOutWorkspaceInfo info;
            IPropertySet connectionProperties = iworkspace_0.ConnectionProperties;
            for (int i = 0; i < iarray_1.Count; i++)
            {
                info = iarray_1.get_Element(i) as CheckInOutWorkspaceInfo;
                if (connectionProperties.IsEqual(info.Workspace.ConnectionProperties))
                {
                    return info;
                }
            }
            info = new CheckInOutWorkspaceInfo(iworkspace_0);
            iarray_1.Add(info);
            return info;
        }

        private void method_2(string string_0)
        {
        }

        private void method_3(int int_1)
        {
        }

        private void method_4(int int_1)
        {
        }

        private void method_5(int int_1)
        {
        }

        private void method_6(int int_1)
        {
        }

        private void method_7()
        {
        }

        private void method_8(esriReplicaProgress esriReplicaProgress_0)
        {
        }

        private void method_9(int int_1)
        {
        }

        public object Hook
        {
            set
            {
                if (value is IMap)
                {
                    this.method_13(value as IMap);
                }
            }
        }
    }
}

