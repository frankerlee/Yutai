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

        private IField _field;
        public FrmFieldProperties(IField field)
        {
            InitializeComponent();
            InitControls();
            _field = field;
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

        private void FrmFieldProperties_Load(object sender, EventArgs e)
        {
            txtName.Text = _field.Name;
            txtAlias.Text = _field.AliasName;
            cboDataType.SelectedItem = _field.Type.ToString();
            udWidth.Value = _field.Length;
            udPrecision.Value = _field.Precision;

            chkVisible.Enabled = false;
            cboDataType.Enabled = false;
            udWidth.Enabled = false;
            udPrecision.Enabled = false;
            txtExpression.Enabled = false;

        }
    }
}
