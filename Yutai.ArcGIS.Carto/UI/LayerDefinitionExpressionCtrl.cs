using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Query.UI;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class LayerDefinitionExpressionCtrl : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private ITableDefinition itableDefinition_0 = null;

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

