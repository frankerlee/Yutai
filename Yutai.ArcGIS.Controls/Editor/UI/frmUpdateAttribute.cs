using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.ArcGIS.Common.Query.UI;
using Yutai.ArcGIS.Common.Wrapper;
using Yutai.Shared;
using ProcessAssist = Yutai.Shared.ProcessAssist;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class frmUpdateAttribute : Form
    {
        private IBasicMap m_pMap = null;

        public frmUpdateAttribute()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cboFields.SelectedIndex != -1)
            {
                IField field = (this.cboFields.SelectedItem as FieldWrap).Field;
                if ((((field.Type == esriFieldType.esriFieldTypeDouble) || (field.Type == esriFieldType.esriFieldTypeInteger)) || (field.Type == esriFieldType.esriFieldTypeSmallInteger)) || (field.Type == esriFieldType.esriFieldTypeSingle))
                {
                    try
                    {
                        double num = double.Parse(this.txtNewValue.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请输入数字类型数据!");
                        return;
                    }
                }
                if (this.cboLayer.SelectedIndex >= 0)
                {
                    Exception exception;
                    IFeatureLayer layer = (this.cboLayer.SelectedItem as LayerObject).Layer as IFeatureLayer;
                    IQueryFilter queryFilter = null;
                    if (this.memoEdit.Text.Trim().Length > 0)
                    {
                        queryFilter = new QueryFilterClass {
                            WhereClause = this.memoEdit.Text.Trim()
                        };
                    }
                    ICursor cursor = null;
                    int count = 0;
                    try
                    {
                        if (this.chkUseSelected.Enabled && this.chkUseSelected.Checked)
                        {
                            count = (layer as IFeatureSelection).SelectionSet.Count;
                            (layer as IFeatureSelection).SelectionSet.Search(queryFilter, false, out cursor);
                        }
                        else
                        {
                            count = layer.FeatureClass.FeatureCount(queryFilter);
                            cursor = layer.FeatureClass.Search(queryFilter, false) as ICursor;
                        }
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        MessageBox.Show("查询条件输入错误，请重新输入!");
                        return;
                    }
                    if (count > 0)
                    {
                        ProcessAssist assist = new ProcessAssist(this);
                        assist.InitProgress();
                        assist.SetMaxValue(count);
                        assist.SetMessage("开始更新属性.....");
                        assist.SetPostion(0);
                        assist.Start();
                        int num3 = 1;
                        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                        string name = field.Name;
                        int index = layer.FeatureClass.Fields.FindField(name);
                        IWorkspaceEdit workspace = (layer.FeatureClass as IDataset).Workspace as IWorkspaceEdit;
                        workspace.StartEditOperation();
                        for (IRow row = cursor.NextRow(); row != null; row = cursor.NextRow())
                        {
                            assist.SetMessage(string.Format("正在更新第{0}个对象,共{1}个对象", num3++, count));
                            assist.Increment(1);
                            try
                            {
                                row.set_Value(index, this.txtNewValue.Text);
                                row.Store();
                            }
                            catch (Exception exception2)
                            {
                                exception = exception2;
                               Logger.Current.Error("", exception, "");
                            }
                        }
                        workspace.StopEditOperation();
                        System.Windows.Forms.Cursor.Current = Cursors.Default;
                        ComReleaser.ReleaseCOMObject(cursor);
                        assist.End();
                    }
                    base.DialogResult = DialogResult.OK;
                }
            }
        }

        private void btnQueryDialog_Click(object sender, EventArgs e)
        {
            if (this.cboLayer.SelectedIndex >= 0)
            {
                this.btnQueryDialog.Enabled = true;
                IFeatureLayer layer = (this.cboLayer.SelectedItem as LayerObject).Layer as IFeatureLayer;
                frmAttributeQueryBuilder builder = new frmAttributeQueryBuilder {
                    CurrentLayer = layer,
                    WhereCaluse = this.memoEdit.Text
                };
                if (builder.ShowDialog() == DialogResult.OK)
                {
                    this.memoEdit.Text = builder.WhereCaluse;
                }
            }
        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboFields.SelectedIndex >= 0)
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }

        private void cboLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.memoEdit.Text = "";
            this.cboFields.Properties.Items.Clear();
            if (this.cboLayer.SelectedIndex >= 0)
            {
                this.btnQueryDialog.Enabled = true;
                IFeatureLayer layer = (this.cboLayer.SelectedItem as LayerObject).Layer as IFeatureLayer;
                if ((layer as IFeatureSelection).SelectionSet.Count > 0)
                {
                    this.chkUseSelected.Enabled = true;
                    this.chkUseSelected.Checked = true;
                }
                else
                {
                    this.chkUseSelected.Checked = false;
                    this.chkUseSelected.Enabled = false;
                    this.chkUseSelected.Checked = false;
                }
                IFields fields = layer.FeatureClass.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField pField = fields.get_Field(i);
                    if ((((pField.Type != esriFieldType.esriFieldTypeOID) && (pField.Type != esriFieldType.esriFieldTypeGeometry)) && ((pField.Type != esriFieldType.esriFieldTypeRaster) && (pField.Type != esriFieldType.esriFieldTypeBlob))) && pField.Editable)
                    {
                        this.cboFields.Properties.Items.Add(new FieldWrap(pField));
                    }
                }
                if (this.cboFields.Properties.Items.Count > 0)
                {
                    this.cboFields.SelectedIndex = 0;
                }
            }
            else
            {
                this.btnQueryDialog.Enabled = false;
                this.chkUseSelected.Checked = false;
                this.chkUseSelected.Enabled = false;
            }
        }

 private void frmUpdateAttribute_Load(object sender, EventArgs e)
        {
            MapHelper.AddEditLayerToList(this.m_pMap, this.cboLayer.Properties.Items);
            if (this.cboLayer.Properties.Items.Count > 0)
            {
                this.cboLayer.SelectedIndex = 0;
            }
        }

 public IBasicMap FocusMap
        {
            set
            {
                this.m_pMap = value;
            }
        }

        internal partial class FieldWrap
        {
            private IField m_pField = null;

            public FieldWrap(IField pField)
            {
                this.m_pField = pField;
            }

            public override string ToString()
            {
                if (this.m_pField != null)
                {
                    return this.m_pField.AliasName;
                }
                return "";
            }

            public IField Field
            {
                get
                {
                    return this.m_pField;
                }
            }
        }
    }
}

