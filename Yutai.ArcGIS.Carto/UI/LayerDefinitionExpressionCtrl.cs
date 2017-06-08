using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    public class LayerDefinitionExpressionCtrl : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private SimpleButton btnQueryDialog;
        private Container container_0 = null;
        private ITableDefinition itableDefinition_0 = null;
        private Label label1;
        private MemoEdit memoEdit;

        public LayerDefinitionExpressionCtrl()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                if (this.itableDefinition_0 != null)
                {
                    if (this.itableDefinition_0 is ITable)
                    {
                        IQueryFilter queryFilter = new QueryFilterClass {
                            WhereClause = this.memoEdit.Text
                        };
                        try
                        {
                            ComReleaser.ReleaseCOMObject((this.itableDefinition_0 as ITable).Search(queryFilter, false));
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("过滤条件输入错误，请检查!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return false;
                        }
                    }
                    this.itableDefinition_0.DefinitionExpression = this.memoEdit.Text;
                }
            }
            return true;
        }

        private void btnQueryDialog_Click(object sender, EventArgs e)
        {
            if (this.itableDefinition_0 != null)
            {
                frmAttributeQueryBuilder builder = new frmAttributeQueryBuilder {
                    Table = this.itableDefinition_0 as ITable,
                    WhereCaluse = this.memoEdit.Text
                };
                if (builder.ShowDialog() == DialogResult.OK)
                {
                    this.memoEdit.Text = builder.WhereCaluse;
                    this.bool_0 = true;
                }
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.memoEdit = new MemoEdit();
            this.btnQueryDialog = new SimpleButton();
            this.memoEdit.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "定义查询";
            this.memoEdit.EditValue = "";
            this.memoEdit.Location = new Point(0x10, 0x20);
            this.memoEdit.Name = "memoEdit";
            this.memoEdit.Size = new Size(0x120, 0x88);
            this.memoEdit.TabIndex = 1;
            this.memoEdit.EditValueChanged += new EventHandler(this.memoEdit_EditValueChanged);
            this.btnQueryDialog.Location = new Point(0x18, 0xb8);
            this.btnQueryDialog.Name = "btnQueryDialog";
            this.btnQueryDialog.Size = new Size(0x58, 0x18);
            this.btnQueryDialog.TabIndex = 2;
            this.btnQueryDialog.Text = "查询生成器";
            this.btnQueryDialog.Click += new EventHandler(this.btnQueryDialog_Click);
            base.Controls.Add(this.btnQueryDialog);
            base.Controls.Add(this.memoEdit);
            base.Controls.Add(this.label1);
            base.Name = "LayerDefinitionExpressionCtrl";
            base.Size = new Size(0x148, 0xe8);
            base.Load += new EventHandler(this.LayerDefinitionExpressionCtrl_Load);
            this.memoEdit.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void LayerDefinitionExpressionCtrl_Load(object sender, EventArgs e)
        {
            if (this.itableDefinition_0 != null)
            {
                this.memoEdit.Text = this.itableDefinition_0.DefinitionExpression;
            }
        }

        private void memoEdit_EditValueChanged(object sender, EventArgs e)
        {
            this.bool_0 = true;
        }

        public IFeatureLayerDefinition FeatureLayerDefinition
        {
            set
            {
                this.itableDefinition_0 = value as ITableDefinition;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        public object SelectItem
        {
            set
            {
                this.itableDefinition_0 = value as ITableDefinition;
            }
        }
    }
}

