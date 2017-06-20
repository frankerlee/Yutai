using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal class frmTable : Form
    {
        private SimpleButton btnEnd;
        private SimpleButton btnFirstRecord;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private SimpleButton btnSaveEditing;
        private SimpleButton btnStartEditing;
        private SimpleButton btnStopEditing;
        private IContainer components = null;
        private DataGrid dataGrid1;
        private bool m_CanDo = false;
        private bool m_InEditing = false;
        private int m_MaxRecord = 0x7d0;
        private ICursor m_pCursor = null;
        private DataTable m_pDataTable = new DataTable();
        private ITable m_pTable = null;
        private int m_RecordNum = 0;
        private string m_strGeometry = "";
        private Panel panel1;
        private TextEdit txtIndex;

        public frmTable()
        {
            this.InitializeComponent();
            this.dataGrid1.SetDataBinding(this.m_pDataTable, "");
            this.dataGrid1.ReadOnly = true;
            this.m_pDataTable.ColumnChanged += new DataColumnChangeEventHandler(this.m_pDataTable_ColumnChanged);
        }

        private void AddFilesInfo(IFields pFields)
        {
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                IField field = pFields.get_Field(i);
                DataColumn column = new DataColumn(field.AliasName) {
                    Caption = field.Name
                };
                if (field.Type == esriFieldType.esriFieldTypeBlob)
                {
                    column.ReadOnly = true;
                }
                else if (field.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    column.ReadOnly = true;
                }
                else
                {
                    column.ReadOnly = !field.Editable;
                }
                this.m_pDataTable.Columns.Add(column);
            }
        }

        private void AddRecord(bool bAll)
        {
            int num = 0;
            if (bAll)
            {
                num = this.m_RecordNum - this.m_pDataTable.Rows.Count;
            }
            else
            {
                num = ((this.m_RecordNum - this.m_pDataTable.Rows.Count) > this.m_MaxRecord) ? this.m_MaxRecord : this.m_RecordNum;
            }
            IFields fields = this.m_pCursor.Fields;
            int num2 = 0;
            IRow row = this.m_pCursor.NextRow();
            object[] values = new object[fields.FieldCount];
            while (row != null)
            {
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if (field.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        values[i] = this.m_strGeometry;
                    }
                    else if (field.Type == esriFieldType.esriFieldTypeBlob)
                    {
                        values[i] = "二进制数据";
                    }
                    else
                    {
                        values[i] = row.get_Value(i);
                    }
                }
                this.m_pDataTable.Rows.Add(values);
                num2++;
                if (num2 > num)
                {
                    break;
                }
                row = this.m_pCursor.NextRow();
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (this.m_RecordNum > this.m_pDataTable.Rows.Count)
            {
                this.AddRecord(true);
            }
            this.dataGrid1.CurrentRowIndex = this.m_pDataTable.Rows.Count - 1;
            this.txtIndex.Text = (this.dataGrid1.CurrentRowIndex + 1).ToString();
            this.btnLast.Enabled = true;
            this.btnFirstRecord.Enabled = true;
            this.btnEnd.Enabled = false;
            this.btnNext.Enabled = false;
        }

        private void btnFirstRecord_Click(object sender, EventArgs e)
        {
            this.dataGrid1.CurrentRowIndex = 0;
            this.txtIndex.Text = (this.dataGrid1.CurrentRowIndex + 1).ToString();
            this.btnLast.Enabled = false;
            this.btnFirstRecord.Enabled = false;
            this.btnNext.Enabled = true;
            this.btnEnd.Enabled = true;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            this.dataGrid1.CurrentRowIndex--;
            this.txtIndex.Text = (this.dataGrid1.CurrentRowIndex + 1).ToString();
            if (this.dataGrid1.CurrentRowIndex == 0)
            {
                this.btnLast.Enabled = false;
                this.btnFirstRecord.Enabled = false;
            }
            this.btnNext.Enabled = true;
            this.btnEnd.Enabled = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.dataGrid1.CurrentRowIndex++;
            this.txtIndex.Text = (this.dataGrid1.CurrentRowIndex + 1).ToString();
            this.btnLast.Enabled = true;
            this.btnFirstRecord.Enabled = true;
            if (this.dataGrid1.CurrentRowIndex == (this.m_pDataTable.Rows.Count - 1))
            {
                this.btnNext.Enabled = false;
                this.btnEnd.Enabled = false;
            }
        }

        private void btnSaveEditing_Click(object sender, EventArgs e)
        {
            Yutai.ArcGIS.Common.Editor.Editor.StopEditing(this.m_pTable as IDataset, true, false);
            Yutai.ArcGIS.Common.Editor.Editor.StartEditing(this.m_pTable as IDataset);
            this.btnSaveEditing.Enabled = false;
        }

        private void btnStartEditing_Click(object sender, EventArgs e)
        {
            SysGrants grants = new SysGrants();
            bool flag = false;
            if ((this.m_pTable as IDataset).Workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
            {
                if (AppConfigInfo.UserID.Length == 0)
                {
                    flag = true;
                }
                else
                {
                    flag = grants.GetStaffAndRolesLayerPri(AppConfigInfo.UserID, 2, (this.m_pTable as IDataset).Name);
                }
            }
            else
            {
                flag = true;
            }
            if (flag)
            {
                ((this.m_pTable as IDataset).Workspace as IWorkspaceEdit).StartEditing(true);
                this.dataGrid1.ReadOnly = false;
                this.btnStartEditing.Enabled = false;
                this.btnSaveEditing.Enabled = false;
                this.m_InEditing = true;
                this.btnStopEditing.Enabled = true;
            }
        }

        private void btnStopEditing_Click(object sender, EventArgs e)
        {
            if (Yutai.ArcGIS.Common.Editor.Editor.StopEditing(this.m_pTable as IDataset, true, true))
            {
                this.dataGrid1.ReadOnly = true;
                this.btnStartEditing.Enabled = true;
                this.m_InEditing = false;
                this.btnSaveEditing.Enabled = false;
                this.btnStopEditing.Enabled = false;
            }
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.m_pDataTable.Rows.Count != 1)
            {
                this.txtIndex.Text = (this.dataGrid1.CurrentRowIndex + 1).ToString();
                if (this.dataGrid1.CurrentRowIndex == (this.m_pDataTable.Rows.Count - 1))
                {
                    this.btnLast.Enabled = true;
                    this.btnFirstRecord.Enabled = true;
                    this.btnEnd.Enabled = false;
                    this.btnNext.Enabled = false;
                }
                else if (this.dataGrid1.CurrentRowIndex == 0)
                {
                    this.btnLast.Enabled = false;
                    this.btnFirstRecord.Enabled = false;
                    this.btnEnd.Enabled = true;
                    this.btnNext.Enabled = true;
                }
                else
                {
                    this.btnLast.Enabled = true;
                    this.btnFirstRecord.Enabled = true;
                    this.btnEnd.Enabled = true;
                    this.btnNext.Enabled = true;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            if (this.m_pCursor != null)
            {
                ComReleaser.ReleaseCOMObject(this.m_pCursor);
            }
            base.Dispose(disposing);
        }

        private void frmTable_Load(object sender, EventArgs e)
        {
            this.Text = "属性表";
            this.Init();
            this.m_CanDo = true;
        }

        private string GetShapeString(IFeatureClass pFeatClass)
        {
            if (pFeatClass == null)
            {
                return "";
            }
            string str = "";
            switch (pFeatClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    str = "点";
                    break;

                case esriGeometryType.esriGeometryMultipoint:
                    str = "多点";
                    break;

                case esriGeometryType.esriGeometryPolyline:
                    str = "线";
                    break;

                case esriGeometryType.esriGeometryPolygon:
                    str = "多边形";
                    break;

                case esriGeometryType.esriGeometryMultiPatch:
                    str = "多面";
                    break;
            }
            int index = pFeatClass.Fields.FindField(pFeatClass.ShapeFieldName);
            IGeometryDef geometryDef = pFeatClass.Fields.get_Field(index).GeometryDef;
            str = str + " ";
            if (geometryDef.HasZ)
            {
                str = str + "Z";
            }
            if (geometryDef.HasM)
            {
                str = str + "M";
            }
            return str;
        }

        public void Init()
        {
            this.m_pDataTable.Rows.Clear();
            this.m_pDataTable.Columns.Clear();
            if (this.m_pTable != null)
            {
                this.m_strGeometry = this.GetShapeString(this.m_pTable as IFeatureClass);
                IQueryFilter queryFilter = null;
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                this.m_RecordNum = this.m_pTable.RowCount(queryFilter);
                this.m_pCursor = this.m_pTable.Search(queryFilter, false);
                this.AddFilesInfo(this.m_pTable.Fields);
                this.AddRecord(false);
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
            if ((this.m_pDataTable.Rows.Count == 0) || (this.m_pDataTable.Rows.Count == 1))
            {
                this.btnFirstRecord.Enabled = false;
                this.btnLast.Enabled = false;
                this.btnEnd.Enabled = false;
                this.btnNext.Enabled = false;
                this.txtIndex.Enabled = false;
            }
            else
            {
                this.btnFirstRecord.Enabled = false;
                this.btnLast.Enabled = false;
                this.btnEnd.Enabled = true;
                this.btnNext.Enabled = true;
                this.txtIndex.Enabled = true;
            }
            this.txtIndex.Text = (this.dataGrid1.CurrentRowIndex + 1).ToString();
            if (this.m_InEditing)
            {
                this.btnSaveEditing.Enabled = true;
                this.btnStopEditing.Enabled = true;
                this.btnStartEditing.Enabled = false;
                this.dataGrid1.ReadOnly = false;
            }
            else
            {
                this.btnSaveEditing.Enabled = false;
                this.btnStopEditing.Enabled = false;
                this.btnStartEditing.Enabled = true;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTable));
            this.dataGrid1 = new DataGrid();
            this.panel1 = new Panel();
            this.btnSaveEditing = new SimpleButton();
            this.btnStopEditing = new SimpleButton();
            this.btnStartEditing = new SimpleButton();
            this.btnEnd = new SimpleButton();
            this.btnNext = new SimpleButton();
            this.txtIndex = new TextEdit();
            this.btnLast = new SimpleButton();
            this.btnFirstRecord = new SimpleButton();
            this.dataGrid1.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.dataGrid1.BorderStyle = BorderStyle.None;
            this.dataGrid1.CaptionVisible = false;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.Dock = DockStyle.Fill;
            this.dataGrid1.HeaderForeColor = SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new Size(0x1c0, 0xf9);
            this.dataGrid1.TabIndex = 4;
            this.dataGrid1.CurrentCellChanged += new EventHandler(this.dataGrid1_CurrentCellChanged);
            this.panel1.Controls.Add(this.btnSaveEditing);
            this.panel1.Controls.Add(this.btnStopEditing);
            this.panel1.Controls.Add(this.btnStartEditing);
            this.panel1.Controls.Add(this.btnEnd);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.txtIndex);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Controls.Add(this.btnFirstRecord);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0xf9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1c0, 0x18);
            this.panel1.TabIndex = 3;
            this.btnSaveEditing.Enabled = false;
            this.btnSaveEditing.Location = new System.Drawing.Point(0x158, 2);
            this.btnSaveEditing.Name = "btnSaveEditing";
            this.btnSaveEditing.Size = new Size(0x40, 0x15);
            this.btnSaveEditing.TabIndex = 10;
            this.btnSaveEditing.Text = "保存编辑";
            this.btnSaveEditing.Click += new EventHandler(this.btnSaveEditing_Click);
            this.btnStopEditing.Enabled = false;
            this.btnStopEditing.Location = new System.Drawing.Point(0x110, 2);
            this.btnStopEditing.Name = "btnStopEditing";
            this.btnStopEditing.Size = new Size(0x40, 0x15);
            this.btnStopEditing.TabIndex = 9;
            this.btnStopEditing.Text = "结束编辑";
            this.btnStopEditing.Click += new EventHandler(this.btnStopEditing_Click);
            this.btnStartEditing.Location = new System.Drawing.Point(200, 1);
            this.btnStartEditing.Name = "btnStartEditing";
            this.btnStartEditing.Size = new Size(0x40, 0x15);
            this.btnStartEditing.TabIndex = 8;
            this.btnStartEditing.Text = "开始编辑";
            this.btnStartEditing.Click += new EventHandler(this.btnStartEditing_Click);
            this.btnEnd.Image = (Image) resources.GetObject("btnEnd.Image");
            this.btnEnd.Location = new System.Drawing.Point(160, 1);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new Size(0x18, 0x15);
            this.btnEnd.TabIndex = 4;
            this.btnEnd.Click += new EventHandler(this.btnEnd_Click);
            this.btnNext.Image = (Image) resources.GetObject("btnNext.Image");
            this.btnNext.Location = new System.Drawing.Point(0x88, 1);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x18, 0x15);
            this.btnNext.TabIndex = 3;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.txtIndex.EditValue = "1";
            this.txtIndex.Location = new System.Drawing.Point(0x38, 1);
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.Size = new Size(80, 0x15);
            this.txtIndex.TabIndex = 2;
            this.txtIndex.KeyUp += new KeyEventHandler(this.txtIndex_KeyUp);
            this.btnLast.Image = (Image) resources.GetObject("btnLast.Image");
            this.btnLast.Location = new System.Drawing.Point(0x20, 1);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x18, 0x15);
            this.btnLast.TabIndex = 1;
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnFirstRecord.Image = (Image) resources.GetObject("btnFirstRecord.Image");
            this.btnFirstRecord.Location = new System.Drawing.Point(8, 1);
            this.btnFirstRecord.Name = "btnFirstRecord";
            this.btnFirstRecord.Size = new Size(0x18, 0x15);
            this.btnFirstRecord.TabIndex = 0;
            this.btnFirstRecord.Click += new EventHandler(this.btnFirstRecord_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1c0, 0x111);
            base.Controls.Add(this.dataGrid1);
            base.Controls.Add(this.panel1);
            
            base.Name = "frmTable";
            base.Load += new EventHandler(this.frmTable_Load);
            this.dataGrid1.EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void m_pDataTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (this.m_InEditing && this.m_CanDo)
            {
                try
                {
                    IRow row;
                    int num;
                    object[] itemArray = e.Row.ItemArray;
                    IWorkspaceEdit workspace = (this.m_pTable as IDataset).Workspace as IWorkspaceEdit;
                    if (itemArray[0] is DBNull)
                    {
                        if (!(this.m_pTable is IFeatureClass))
                        {
                            workspace.StartEditOperation();
                            row = this.m_pTable.CreateRow();
                            num = row.Fields.FindFieldByAliasName(e.Column.ColumnName);
                            if (num != -1)
                            {
                                row.set_Value(num, e.ProposedValue);
                                row.Store();
                            }
                            workspace.StopEditOperation();
                            this.m_CanDo = false;
                            e.Row[this.m_pTable.OIDFieldName] = row.OID;
                            this.m_CanDo = true;
                        }
                    }
                    else
                    {
                        int num2 = Convert.ToInt32(itemArray[0]);
                        IQueryFilter queryFilter = new QueryFilterClass {
                            WhereClause = this.m_pTable.OIDFieldName + " = " + num2.ToString()
                        };
                        ICursor o = this.m_pTable.Search(queryFilter, false);
                        row = o.NextRow();
                        if (row != null)
                        {
                            workspace.StartEditOperation();
                            num = row.Fields.FindFieldByAliasName(e.Column.ColumnName);
                            if (num != -1)
                            {
                                row.set_Value(num, e.ProposedValue);
                            }
                            row.Store();
                            workspace.StopEditOperation();
                        }
                        ComReleaser.ReleaseCOMObject(o);
                    }
                    this.btnSaveEditing.Enabled = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void txtIndex_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int num = Convert.ToInt32(this.txtIndex.Text);
                    if (num <= 0)
                    {
                        num = 1;
                    }
                    if (num >= this.m_pDataTable.Rows.Count)
                    {
                        num = this.m_pDataTable.Rows.Count - 1;
                    }
                    this.txtIndex.Text = num.ToString();
                    this.dataGrid1.CurrentRowIndex = num - 1;
                    if (this.dataGrid1.CurrentRowIndex == (this.m_pDataTable.Rows.Count - 1))
                    {
                        this.btnLast.Enabled = true;
                        this.btnFirstRecord.Enabled = true;
                        this.btnEnd.Enabled = false;
                        this.btnNext.Enabled = false;
                    }
                    else if (this.dataGrid1.CurrentRowIndex == 0)
                    {
                        this.btnLast.Enabled = false;
                        this.btnFirstRecord.Enabled = false;
                        this.btnEnd.Enabled = true;
                        this.btnNext.Enabled = true;
                    }
                    else
                    {
                        this.btnLast.Enabled = true;
                        this.btnFirstRecord.Enabled = true;
                        this.btnEnd.Enabled = true;
                        this.btnNext.Enabled = true;
                    }
                }
                catch
                {
                    MessageBox.Show("输入数据格式错误!");
                }
            }
        }

        public ITable Table
        {
            set
            {
                this.m_pTable = value;
                this.m_InEditing = ((this.m_pTable as IDataset).Workspace as IWorkspaceEdit).IsBeingEdited();
                if (this.m_CanDo)
                {
                    this.Init();
                }
            }
        }
    }
}

