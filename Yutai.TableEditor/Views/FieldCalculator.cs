using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.TableEditor.Functions;

namespace Yutai.Plugins.TableEditor.Views
{
    public partial class FieldCalculator : Form
    {
        private IFields _fields;

        public FieldCalculator(IFields fields, IField field)
        {
            InitializeComponent();
            _fields = fields;
            fieldListBox.Fields = _fields;
            functionTreeView.InitControl();
            lblField.Text = $@"字段名称 = {field.Name}";
            lblType.Text = Convert(field.Type);
        }

        private string Convert(esriFieldType type)
        {
            switch (type)
            {
                case esriFieldType.esriFieldTypeSmallInteger:
                    return "短整型";
                case esriFieldType.esriFieldTypeInteger:
                    return "整型";
                case esriFieldType.esriFieldTypeSingle:
                    return "浮点数";
                case esriFieldType.esriFieldTypeDouble:
                    return "浮点数";
                case esriFieldType.esriFieldTypeString:
                    return "字符串";
                case esriFieldType.esriFieldTypeDate:
                    return "日期";
                case esriFieldType.esriFieldTypeOID:
                    return "表内编号";
                case esriFieldType.esriFieldTypeGeometry:
                    return "图形对象";
                case esriFieldType.esriFieldTypeBlob:
                    return "二进制";
                case esriFieldType.esriFieldTypeRaster:
                    return "影像图";
                case esriFieldType.esriFieldTypeGUID:
                    return "永久唯一编号";
                case esriFieldType.esriFieldTypeGlobalID:
                    return "全局编号";
                case esriFieldType.esriFieldTypeXML:
                    return "XML";
            }
            return null;
        }

        public string Expression
        {
            get { return txtExpression.Text; } 
        }

        private void functionTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            IFunction pFunction = e.Node.Tag as IFunction;
            if (pFunction == null)
                return;
            txtExpression.Add(pFunction);
        }

        private void fieldListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.fieldListBox.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
                return;
            txtExpression.Add($"[{fieldListBox.Items[index]}]");
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            txtExpression.Add(" + ");
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            txtExpression.Add(" - ");
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            txtExpression.Add(" * ");
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            txtExpression.Add(" / ");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtExpression.Clear();
        }

        private void txtExpression_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtExpression.Text.Trim()))
            {
                lblValidation.Text = @"表达式为空";
            }
            else
            {
                lblValidation.Text = @"";
            }
        }
    }
}
