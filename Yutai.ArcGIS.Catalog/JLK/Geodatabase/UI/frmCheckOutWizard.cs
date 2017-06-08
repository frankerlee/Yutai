namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.DataSourcesFile;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.GeoDatabaseDistributed;
    using JLK.Editors;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmCheckOutWizard : Form
    {
        private AfterCheckOutSetupCtrl afterCheckOutSetupCtrl_0 = new AfterCheckOutSetupCtrl();
        private SimpleButton btnCancel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private CheckOutSetupCtrl checkOutSetupCtrl_0 = new CheckOutSetupCtrl();
        private Container container_0 = null;
        private IArray iarray_0 = null;
        private int int_0 = 0;
        private int int_1 = 0;
        private int int_2 = 1;
        private Label lblCheckFC;
        private Label lblCheckOutType;
        private Panel panel1;
        private Panel panel2;
        private Panel panelProgress;
        private System.Windows.Forms.ProgressBar progressBarFC;
        private SelectCheckOutWorkspaceCtrl selectCheckOutWorkspaceCtrl_0 = null;

        public frmCheckOutWizard()
        {
            this.InitializeComponent();
            this.int_0 = 0;
            switch (this.int_0)
            {
                case 0:
                    this.Text = "检出数据向导";
                    break;

                case 1:
                    this.Text = "检入数据向导";
                    break;

                case 2:
                    this.Text = "提取数据向导";
                    break;

                case 3:
                    this.Text = "导出变化向导";
                    break;
            }
            this.selectCheckOutWorkspaceCtrl_0 = new SelectCheckOutWorkspaceCtrl(0);
            this.panel2.Controls.Add(this.afterCheckOutSetupCtrl_0);
            this.afterCheckOutSetupCtrl_0.Dock = DockStyle.Fill;
            this.afterCheckOutSetupCtrl_0.Visible = false;
            this.panel2.Controls.Add(this.checkOutSetupCtrl_0);
            this.checkOutSetupCtrl_0.Dock = DockStyle.Fill;
            this.checkOutSetupCtrl_0.Visible = false;
            this.panel2.Controls.Add(this.selectCheckOutWorkspaceCtrl_0);
            this.selectCheckOutWorkspaceCtrl_0.Dock = DockStyle.Fill;
            this.selectCheckOutWorkspaceCtrl_0.Visible = false;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (this.int_0 != 0)
            {
                if (this.int_0 != 1)
                {
                }
            }
            else
            {
                switch (this.int_1)
                {
                    case 0:
                        return;

                    case 1:
                        if (this.iarray_0.Count != 1)
                        {
                            this.selectCheckOutWorkspaceCtrl_0.Visible = true;
                            this.checkOutSetupCtrl_0.Visible = false;
                            this.btnLast.Enabled = false;
                            break;
                        }
                        this.btnNext.Text = "上一步";
                        this.btnLast.Enabled = false;
                        this.checkOutSetupCtrl_0.Visible = true;
                        this.afterCheckOutSetupCtrl_0.Visible = false;
                        break;

                    case 2:
                        this.afterCheckOutSetupCtrl_0.Visible = false;
                        this.checkOutSetupCtrl_0.Visible = true;
                        this.btnNext.Text = "上一步";
                        break;
                }
            }
            this.int_1--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.int_0 != 0)
            {
                if (this.int_0 != 1)
                {
                }
            }
            else
            {
                switch (this.int_1)
                {
                    case 0:
                        if (this.iarray_0.Count != 1)
                        {
                            if (!this.selectCheckOutWorkspaceCtrl_0.Do())
                            {
                                return;
                            }
                            this.selectCheckOutWorkspaceCtrl_0.Visible = false;
                            this.checkOutSetupCtrl_0.Visible = true;
                            break;
                        }
                        if (!this.checkOutSetupCtrl_0.Do())
                        {
                            return;
                        }
                        this.checkOutSetupCtrl_0.Visible = false;
                        this.afterCheckOutSetupCtrl_0.Visible = true;
                        this.btnNext.Text = "完成";
                        this.btnLast.Enabled = true;
                        break;

                    case 1:
                        if (this.iarray_0.Count != 1)
                        {
                            if (!this.checkOutSetupCtrl_0.Do())
                            {
                                return;
                            }
                            this.checkOutSetupCtrl_0.Visible = false;
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
            }
            this.int_1++;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmCheckOutWizard_Load(object sender, EventArgs e)
        {
            if (this.iarray_0.Count == 0)
            {
                MessageBox.Show("没有检出的数据!");
                base.Close();
            }
            else if (this.iarray_0.Count == 1)
            {
                CheckInOutWorkspaceInfo info = this.iarray_0.get_Element(0) as CheckInOutWorkspaceInfo;
                CheckOutHelper.m_pHelper = new CheckOutHelper();
                CheckOutHelper.m_pHelper.MasterWorkspaceName = (info.Workspace as IDataset).FullName as IWorkspaceName;
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
                CheckOutHelper.m_pHelper.EnumName = edit as IEnumName;
                this.checkOutSetupCtrl_0.Visible = true;
            }
            else
            {
                CheckOutHelper.m_pHelper = new CheckOutHelper();
                this.selectCheckOutWorkspaceCtrl_0.WorkspaceArray = this.iarray_0;
                this.selectCheckOutWorkspaceCtrl_0.Visible = true;
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmCheckOutWizard));
            this.panel1 = new Panel();
            this.btnCancel = new SimpleButton();
            this.btnNext = new SimpleButton();
            this.btnLast = new SimpleButton();
            this.panel2 = new Panel();
            this.panelProgress = new Panel();
            this.progressBarFC = new System.Windows.Forms.ProgressBar();
            this.lblCheckFC = new Label();
            this.lblCheckOutType = new Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelProgress.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x11d);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(440, 40);
            this.panel1.TabIndex = 1;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x158, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnNext.Location = new Point(0x110, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x38, 0x18);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new Point(200, 8);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x38, 0x18);
            this.btnLast.TabIndex = 3;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.panel2.Controls.Add(this.panelProgress);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(440, 0x11d);
            this.panel2.TabIndex = 2;
            this.panelProgress.Controls.Add(this.progressBarFC);
            this.panelProgress.Controls.Add(this.lblCheckFC);
            this.panelProgress.Controls.Add(this.lblCheckOutType);
            this.panelProgress.Location = new Point(0x30, 0x18);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new Size(360, 0xe0);
            this.panelProgress.TabIndex = 0;
            this.panelProgress.Visible = false;
            this.progressBarFC.Location = new Point(8, 0x70);
            this.progressBarFC.Name = "progressBarFC";
            this.progressBarFC.Size = new Size(0xf8, 0x10);
            this.progressBarFC.TabIndex = 2;
            this.lblCheckFC.AutoSize = true;
            this.lblCheckFC.Location = new Point(0x10, 80);
            this.lblCheckFC.Name = "lblCheckFC";
            this.lblCheckFC.Size = new Size(0, 12);
            this.lblCheckFC.TabIndex = 1;
            this.lblCheckOutType.AutoSize = true;
            this.lblCheckOutType.Location = new Point(0x10, 40);
            this.lblCheckOutType.Name = "lblCheckOutType";
            this.lblCheckOutType.Size = new Size(0, 12);
            this.lblCheckOutType.TabIndex = 0;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(440, 0x145);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.Name = "frmCheckOutWizard";
            base.Load += new EventHandler(this.frmCheckOutWizard_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            this.afterCheckOutSetupCtrl_0.Visible = false;
            this.panelProgress.Visible = true;
            ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_Event pHelper = CheckOutHelper.m_pHelper;
            pHelper.add_Step(new ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_StepEventHandler(this.method_7));
            (pHelper as IReplicaProgress_Event).add_Startup(new IReplicaProgress_StartupEventHandler(this.method_11));
            CheckOutHelper.m_pHelper.Do();
        }

        private bool method_1()
        {
            return false;
        }

        private void method_10(int int_3)
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
                        if ((workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) && (workspace is IWorkspaceEdit))
                        {
                            IVersionedObject obj2 = layer2.FeatureClass as IVersionedObject;
                            if (obj2.IsRegisteredAsVersioned)
                            {
                                (this.method_14(iarray_1, workspace).EnumName as IEnumNameEdit).Add(featureClass.FullName);
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
            IVersionedObject obj2;
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
                        if ((workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) && (workspace is IWorkspaceEdit))
                        {
                            obj2 = layer.FeatureClass as IVersionedObject;
                            if (obj2.IsRegisteredAsVersioned)
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
                    if ((workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) && (workspace is IWorkspaceEdit))
                    {
                        obj2 = table as IVersionedObject;
                        if (obj2.IsRegisteredAsVersioned)
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
            this.lblCheckFC.Text = string_0;
            Application.DoEvents();
        }

        private void method_3(int int_3)
        {
            this.progressBarFC.Maximum = int_3;
        }

        private void method_4(int int_3)
        {
            this.progressBarFC.Minimum = int_3;
            this.progressBarFC.Value = int_3;
        }

        private void method_5(int int_3)
        {
        }

        private void method_6(int int_3)
        {
            this.int_2 = int_3;
        }

        private void method_7()
        {
            this.progressBarFC.Increment(this.int_2);
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
                        this.lblCheckOutType.Text = "检出数据和方案";
                        goto Label_00FA;

                    case esriReplicaProgress.esriRPFetchRelatedObjects:
                        this.lblCheckOutType.Text = "FetchRelatedObjects";
                        goto Label_00FA;
                }
                if (progress != esriReplicaProgress.esriRPRegisteringCheckOut)
                {
                    goto Label_00AE;
                }
                this.lblCheckOutType.Text = "注册检出";
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

        private void method_9(int int_3)
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

