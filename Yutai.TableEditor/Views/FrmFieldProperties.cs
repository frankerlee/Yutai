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
using Yutai.UI.Helpers;
using FieldType = Yutai.ArcGIS.Common.Geodatabase.FieldType;

namespace Yutai.Plugins.TableEditor.Views
{
    public partial class FrmFieldProperties : Form, IFieldPropertiesView
    {
        public FrmFieldProperties()
        {
            InitializeComponent();
            InitControls();
        }

        public FrmFieldProperties(IField field)
        {
            InitializeComponent();
            InitControls();

            txtName.Text = field.Name;
            txtAlias.Text = field.AliasName;
            cboDataType.SelectedValue = field.Type;
            udWidth.Value = field.Length;
            udPrecision.Value = field.Precision;

            chkVisible.Enabled = false;
            cboDataType.Enabled = false;
            udWidth.Enabled = false;
            udPrecision.Enabled = false;
            txtExpression.Enabled = false;
        }

        private void InitControls()
        {
            cboDataType.DataSource = Enum.GetNames(typeof(esriFieldType));
        }



        public IField NewField
        {
            get
            {
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = pField as IFieldEdit;
                pFieldEdit.Name_2 = txtName.Text;
                pFieldEdit.AliasName_2 = txtAlias.Text;
                pFieldEdit.Type_2 = (esriFieldType)Enum.Parse(typeof(esriFieldType), cboDataType.SelectedItem.ToString(), false);
                pFieldEdit.Precision_2 = (int)udPrecision.Value;
                pFieldEdit.Length_2 = (int)udWidth.Value;

                return pFieldEdit as IField;
            }
        }
    }
}
