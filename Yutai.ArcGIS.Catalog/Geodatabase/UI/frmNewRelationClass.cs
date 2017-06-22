using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmNewRelationClass : Form
    {
        private Container container_0 = null;
        private int int_0 = 0;

        public frmNewRelationClass()
        {
            this.InitializeComponent();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    return;

                case 1:
                    this.newRelationClassSetClass_0.Visible = true;
                    this.newRelationClass_RelationType_0.Visible = false;
                    this.btnLast.Enabled = false;
                    break;

                case 2:
                    this.newRelationClass_RelationType_0.Visible = true;
                    this.newRelationClass_LabelAndNotification_0.Visible = false;
                    break;

                case 3:
                    this.newRelationClass_LabelAndNotification_0.Visible = true;
                    this.newRelationClass_SetCardinality_0.Visible = false;
                    break;

                case 4:
                    if (!NewRelationClassHelper.IsAttributed)
                    {
                        this.newRelationClass_SetCardinality_0.Visible = true;
                        this.newRelationClass_SetKey_0.Visible = false;
                        this.btnNext.Text = "下一步";
                        break;
                    }
                    this.objectFieldsPage_0.Visible = false;
                    this.newRelationClass_SetCardinality_0.Visible = true;
                    break;

                case 5:
                    this.objectFieldsPage_0.Visible = true;
                    this.newRelationClass_SetKey_0.Visible = false;
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
                    if (!(NewRelationClassHelper.relClassName.Trim() == ""))
                    {
                        if (NewRelationClassHelper.OriginClass == null)
                        {
                            MessageBox.Show("请选择源对象类!");
                            return;
                        }
                        if (NewRelationClassHelper.DestinationClass == null)
                        {
                            MessageBox.Show("请选择目标对象类!");
                            return;
                        }
                        if (NewRelationClassHelper.OriginClass.ObjectClassID == -1)
                        {
                            MessageBox.Show("源对象类未注册为空间数据库中的对象类，无法用于创建关系!");
                            return;
                        }
                        if (NewRelationClassHelper.DestinationClass.ObjectClassID == -1)
                        {
                            MessageBox.Show("目标对象类未注册为空间数据库中的对象类，无法用于创建关系!");
                            return;
                        }
                        this.newRelationClassSetClass_0.Visible = false;
                        this.newRelationClass_RelationType_0.Visible = true;
                        this.btnLast.Enabled = true;
                        break;
                    }
                    MessageBox.Show("请输入关系名称!");
                    return;

                case 1:
                    this.newRelationClass_RelationType_0.Visible = false;
                    this.newRelationClass_LabelAndNotification_0.Visible = true;
                    break;

                case 2:
                    this.newRelationClass_LabelAndNotification_0.Visible = false;
                    this.newRelationClass_SetCardinality_0.Visible = true;
                    break;

                case 3:
                    if (!NewRelationClassHelper.IsAttributed)
                    {
                        this.newRelationClass_SetCardinality_0.Visible = false;
                        this.newRelationClass_SetKey_0.Visible = true;
                        this.btnNext.Text = "完成";
                        break;
                    }
                    this.objectFieldsPage_0.Fields = NewRelationClassHelper.relAttrFields;
                    this.objectFieldsPage_0.IsEdit = true;
                    this.objectFieldsPage_0.Workspace = NewRelationClassHelper.m_pWorkspace;
                    this.objectFieldsPage_0.UseType = enumUseType.enumUTObjectClass;
                    this.objectFieldsPage_0.InitControl();
                    this.newRelationClass_SetCardinality_0.Visible = false;
                    this.objectFieldsPage_0.Visible = true;
                    break;

                case 4:
                    if (!NewRelationClassHelper.IsAttributed)
                    {
                        if (NewRelationClassHelper.CreateRelation() != null)
                        {
                            base.DialogResult = DialogResult.OK;
                        }
                        return;
                    }
                    this.objectFieldsPage_0.Visible = false;
                    this.newRelationClass_SetKey_0.Visible = true;
                    this.btnNext.Text = "完成";
                    break;

                case 5:
                    if (NewRelationClassHelper.CreateRelation() != null)
                    {
                        base.DialogResult = DialogResult.OK;
                    }
                    return;
            }
            this.int_0++;
        }

 private void frmNewRelationClass_Load(object sender, EventArgs e)
        {
            this.newRelationClassSetClass_0 = new NewRelationClassSetClass();
            this.newRelationClassSetClass_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.newRelationClassSetClass_0);
            this.newRelationClass_RelationType_0 = new NewRelationClass_RelationType();
            this.newRelationClass_RelationType_0.Dock = DockStyle.Fill;
            this.newRelationClass_RelationType_0.Visible = false;
            this.panel2.Controls.Add(this.newRelationClass_RelationType_0);
            this.newRelationClass_LabelAndNotification_0 = new NewRelationClass_LabelAndNotification();
            this.newRelationClass_LabelAndNotification_0.Visible = false;
            this.newRelationClass_LabelAndNotification_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.newRelationClass_LabelAndNotification_0);
            this.objectFieldsPage_0 = new ObjectFieldsPage();
            this.objectFieldsPage_0.Visible = false;
            this.objectFieldsPage_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.objectFieldsPage_0);
            this.newRelationClass_SetCardinality_0 = new NewRelationClass_SetCardinality();
            this.newRelationClass_SetCardinality_0.Dock = DockStyle.Fill;
            this.newRelationClass_SetCardinality_0.Visible = false;
            this.panel2.Controls.Add(this.newRelationClass_SetCardinality_0);
            this.newRelationClass_SetKey_0 = new NewRelationClass_SetKey();
            this.newRelationClass_SetKey_0.Dock = DockStyle.Fill;
            this.newRelationClass_SetKey_0.Visible = false;
            this.panel2.Controls.Add(this.newRelationClass_SetKey_0);
        }

 private void simpleButton3_Click(object sender, EventArgs e)
        {
        }

        public IFeatureDataset FeatureDataset
        {
            set
            {
                NewRelationClassHelper.m_pFeatureDataset = value;
                NewRelationClassHelper.m_pWorkspace = NewRelationClassHelper.m_pFeatureDataset.Workspace;
            }
        }

        public IWorkspace Workspace
        {
            set
            {
                NewRelationClassHelper.m_pWorkspace = value;
                NewRelationClassHelper.m_pFeatureDataset = null;
            }
        }
    }
}

