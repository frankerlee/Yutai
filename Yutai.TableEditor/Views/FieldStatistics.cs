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
using Yutai.Plugins.TableEditor.Editor;

namespace Yutai.Plugins.TableEditor.Views
{
    public partial class FieldStatistics : Form
    {
        private ITableView _view;
        public FieldStatistics(ITableView view, string fieldName)
        {
            InitializeComponent();
            _view = view;
            InitControls();
            cboField.SelectedItem = fieldName;
        }

        private void InitControls()
        {
            cboField.Items.Clear();
            IFields pFields = _view.FeatureLayer.FeatureClass.Fields;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                IField pField = pFields.Field[i];
                if (pField.Type == esriFieldType.esriFieldTypeDouble ||
                    pField.Type == esriFieldType.esriFieldTypeInteger ||
                    pField.Type == esriFieldType.esriFieldTypeSingle ||
                    pField.Type == esriFieldType.esriFieldTypeSmallInteger)
                {
                    cboField.Items.Add(pField.Name);
                }
            }
            _fieldStatsGrid.FeatureLayer = _view.FeatureLayer;
        }

        private void cboField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboField.SelectedItem == null)
                return;
            string fieldName = cboField.SelectedItem.ToString();
            
            _fieldStatsGrid.FieldName = fieldName;
            _fieldStatsGrid.Statistics();
            _fieldStatsGrid.ShowResult();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_fieldStatsGrid.Result);
        }
    }
}
