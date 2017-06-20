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
    public class frmUpdateAttribute : Form
    {
        private SimpleButton btnOK;
        private SimpleButton btnQueryDialog;
        private ComboBoxEdit cboFields;
        private ComboBoxEdit cboLayer;
        private CheckEdit chkUseSelected;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private IBasicMap m_pMap = null;
        private MemoEdit memoEdit;
        private SimpleButton simpleButton2;
        private TextEdit txtNewValue;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmUpdateAttribute_Load(object sender, EventArgs e)
        {
            MapHelper.AddEditLayerToList(this.m_pMap, this.cboLayer.Properties.Items);
            if (this.cboLayer.Properties.Items.Count > 0)
            {
                this.cboLayer.SelectedIndex = 0;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateAttribute));
            this.label1 = new Label();
            this.label2 = new Label();
            this.cboLayer = new ComboBoxEdit();
            this.cboFields = new ComboBoxEdit();
            this.label3 = new Label();
            this.txtNewValue = new TextEdit();
            this.chkUseSelected = new CheckEdit();
            this.label4 = new Label();
            this.btnQueryDialog = new SimpleButton();
            this.memoEdit = new MemoEdit();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.cboLayer.Properties.BeginInit();
            this.cboFields.Properties.BeginInit();
            this.txtNewValue.Properties.BeginInit();
            this.chkUseSelected.Properties.BeginInit();
            this.memoEdit.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "字段:";
            this.cboLayer.EditValue = "";
            this.cboLayer.Location = new Point(0x38, 8);
            this.cboLayer.Name = "cboLayer";
            this.cboLayer.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLayer.Size = new Size(0xa8, 0x15);
            this.cboLayer.TabIndex = 2;
            this.cboLayer.SelectedIndexChanged += new EventHandler(this.cboLayer_SelectedIndexChanged);
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(0x38, 40);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(0xa8, 0x15);
            this.cboFields.TabIndex = 3;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 80);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "新值:";
            this.txtNewValue.EditValue = "";
            this.txtNewValue.Location = new Point(0x38, 0x48);
            this.txtNewValue.Name = "txtNewValue";
            this.txtNewValue.Size = new Size(0xa8, 0x15);
            this.txtNewValue.TabIndex = 5;
            this.chkUseSelected.Location = new Point(8, 0x68);
            this.chkUseSelected.Name = "chkUseSelected";
            this.chkUseSelected.Properties.Caption = "使用选择集";
            this.chkUseSelected.Size = new Size(0x60, 0x13);
            this.chkUseSelected.TabIndex = 6;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x88);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x9b, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "仅更新满足以下条件的要素:";
            this.btnQueryDialog.Enabled = false;
            this.btnQueryDialog.Location = new Point(0x10, 0x138);
            this.btnQueryDialog.Name = "btnQueryDialog";
            this.btnQueryDialog.Size = new Size(0x58, 0x18);
            this.btnQueryDialog.TabIndex = 9;
            this.btnQueryDialog.Text = "查询生成器";
            this.btnQueryDialog.Click += new EventHandler(this.btnQueryDialog_Click);
            this.memoEdit.EditValue = "";
            this.memoEdit.Location = new Point(8, 160);
            this.memoEdit.Name = "memoEdit";
            this.memoEdit.Size = new Size(0x120, 0x88);
            this.memoEdit.TabIndex = 8;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(0xa8, 0x138);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new Point(0xe8, 0x138);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x38, 0x18);
            this.simpleButton2.TabIndex = 11;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x138, 0x15d);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnQueryDialog);
            base.Controls.Add(this.memoEdit);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.chkUseSelected);
            base.Controls.Add(this.txtNewValue);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.cboFields);
            base.Controls.Add(this.cboLayer);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            
            base.Name = "frmUpdateAttribute";
            this.Text = "更新属性值";
            base.Load += new EventHandler(this.frmUpdateAttribute_Load);
            this.cboLayer.Properties.EndInit();
            this.cboFields.Properties.EndInit();
            this.txtNewValue.Properties.EndInit();
            this.chkUseSelected.Properties.EndInit();
            this.memoEdit.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.m_pMap = value;
            }
        }

        internal class FieldWrap
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

