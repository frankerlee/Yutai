using System;
using System.Collections;
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
    public partial class frmExtractionDataWizard : Form
    {
        private AfterCheckOutSetupCtrl afterCheckOutSetupCtrl_0 = new AfterCheckOutSetupCtrl();
        private Container container_0 = null;
        private ExtractionDataSetupCtrl extractionDataSetupCtrl_0 = new ExtractionDataSetupCtrl();
        private IArray iarray_0 = null;
        private int int_0 = 0;
        private int int_1 = 1;
        private SelectCheckOutWorkspaceCtrl selectCheckOutWorkspaceCtrl_0 = null;

        public frmExtractionDataWizard()
        {
            this.InitializeComponent();
            this.selectCheckOutWorkspaceCtrl_0 = new SelectCheckOutWorkspaceCtrl(2);
            this.panel2.Controls.Add(this.afterCheckOutSetupCtrl_0);
            this.afterCheckOutSetupCtrl_0.Dock = DockStyle.Fill;
            this.afterCheckOutSetupCtrl_0.Visible = false;
            this.panel2.Controls.Add(this.extractionDataSetupCtrl_0);
            this.extractionDataSetupCtrl_0.Dock = DockStyle.Fill;
            this.extractionDataSetupCtrl_0.Visible = false;
            this.panel2.Controls.Add(this.selectCheckOutWorkspaceCtrl_0);
            this.selectCheckOutWorkspaceCtrl_0.Dock = DockStyle.Fill;
            this.selectCheckOutWorkspaceCtrl_0.Visible = false;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    if (this.iarray_0.Count != 1)
                    {
                        this.btnLast.Enabled = false;
                        return;
                    }
                    this.btnLast.Enabled = false;
                    return;

                case 1:
                    if (this.iarray_0.Count != 1)
                    {
                        this.selectCheckOutWorkspaceCtrl_0.Visible = true;
                        this.extractionDataSetupCtrl_0.Visible = false;
                        this.btnLast.Enabled = false;
                        break;
                    }
                    this.btnNext.Text = "下一步";
                    this.btnLast.Enabled = false;
                    this.extractionDataSetupCtrl_0.Visible = true;
                    this.afterCheckOutSetupCtrl_0.Visible = false;
                    break;

                case 2:
                    this.afterCheckOutSetupCtrl_0.Visible = false;
                    this.extractionDataSetupCtrl_0.Visible = true;
                    this.btnNext.Text = "下一步";
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
                        this.btnLast.Enabled = true;
                        this.selectCheckOutWorkspaceCtrl_0.Visible = false;
                        this.extractionDataSetupCtrl_0.Visible = true;
                        break;
                    }
                    if (!this.extractionDataSetupCtrl_0.Do())
                    {
                        return;
                    }
                    this.extractionDataSetupCtrl_0.Visible = false;
                    this.afterCheckOutSetupCtrl_0.Visible = true;
                    this.btnNext.Text = "完成";
                    this.btnLast.Enabled = true;
                    break;

                case 1:
                    if (this.iarray_0.Count != 1)
                    {
                        if (!this.extractionDataSetupCtrl_0.Do())
                        {
                            return;
                        }
                        this.extractionDataSetupCtrl_0.Visible = false;
                        this.afterCheckOutSetupCtrl_0.Visible = true;
                        this.btnNext.Text = "完成";
                        this.btnLast.Enabled = true;
                        break;
                    }
                    this.method_0();
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                    return;

                case 2:
                    this.method_0();
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                    return;
            }
            this.int_0++;
        }

 private void frmExtractionDataWizard_Load(object sender, EventArgs e)
        {
            if (this.iarray_0.Count == 0)
            {
                MessageBox.Show("没有可用于提取的数据!");
                base.Close();
            }
            else if (this.iarray_0.Count == 1)
            {
                CheckInOutWorkspaceInfo info = this.iarray_0.get_Element(0) as CheckInOutWorkspaceInfo;
                ExtractionDataHelper.m_pHelper = new ExtractionDataHelper();
                ExtractionDataHelper.m_pHelper.MasterWorkspaceName = (info.Workspace as IDataset).FullName as IWorkspaceName;
                IEnumNameEdit edit = new NamesEnumeratorClass();
                IEnumName enumName = info.EnumName;
                enumName.Reset();
                IName name = enumName.Next();
                IList list = new ArrayList();
                IList list2 = new ArrayList();
                while (name != null)
                {
                    string str;
                    if (name is IFeatureClassName)
                    {
                        if ((name as IFeatureClassName).FeatureDatasetName != null)
                        {
                            IDatasetName featureDatasetName = (name as IFeatureClassName).FeatureDatasetName;
                            str = featureDatasetName.Name;
                            if (list.IndexOf(str) == -1)
                            {
                                list.Add(str);
                                edit.Add(featureDatasetName as IName);
                            }
                        }
                        else
                        {
                            str = (name as IDatasetName).Name;
                            if (list2.IndexOf(str) == -1)
                            {
                                list2.Add(str);
                                edit.Add(name);
                            }
                        }
                    }
                    else if (name is IFeatureDatasetName)
                    {
                        str = (name as IDatasetName).Name;
                        if (list.IndexOf(str) == -1)
                        {
                            list.Add(str);
                            edit.Add(name);
                        }
                    }
                    else
                    {
                        str = (name as IDatasetName).Name;
                        if (list2.IndexOf(str) == -1)
                        {
                            list2.Add(str);
                            edit.Add(name);
                        }
                    }
                    name = enumName.Next();
                }
                ExtractionDataHelper.m_pHelper.EnumName = edit as IEnumName;
                this.extractionDataSetupCtrl_0.Visible = true;
            }
            else
            {
                ExtractionDataHelper.m_pHelper = new ExtractionDataHelper();
                this.selectCheckOutWorkspaceCtrl_0.WorkspaceArray = this.iarray_0;
                this.selectCheckOutWorkspaceCtrl_0.Visible = true;
            }
        }

 private void method_0()
        {
            this.afterCheckOutSetupCtrl_0.Visible = false;
            this.panelProgress.Visible = true;
            ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_Event pHelper = ExtractionDataHelper.m_pHelper;
            pHelper.Step+=(new ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_StepEventHandler(this.method_7));
            (pHelper as IReplicaProgress_Event).Startup+=(new IReplicaProgress_StartupEventHandler(this.method_11));
            ExtractionDataHelper.m_pHelper.Do();
        }

        private bool method_1()
        {
            return false;
        }

        private void method_10(int int_2)
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
                        if (workspace.Type != esriWorkspaceType.esriFileSystemWorkspace)
                        {
                            (this.method_14(this.iarray_0, workspace).EnumName as IEnumNameEdit).Add(featureClass.FullName);
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
                        if (workspace.Type != esriWorkspaceType.esriFileSystemWorkspace)
                        {
                            (this.method_14(this.iarray_0, workspace).EnumName as IEnumNameEdit).Add(featureClass.FullName);
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
                    if (workspace.Type != esriWorkspaceType.esriFileSystemWorkspace)
                    {
                        (this.method_14(this.iarray_0, workspace).EnumName as IEnumNameEdit).Add(featureClass.FullName);
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
            this.lblCheckFC.Text = string_0;
            Application.DoEvents();
        }

        private void method_3(int int_2)
        {
            this.progressBarFC.Maximum = int_2;
        }

        private void method_4(int int_2)
        {
            this.progressBarFC.Minimum = int_2;
            this.progressBarFC.Value = int_2;
        }

        private void method_5(int int_2)
        {
        }

        private void method_6(int int_2)
        {
            this.int_1 = int_2;
        }

        private void method_7()
        {
            this.progressBarFC.Increment(this.int_1);
            Application.DoEvents();
        }

        private void method_8(esriReplicaProgress esriReplicaProgress_0)
        {
            esriReplicaProgress progress = esriReplicaProgress_0;
            if (progress <= esriReplicaProgress.esriRPRegisteringCheckOut)
            {
                switch (progress)
                {
                    case esriReplicaProgress.esriRPExtractSchema:
                        this.lblCheckOutType.Text = "ExtractSchema";
                        goto Label_00FA;

                    case esriReplicaProgress.esriRPExtractData:
                        this.lblCheckOutType.Text = "ExtractData";
                        goto Label_00FA;

                    case (esriReplicaProgress.esriRPExtractData | esriReplicaProgress.esriRPExtractSchema):
                        goto Label_00AE;

                    case esriReplicaProgress.esriRPExtractSchemaAndData:
                        this.lblCheckOutType.Text = "提取数据和方案";
                        goto Label_00FA;

                    case esriReplicaProgress.esriRPFetchRelatedObjects:
                        this.lblCheckOutType.Text = "FetchRelatedObjects";
                        goto Label_00FA;
                }
                if (progress != esriReplicaProgress.esriRPRegisteringCheckOut)
                {
                    goto Label_00AE;
                }
                this.lblCheckOutType.Text = " gg";
                goto Label_00FA;
            }
            switch (progress)
            {
                case esriReplicaProgress.esriRPCreatingCheckOut:
                    this.lblCheckOutType.Text = "CreatingCheckOut";
                    goto Label_00FA;

                case esriReplicaProgress.esriRPSynchronizingCheckOut:
                    this.lblCheckOutType.Text = "RPSynchronizingCheckOut";
                    goto Label_00FA;

                case esriReplicaProgress.esriRPCreatingReplica:
                    this.lblCheckOutType.Text = "CreatingReplica";
                    goto Label_00FA;
            }
        Label_00AE:
            this.lblCheckOutType.Text = esriReplicaProgress_0.ToString();
        Label_00FA:
            Application.DoEvents();
        }

        private void method_9(int int_2)
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

