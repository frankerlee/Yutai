using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal partial class GraphicsSet : UserControl
    {

        public GraphicsSet()
        {
            this.InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.SelectedIndices.Count - 1; i >= 0; i--)
            {
                this.listView1.Items.RemoveAt(this.listView1.SelectedIndices[i]);
            }
        }

        private void btnSelectField_Click(object sender, EventArgs e)
        {
            if (GraphicHelper.pGraphicHelper.DataSource != null)
            {
                IFields fields;
                if (GraphicHelper.pGraphicHelper.DataSource is ISelectionSet)
                {
                    fields = (GraphicHelper.pGraphicHelper.DataSource as ISelectionSet).Target.Fields;
                }
                else
                {
                    fields = (GraphicHelper.pGraphicHelper.DataSource as ITable).Fields;
                }
                int index = fields.FindFieldByAliasName(this.cboFields.Text);
                IField field = fields.get_Field(index);
                ListViewItem item = new ListViewItem(new string[] { field.Name, field.AliasName });
                this.listView1.Items.Add(item);
            }
        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboFields.SelectedIndex != -1)
            {
                this.btnSelectField.Enabled = true;
            }
            else
            {
                this.btnSelectField.Enabled = false;
            }
        }

        private void cboHorField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboHorField.SelectedIndex != -1)
            {
                GraphicHelper.pGraphicHelper.HorFieldName = this.cboHorField.Text;
            }
        }

 public bool Do()
        {
            if (this.listView1.Items.Count == 0)
            {
                MessageBox.Show("请至少添加一个数字字段!");
                return false;
            }
            GraphicHelper.pGraphicHelper.FiledNames.Clear();
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                ListViewItem item = this.listView1.Items[i];
                GraphicHelper.pGraphicHelper.Add(item.SubItems[1].Text);
            }
            return GraphicHelper.pGraphicHelper.Show();
        }

        private void ExportToExcelSet_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        public void Init()
        {
            this.cboHorField.Properties.Items.Clear();
            this.cboFields.Properties.Items.Clear();
            this.listView1.Items.Clear();
            if (GraphicHelper.pGraphicHelper.Cursor != null)
            {
                IFields fields;
                if (GraphicHelper.pGraphicHelper.DataSource is ISelectionSet)
                {
                    fields = (GraphicHelper.pGraphicHelper.DataSource as ISelectionSet).Target.Fields;
                }
                else
                {
                    fields = (GraphicHelper.pGraphicHelper.DataSource as ITable).Fields;
                }
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    esriFieldType type = field.Type;
                    switch (type)
                    {
                        case esriFieldType.esriFieldTypeOID:
                        case esriFieldType.esriFieldTypeSmallInteger:
                        case esriFieldType.esriFieldTypeString:
                        case esriFieldType.esriFieldTypeInteger:
                        case esriFieldType.esriFieldTypeSingle:
                            this.cboHorField.Properties.Items.Add(field.AliasName);
                            break;
                    }
                    if ((((type == esriFieldType.esriFieldTypeDouble) || (type == esriFieldType.esriFieldTypeSmallInteger)) || (type == esriFieldType.esriFieldTypeInteger)) || (type == esriFieldType.esriFieldTypeSingle))
                    {
                        this.cboFields.Properties.Items.Add(field.AliasName);
                    }
                }
                if (this.cboHorField.Properties.Items.Count > 0)
                {
                    this.cboHorField.SelectedIndex = 0;
                }
                if (this.cboFields.Properties.Items.Count > 0)
                {
                    this.cboFields.SelectedIndex = 0;
                }
            }
        }

 private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
            }
        }

        private void txtTitle_EditValueChanged(object sender, EventArgs e)
        {
            GraphicHelper.pGraphicHelper.Title = this.txtTitle.Text;
        }
    }
}

