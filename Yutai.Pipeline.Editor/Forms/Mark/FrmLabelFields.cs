using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Forms.Mark
{
    public partial class FrmLabelFields : Form
    {
        private  List<string> _Fields = new List<string>();
        private string _rtbVBscriptText ;
        private bool _SimpleChecked;

 
        public FrmLabelFields(ILayer layer)
        {
            InitializeComponent();
            LoadAllFields(layer);
            LoadUpField(layer);
            LoadDownField(layer);
            LoadLeftField(layer);
            LoadRightField(layer);
            rdbSinglelabel.Checked = true;
        }
        public List<string> Fields
        {
            get { return _Fields; }
            set { _Fields = value; }
        }

        public string rtbVBscriptText
        {
            get { return _rtbVBscriptText; }
            set { _rtbVBscriptText = value; }
        }
        public bool  SimpleChecked
        {
            get { return _SimpleChecked; }
            set { _SimpleChecked = value; }
        }
        private DataTable LoadDataTable(ILayer layer)
        {
            DataTable pFieldTable = new DataTable();
            ITable table = layer as ITable;
            pFieldTable.Columns.Add(new DataColumn("FieldName", typeof(string)));
            pFieldTable.Columns.Add(new DataColumn("FieldAlias", typeof(string)));

            for (int i = 0; i < table.Fields.FieldCount; i++)
            {

                IField pField = table.Fields.get_Field(i);
                if (pField.Type == esriFieldType.esriFieldTypeBlob || pField.Type == esriFieldType.esriFieldTypeGeometry)
                    continue;
                if (pField.Type == esriFieldType.esriFieldTypeRaster || pField.Type == esriFieldType.esriFieldTypeXML)
                    continue;
                DataRow pRow = pFieldTable.NewRow();
                pRow[0] = pField.Name;
                pRow[1] = pField.AliasName;
                pFieldTable.Rows.Add(pRow);
            }
            return pFieldTable;
        }

        private void LoadUpField(ILayer layer)
        {
            DataTable FieldTable = LoadDataTable(layer);
            DataRow Row = FieldTable.NewRow();
            FieldTable.Rows.InsertAt(Row, 0);
            cobUp.DataSource = FieldTable;
            cobUp.DisplayMember = "FieldAlias";
            cobUp.ValueMember = "FieldName";
            cobUp.SelectedIndex = 5;
           
        }

        private void LoadDownField(ILayer layer)
        {
            DataTable FieldTable = LoadDataTable(layer);
            DataRow Row = FieldTable.NewRow();
            FieldTable.Rows.InsertAt(Row, 0);
            cobDown.DataSource = FieldTable;
            cobDown.DisplayMember = "FieldAlias";
            cobDown.ValueMember = "FieldName";
            cobDown.SelectedIndex = 6;
        }
        private void LoadLeftField(ILayer layer)
        {
            DataTable FieldTable = LoadDataTable(layer);
            DataRow Row = FieldTable.NewRow();
            FieldTable.Rows.InsertAt(Row, 0);
            cobLeft.DataSource = FieldTable;
            cobLeft.DisplayMember = "FieldAlias";
            cobLeft.ValueMember = "FieldName";
        }
        private void LoadRightField(ILayer layer)
        {
            DataTable FieldTable = LoadDataTable(layer);
            DataRow Row = FieldTable.NewRow();
            FieldTable.Rows.InsertAt( Row ,0);
            cobRight.DataSource = FieldTable;
            cobRight.DisplayMember = "FieldAlias";
            cobRight.ValueMember = "FieldName";
        }
        private void LoadAllFields(ILayer layer)
        {
            DataTable pFieldTable = LoadDataTable(layer);
            lsbAllFields.DataSource = pFieldTable;
            lsbAllFields.DisplayMember = "FieldAlias";
            lsbAllFields.ValueMember = "FieldName";      
        }

       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lsbAllFields.Items.Count <= 0) return;
           
            if (lsbSelectedFields.Items.Count == 0)
            {
                lsbSelectedFields.Items.Add(lsbAllFields.SelectedItem);
            }
            else
            {
                bool b = true;
                for (int j = 0; j < lsbSelectedFields.Items.Count; j++)
                {

                    if (lsbSelectedFields.Items[j] == lsbAllFields.SelectedItem)
                    {
                        b = false;
                    }
                }
                if (b)
                {
                    lsbSelectedFields.Items.Add(lsbAllFields.SelectedItem);
                }
            }
                
            lsbSelectedFields.DisplayMember = "FieldAlias";
            lsbSelectedFields.ValueMember = "FieldName";
            
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int selectindex = lsbSelectedFields.SelectedIndex;
            object upItem = lsbSelectedFields.Items[selectindex - 1];
            object downItem = lsbSelectedFields.SelectedItem;
            lsbSelectedFields.Items[selectindex - 1] = downItem;
            lsbSelectedFields.Items[selectindex] = upItem;

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int selectindex = lsbSelectedFields.SelectedIndex;
            object upItem =  lsbSelectedFields.SelectedItem;
            object downItem = lsbSelectedFields.Items[selectindex + 1];
            lsbSelectedFields.Items[selectindex + 1] = upItem;
            lsbSelectedFields.Items[selectindex] = downItem;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsbSelectedFields.SelectedItems.Count <= 0) return;
            lsbSelectedFields.Items.Remove(lsbSelectedFields.SelectedItem);
            //for (int i = lsbSelectedFields.SelectedItems.Count - 1; i >= 0; i--)
            //{
            //    lsbSelectedFields.Items.Remove(lsbSelectedFields.SelectedItems[i]);
            //}
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _rtbVBscriptText = "";
            if (rdbSinglelabel.Checked)
            {
                _Fields = new List<string>();
                if (lsbSelectedFields.Items.Count==0)
                {
                    MessageBox.Show("未选择注记字段，请重新设置！");
                    return;
                }
                foreach (var selectedItem in lsbSelectedFields.Items)
                {
                    _Fields.Add(((System.Data.DataRowView) (selectedItem)).Row.ItemArray[0].ToString());
                }
                _SimpleChecked = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                if (cobUp.SelectedIndex <= 0 && cobLeft.SelectedIndex <= 0 && cobRight.SelectedIndex <= 0 && cobDown.SelectedIndex <= 0)
                {
                    MessageBox.Show("未选择注记字段，请重新设置！");
                    return;
                }

                if (cobUp.SelectedIndex == 0 || cobUp.SelectedIndex == -1)
                {
                    _rtbVBscriptText = "\"" + cobUp.Text.ToString() + "\"" ;
                }

                else
                {
                    _rtbVBscriptText = "[" + cobUp.SelectedValue + "]" ;
                }


                if (cobLeft.SelectedIndex == 0 || cobLeft.SelectedIndex == -1)
                {
                    _rtbVBscriptText = _rtbVBscriptText + "&vbnewline&" + "\"" + cobLeft.Text.ToString() + labDelimiter.Text + "\"";
                }
                else
                {
                    _rtbVBscriptText = _rtbVBscriptText + "&vbnewline&" + "[" + cobLeft.SelectedValue + "]" + "&" +
                                          "\"" + labDelimiter.Text + "\"" ;
                }


                if (cobRight.SelectedIndex == 0 || cobRight.SelectedIndex == -1)
                {
                    _rtbVBscriptText = _rtbVBscriptText + "&" + "\"" + cobRight.Text.ToString() + "\"";
                }
                else
                {
                    _rtbVBscriptText = _rtbVBscriptText + "&" + "[" + cobRight.SelectedValue + "]";
                }


                if (cobDown.SelectedIndex == 0 || cobDown.SelectedIndex == -1)
                {
                    _rtbVBscriptText = _rtbVBscriptText + "&vbnewline&" + "\"" + cobRight.Text.ToString() + "\"";
                }
                else
                {
                    _rtbVBscriptText = _rtbVBscriptText + "&vbnewline&" + "[" +
                                           cobDown.SelectedValue + "]";
                }
                _SimpleChecked = false;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
           
        }
        
        private void rdbSinglelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSinglelabel.Checked)
            {
                grbSimgleLabel.Enabled = true;
                grbMultilinelabel.Enabled = false;
            }
            if (!rdbSinglelabel.Checked)
            {
                grbSimgleLabel.Enabled = false ;
                grbMultilinelabel.Enabled = true ;
            }

        }

        private void rdbMultilinelabel_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMultilinelabel .Checked)
            {
                grbSimgleLabel.Enabled = false;
                grbMultilinelabel.Enabled = true;
            }
            if (!rdbMultilinelabel.Checked)
            {
                grbSimgleLabel.Enabled = true ;
                grbMultilinelabel.Enabled = false ;
            }
        }





    }
}
