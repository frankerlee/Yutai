using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmNewGN : Form
    {
        private BulidGeometryNetwork_SelectFeatureClass bulidGeometryNetwork_SelectFeatureClass_0 = new BulidGeometryNetwork_SelectFeatureClass();
        private BulidGeometryNetworkType bulidGeometryNetworkType_0 = new BulidGeometryNetworkType();
        private BulidGN_Configuration bulidGN_Configuration_0 = new BulidGN_Configuration();
        private BulidGN_EnableFieldSet bulidGN_EnableFieldSet_0 = new BulidGN_EnableFieldSet();
        private BulidGN_IsContaincomplexEdge bulidGN_IsContaincomplexEdge_0 = new BulidGN_IsContaincomplexEdge();
        private BulidGN_SnapSet bulidGN_SnapSet_0 = new BulidGN_SnapSet();
        private BulidGN_SourceSkin bulidGN_SourceSkin_0 = new BulidGN_SourceSkin();
        private BulidGN_Summary bulidGN_Summary_0 = new BulidGN_Summary();
        private BulidGN_WeightAssociation bulidGN_WeightAssociation_0 = new BulidGN_WeightAssociation();
        private BulidGN_Weights bulidGN_Weights_0 = new BulidGN_Weights();
        private Container container_0 = null;
        private int int_0 = 0;

        public frmNewGN()
        {
            this.InitializeComponent();
            BulidGeometryNetworkHelper.Init();
            this.bulidGeometryNetworkType_0.Dock = DockStyle.Fill;
            this.bulidGeometryNetworkType_0.Visible = true;
            this.panel2.Controls.Add(this.bulidGeometryNetworkType_0);
            this.bulidGeometryNetwork_SelectFeatureClass_0.Dock = DockStyle.Fill;
            this.bulidGeometryNetwork_SelectFeatureClass_0.Visible = false;
            this.panel2.Controls.Add(this.bulidGeometryNetwork_SelectFeatureClass_0);
            this.bulidGN_IsContaincomplexEdge_0.Dock = DockStyle.Fill;
            this.bulidGN_IsContaincomplexEdge_0.Visible = false;
            this.panel2.Controls.Add(this.bulidGN_IsContaincomplexEdge_0);
            this.bulidGN_SnapSet_0.Dock = DockStyle.Fill;
            this.bulidGN_SnapSet_0.Visible = false;
            this.panel2.Controls.Add(this.bulidGN_SnapSet_0);
            this.bulidGN_Weights_0.Dock = DockStyle.Fill;
            this.bulidGN_Weights_0.Visible = false;
            this.panel2.Controls.Add(this.bulidGN_Weights_0);
            this.bulidGN_SourceSkin_0.Dock = DockStyle.Fill;
            this.bulidGN_SourceSkin_0.Visible = false;
            this.panel2.Controls.Add(this.bulidGN_SourceSkin_0);
            this.bulidGN_Configuration_0.Dock = DockStyle.Fill;
            this.bulidGN_Configuration_0.Visible = false;
            this.panel2.Controls.Add(this.bulidGN_Configuration_0);
            this.bulidGN_EnableFieldSet_0.Dock = DockStyle.Fill;
            this.bulidGN_EnableFieldSet_0.Visible = false;
            this.panel2.Controls.Add(this.bulidGN_EnableFieldSet_0);
            this.bulidGN_Summary_0.Dock = DockStyle.Fill;
            this.bulidGN_Summary_0.Visible = false;
            this.panel2.Controls.Add(this.bulidGN_Summary_0);
            this.bulidGN_WeightAssociation_0.Dock = DockStyle.Fill;
            this.bulidGN_WeightAssociation_0.Visible = false;
            this.panel2.Controls.Add(this.bulidGN_WeightAssociation_0);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    return;

                case 1:
                    this.bulidGeometryNetworkType_0.Visible = true;
                    this.bulidGeometryNetwork_SelectFeatureClass_0.Visible = false;
                    this.btnLast.Enabled = false;
                    break;

                case 2:
                    this.bulidGeometryNetwork_SelectFeatureClass_0.Visible = true;
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.IsEmpty)
                    {
                        if (BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                        {
                            this.bulidGN_EnableFieldSet_0.Visible = false;
                        }
                        else if (BulidGeometryNetworkHelper.BulidGNHelper.HasLineFeatureClass())
                        {
                            this.bulidGN_IsContaincomplexEdge_0.Visible = false;
                        }
                        else
                        {
                            this.bulidGN_SnapSet_0.Visible = false;
                        }
                        break;
                    }
                    this.bulidGN_Summary_0.Visible = false;
                    this.btnNext.Text = "下一步";
                    break;

                case 3:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                    {
                        if (BulidGeometryNetworkHelper.BulidGNHelper.HasLineFeatureClass())
                        {
                            this.bulidGN_IsContaincomplexEdge_0.Visible = true;
                            this.bulidGN_SnapSet_0.Visible = false;
                        }
                        else
                        {
                            this.bulidGN_SnapSet_0.Visible = true;
                            this.bulidGN_SourceSkin_0.Visible = false;
                        }
                        break;
                    }
                    this.bulidGN_EnableFieldSet_0.Visible = true;
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasLineFeatureClass())
                    {
                        this.bulidGN_SnapSet_0.Visible = false;
                        break;
                    }
                    this.bulidGN_IsContaincomplexEdge_0.Visible = false;
                    break;

                case 4:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                    {
                        if (BulidGeometryNetworkHelper.BulidGNHelper.HasLineFeatureClass())
                        {
                            this.bulidGN_SnapSet_0.Visible = true;
                            if (BulidGeometryNetworkHelper.BulidGNHelper.HasPointFeatureClass())
                            {
                                this.bulidGN_SourceSkin_0.Visible = false;
                            }
                            else
                            {
                                this.bulidGN_Weights_0.Visible = false;
                                if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                                {
                                    this.btnNext.Text = "下一步";
                                }
                            }
                        }
                        else
                        {
                            this.bulidGN_SourceSkin_0.Visible = true;
                            this.bulidGN_Weights_0.Visible = false;
                            if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                            {
                                this.btnNext.Text = "下一步";
                            }
                        }
                        break;
                    }
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasLineFeatureClass())
                    {
                        this.bulidGN_SnapSet_0.Visible = true;
                        this.bulidGN_SourceSkin_0.Visible = false;
                        break;
                    }
                    this.bulidGN_IsContaincomplexEdge_0.Visible = true;
                    this.bulidGN_SnapSet_0.Visible = false;
                    break;

                case 5:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                    {
                        if (this.bulidGN_Weights_0.Visible)
                        {
                            this.bulidGN_SourceSkin_0.Visible = true;
                            this.bulidGN_Weights_0.Visible = false;
                            if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                            {
                                this.btnNext.Text = "下一步";
                            }
                        }
                        else
                        {
                            this.btnNext.Text = "下一步";
                            this.bulidGN_Summary_0.Visible = false;
                            this.bulidGN_Weights_0.Visible = true;
                        }
                        break;
                    }
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasLineFeatureClass())
                    {
                        this.bulidGN_SourceSkin_0.Visible = true;
                        this.bulidGN_Weights_0.Visible = false;
                        if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                        {
                            this.btnNext.Text = "下一步";
                        }
                        break;
                    }
                    this.bulidGN_SnapSet_0.Visible = true;
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasPointFeatureClass())
                    {
                        this.bulidGN_Weights_0.Visible = false;
                        if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                        {
                            this.btnNext.Text = "下一步";
                        }
                        break;
                    }
                    this.bulidGN_SourceSkin_0.Visible = false;
                    break;

                case 6:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                    {
                        if (this.bulidGN_WeightAssociation_0.Visible)
                        {
                            this.bulidGN_WeightAssociation_0.Visible = false;
                            this.bulidGN_Weights_0.Visible = true;
                        }
                        else if (this.bulidGN_Configuration_0.Visible)
                        {
                            this.bulidGN_Configuration_0.Visible = false;
                            if (BulidGeometryNetworkHelper.BulidGNHelper.Weights.Count > 0)
                            {
                                this.bulidGN_WeightAssociation_0.Visible = true;
                            }
                            else
                            {
                                this.bulidGN_Weights_0.Visible = true;
                            }
                        }
                        else if (this.bulidGN_Summary_0.Visible)
                        {
                            this.bulidGN_Summary_0.Visible = false;
                            this.btnNext.Text = "下一步";
                            if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                            {
                                if (BulidGeometryNetworkHelper.BulidGNHelper.Weights.Count == 0)
                                {
                                    this.bulidGN_Weights_0.Visible = true;
                                }
                                else
                                {
                                    this.bulidGN_WeightAssociation_0.Visible = true;
                                }
                            }
                            else
                            {
                                this.bulidGN_Configuration_0.Visible = true;
                            }
                        }
                        break;
                    }
                    if (!this.bulidGN_Weights_0.Visible)
                    {
                        if (this.bulidGN_WeightAssociation_0.Visible)
                        {
                            this.bulidGN_Weights_0.Visible = true;
                            this.bulidGN_WeightAssociation_0.Visible = false;
                        }
                        else if (this.bulidGN_Summary_0.Visible)
                        {
                            this.btnNext.Text = "下一步";
                            this.bulidGN_Summary_0.Visible = false;
                            this.bulidGN_Weights_0.Visible = true;
                        }
                        else
                        {
                            this.bulidGN_Configuration_0.Visible = false;
                            this.bulidGN_Weights_0.Visible = true;
                        }
                        break;
                    }
                    this.bulidGN_SourceSkin_0.Visible = true;
                    this.bulidGN_Weights_0.Visible = false;
                    break;

                case 7:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                    {
                        if (BulidGeometryNetworkHelper.BulidGNHelper.Weights.Count > 0)
                        {
                            this.bulidGN_WeightAssociation_0.Visible = true;
                            if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                            {
                                this.bulidGN_Summary_0.Visible = false;
                                this.btnNext.Text = "下一步";
                            }
                            else
                            {
                                this.bulidGN_Configuration_0.Visible = false;
                            }
                        }
                        else if (this.bulidGN_Summary_0.Visible)
                        {
                            this.bulidGN_Configuration_0.Visible = true;
                            this.bulidGN_Summary_0.Visible = false;
                            this.btnNext.Text = "下一步";
                        }
                        break;
                    }
                    if (!this.bulidGN_WeightAssociation_0.Visible)
                    {
                        if (this.bulidGN_Summary_0.Visible)
                        {
                            if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                            {
                                if (BulidGeometryNetworkHelper.BulidGNHelper.Weights.Count > 0)
                                {
                                    this.bulidGN_WeightAssociation_0.Visible = true;
                                }
                                else
                                {
                                    this.bulidGN_Weights_0.Visible = true;
                                }
                            }
                            else
                            {
                                this.bulidGN_Configuration_0.Visible = true;
                            }
                            this.bulidGN_Summary_0.Visible = false;
                            this.btnNext.Text = "下一步";
                        }
                        else if ((BulidGeometryNetworkHelper.BulidGNHelper.Weights.Count > 0) && this.bulidGN_Configuration_0.Visible)
                        {
                            this.bulidGN_WeightAssociation_0.Visible = true;
                            this.bulidGN_Configuration_0.Visible = false;
                        }
                        break;
                    }
                    this.bulidGN_Weights_0.Visible = true;
                    this.bulidGN_WeightAssociation_0.Visible = false;
                    break;

                case 8:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                    {
                        if (this.bulidGN_Configuration_0.Visible)
                        {
                            this.bulidGN_Configuration_0.Visible = true;
                            this.bulidGN_Summary_0.Visible = false;
                            this.btnNext.Text = "下一步";
                        }
                        break;
                    }
                    if (BulidGeometryNetworkHelper.BulidGNHelper.Weights.Count <= 0)
                    {
                        if (this.bulidGN_Summary_0.Visible)
                        {
                            this.bulidGN_Configuration_0.Visible = true;
                            this.bulidGN_Summary_0.Visible = false;
                            this.btnNext.Text = "下一步";
                        }
                        break;
                    }
                    this.bulidGN_WeightAssociation_0.Visible = true;
                    if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type != esriWorkspaceType.esriLocalDatabaseWorkspace)
                    {
                        this.bulidGN_Configuration_0.Visible = false;
                        break;
                    }
                    this.bulidGN_Summary_0.Visible = false;
                    this.btnNext.Text = "下一步";
                    break;

                case 9:
                    this.bulidGN_Configuration_0.Visible = true;
                    this.bulidGN_Summary_0.Visible = false;
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
                    this.bulidGeometryNetworkType_0.Visible = false;
                    this.bulidGeometryNetwork_SelectFeatureClass_0.Visible = true;
                    this.btnLast.Enabled = true;
                    break;

                case 1:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.IsEmpty)
                    {
                        if (BulidGeometryNetworkHelper.BulidGNHelper.GetSelectFefatureClassCount() == 0)
                        {
                            MessageBox.Show("需要选择构建网络的要素!");
                            return;
                        }
                        this.bulidGeometryNetwork_SelectFeatureClass_0.Visible = false;
                        if (BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                        {
                            this.bulidGN_EnableFieldSet_0.Visible = true;
                        }
                        else if (BulidGeometryNetworkHelper.BulidGNHelper.HasLineFeatureClass())
                        {
                            this.bulidGN_IsContaincomplexEdge_0.Visible = true;
                        }
                        else
                        {
                            this.bulidGN_SnapSet_0.Visible = true;
                        }
                        break;
                    }
                    this.bulidGeometryNetwork_SelectFeatureClass_0.Visible = false;
                    this.bulidGN_Summary_0.Visible = true;
                    this.btnNext.Text = "完成";
                    break;

                case 2:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                    {
                        if (BulidGeometryNetworkHelper.BulidGNHelper.HasLineFeatureClass())
                        {
                            this.bulidGN_IsContaincomplexEdge_0.Visible = false;
                            this.bulidGN_SnapSet_0.Visible = true;
                        }
                        else
                        {
                            this.bulidGN_SnapSet_0.Visible = false;
                            this.bulidGN_SourceSkin_0.Visible = true;
                        }
                        break;
                    }
                    this.bulidGN_EnableFieldSet_0.Visible = false;
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasLineFeatureClass())
                    {
                        this.bulidGN_SnapSet_0.Visible = true;
                        break;
                    }
                    this.bulidGN_IsContaincomplexEdge_0.Visible = true;
                    break;

                case 3:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                    {
                        if (BulidGeometryNetworkHelper.BulidGNHelper.HasLineFeatureClass())
                        {
                            this.bulidGN_SnapSet_0.Visible = false;
                            if (BulidGeometryNetworkHelper.BulidGNHelper.HasPointFeatureClass())
                            {
                                this.bulidGN_SourceSkin_0.Visible = true;
                            }
                            else
                            {
                                this.bulidGN_Weights_0.Visible = true;
                            }
                        }
                        else
                        {
                            this.bulidGN_SourceSkin_0.Visible = false;
                            this.bulidGN_Weights_0.Visible = true;
                        }
                        break;
                    }
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasLineFeatureClass())
                    {
                        this.bulidGN_SnapSet_0.Visible = false;
                        this.bulidGN_SourceSkin_0.Visible = true;
                        break;
                    }
                    this.bulidGN_IsContaincomplexEdge_0.Visible = false;
                    this.bulidGN_SnapSet_0.Visible = true;
                    break;

                case 4:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                    {
                        if (this.bulidGN_SourceSkin_0.Visible)
                        {
                            this.bulidGN_SourceSkin_0.Visible = false;
                            this.bulidGN_Weights_0.Visible = true;
                        }
                        else
                        {
                            this.bulidGN_Weights_0.Visible = false;
                            if (BulidGeometryNetworkHelper.BulidGNHelper.Weights.Count > 0)
                            {
                                this.bulidGN_WeightAssociation_0.Visible = true;
                            }
                            else if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                            {
                                this.bulidGN_Summary_0.Visible = true;
                                this.btnNext.Text = "完成";
                            }
                            else
                            {
                                this.bulidGN_Configuration_0.Visible = true;
                            }
                        }
                        break;
                    }
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasLineFeatureClass())
                    {
                        this.bulidGN_SourceSkin_0.Visible = false;
                        this.bulidGN_Weights_0.Visible = true;
                        break;
                    }
                    this.bulidGN_SnapSet_0.Visible = false;
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasPointFeatureClass())
                    {
                        this.bulidGN_Weights_0.Visible = true;
                        break;
                    }
                    this.bulidGN_SourceSkin_0.Visible = true;
                    break;

                case 5:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                    {
                        if (this.bulidGN_Weights_0.Visible)
                        {
                            this.bulidGN_Weights_0.Visible = false;
                            if (BulidGeometryNetworkHelper.BulidGNHelper.Weights.Count > 0)
                            {
                                this.bulidGN_WeightAssociation_0.Visible = true;
                            }
                            else if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                            {
                                this.bulidGN_Summary_0.Visible = true;
                                this.btnNext.Text = "完成";
                            }
                            else
                            {
                                this.bulidGN_Configuration_0.Visible = true;
                            }
                            break;
                        }
                        if (this.bulidGN_Configuration_0.Visible)
                        {
                            this.bulidGN_Configuration_0.Visible = false;
                            this.bulidGN_Summary_0.Visible = true;
                            this.btnNext.Text = "完成";
                            break;
                        }
                        if (this.bulidGN_WeightAssociation_0.Visible)
                        {
                            this.bulidGN_WeightAssociation_0.Visible = false;
                            if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                            {
                                this.bulidGN_Summary_0.Visible = true;
                                this.btnNext.Text = "完成";
                            }
                            else
                            {
                                this.bulidGN_Configuration_0.Visible = true;
                            }
                            break;
                        }
                        if (BulidGeometryNetworkHelper.BulidGNHelper.CreateGeometricNetwork(BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset) != null)
                        {
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                        }
                        return;
                    }
                    if (!this.bulidGN_SourceSkin_0.Visible)
                    {
                        this.bulidGN_Weights_0.Visible = false;
                        if (BulidGeometryNetworkHelper.BulidGNHelper.Weights.Count > 0)
                        {
                            this.bulidGN_WeightAssociation_0.Visible = true;
                        }
                        else if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                        {
                            this.bulidGN_Summary_0.Visible = true;
                            this.btnNext.Text = "完成";
                        }
                        else
                        {
                            this.bulidGN_Configuration_0.Visible = true;
                        }
                        break;
                    }
                    this.bulidGN_SourceSkin_0.Visible = false;
                    this.bulidGN_Weights_0.Visible = true;
                    break;

                case 6:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                    {
                        if (this.bulidGN_WeightAssociation_0.Visible)
                        {
                            this.bulidGN_WeightAssociation_0.Visible = false;
                            if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                            {
                                this.bulidGN_Summary_0.Visible = true;
                                this.btnNext.Text = "完成";
                            }
                            else
                            {
                                this.bulidGN_Configuration_0.Visible = true;
                            }
                            break;
                        }
                        if (this.bulidGN_Configuration_0.Visible)
                        {
                            this.bulidGN_Configuration_0.Visible = false;
                            this.bulidGN_Summary_0.Visible = true;
                            this.btnNext.Text = "完成";
                            break;
                        }
                        if (BulidGeometryNetworkHelper.BulidGNHelper.CreateGeometricNetwork(BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset) != null)
                        {
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                        }
                        return;
                    }
                    if (!this.bulidGN_Weights_0.Visible)
                    {
                        if (this.bulidGN_Configuration_0.Visible)
                        {
                            this.bulidGN_Configuration_0.Visible = false;
                            this.bulidGN_Summary_0.Visible = true;
                            this.btnNext.Text = "完成";
                            break;
                        }
                        if (this.bulidGN_WeightAssociation_0.Visible)
                        {
                            this.bulidGN_WeightAssociation_0.Visible = false;
                            if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                            {
                                this.bulidGN_Summary_0.Visible = true;
                                this.btnNext.Text = "完成";
                            }
                            else
                            {
                                this.bulidGN_Configuration_0.Visible = true;
                            }
                            break;
                        }
                        if (BulidGeometryNetworkHelper.BulidGNHelper.CreateGeometricNetwork(BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset) != null)
                        {
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                        }
                        return;
                    }
                    this.bulidGN_Weights_0.Visible = false;
                    if (BulidGeometryNetworkHelper.BulidGNHelper.Weights.Count <= 0)
                    {
                        if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                        {
                            this.bulidGN_Summary_0.Visible = true;
                            this.btnNext.Text = "完成";
                        }
                        else
                        {
                            this.bulidGN_Configuration_0.Visible = true;
                        }
                        break;
                    }
                    this.bulidGN_WeightAssociation_0.Visible = true;
                    break;

                case 7:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                    {
                        if (this.bulidGN_Configuration_0.Visible)
                        {
                            this.bulidGN_Configuration_0.Visible = false;
                            this.bulidGN_Summary_0.Visible = true;
                            this.btnNext.Text = "完成";
                        }
                        break;
                    }
                    if (!this.bulidGN_WeightAssociation_0.Visible)
                    {
                        if (this.bulidGN_Configuration_0.Visible)
                        {
                            this.bulidGN_Configuration_0.Visible = false;
                            this.bulidGN_Summary_0.Visible = true;
                            this.btnNext.Text = "完成";
                            break;
                        }
                        if (BulidGeometryNetworkHelper.BulidGNHelper.CreateGeometricNetwork(BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset) != null)
                        {
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                        }
                        return;
                    }
                    this.bulidGN_WeightAssociation_0.Visible = false;
                    if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace.Type != esriWorkspaceType.esriLocalDatabaseWorkspace)
                    {
                        this.bulidGN_Configuration_0.Visible = true;
                        break;
                    }
                    this.bulidGN_Summary_0.Visible = true;
                    this.btnNext.Text = "完成";
                    break;

                case 8:
                    if (!BulidGeometryNetworkHelper.BulidGNHelper.HasEnabeldField())
                    {
                        if (BulidGeometryNetworkHelper.BulidGNHelper.CreateGeometricNetwork(BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset) != null)
                        {
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                        }
                        return;
                    }
                    if (this.bulidGN_Configuration_0.Visible)
                    {
                        this.bulidGN_Configuration_0.Visible = false;
                        this.bulidGN_Summary_0.Visible = true;
                        this.btnNext.Text = "完成";
                    }
                    break;

                case 9:
                    if (BulidGeometryNetworkHelper.BulidGNHelper.CreateGeometricNetwork(BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset) != null)
                    {
                        base.DialogResult = DialogResult.OK;
                        base.Close();
                    }
                    return;
            }
            this.int_0++;
        }

 public IFeatureDataset FeatureDataset
        {
            set
            {
                BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset = value;
            }
        }
    }
}

