using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmNewRelationClass : Form
    {
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private Container container_0 = null;
        private int int_0 = 0;
        private NewRelationClass_LabelAndNotification newRelationClass_LabelAndNotification_0;
        private NewRelationClass_RelationType newRelationClass_RelationType_0;
        private NewRelationClass_SetCardinality newRelationClass_SetCardinality_0;
        private NewRelationClass_SetKey newRelationClass_SetKey_0;
        private NewRelationClassSetClass newRelationClassSetClass_0;
        private ObjectFieldsPage objectFieldsPage_0;
        private Panel panel1;
        private Panel panel2;
        private SimpleButton simpleButton3;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewRelationClass));
            this.panel1 = new Panel();
            this.btnNext = new SimpleButton();
            this.btnLast = new SimpleButton();
            this.simpleButton3 = new SimpleButton();
            this.panel2 = new Panel();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 460);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x17a, 40);
            this.panel1.TabIndex = 0;
            this.btnNext.Location = new Point(0xf8, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x38, 0x18);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new Point(0xb8, 8);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x38, 0x18);
            this.btnLast.TabIndex = 3;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.simpleButton3.Location = new Point(0x138, 8);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(0x38, 0x18);
            this.simpleButton3.TabIndex = 5;
            this.simpleButton3.Text = "取消";
            this.simpleButton3.Click += new EventHandler(this.simpleButton3_Click);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x17a, 460);
            this.panel2.TabIndex = 1;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x17a, 500);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNewRelationClass";
            this.Text = "新建关系类";
            base.Load += new EventHandler(this.frmNewRelationClass_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
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

