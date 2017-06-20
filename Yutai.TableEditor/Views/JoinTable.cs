using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.TableEditor.Controls;

namespace Yutai.Plugins.TableEditor.Views
{
    public partial class JoinTable : Form
    {
        private IFeatureClass _featureClass;
        private JoinModel _model;

        public JoinTable()
        {
            InitializeComponent();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
                selectFields1.SelectAll();
            else
                selectFields1.SelectClear();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            _featureClass = SelectFeatureClassDialog();
            txtDatasource.Text = _featureClass.AliasName;
            cboCurrent.Fields = _featureClass.Fields;
            cboExternal.Fields = _featureClass.Fields;
            selectFields1.Fields = _featureClass.Fields;
        }
        public static IFeatureClass SelectFeatureClassDialog()
        {
            IGxDialog pGxDialog = new GxDialogClass();
            pGxDialog.ObjectFilter = new GxFilterFeatureClassesClass();
            pGxDialog.AllowMultiSelect = false;
            pGxDialog.RememberLocation = true;
            IEnumGxObject pEnumGxObject;
            if (pGxDialog.DoModalOpen(0, out pEnumGxObject))
            {
                IGxObject pSelectGxObject = pEnumGxObject.Next();
                IGxDataset pGxDataset = (IGxDataset)pSelectGxObject;
                return pGxDataset.Dataset as IFeatureClass;
            }
            return null;
        }

        public JoinModel Model
        {
            get { return _model; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _model = null;
            if (_featureClass == null)
                return;
            if (cboCurrent.Field == null)
                return;
            if (cboExternal.Field == null)
                return;
            string fields = selectFields1.GetSelectedFields(',');
            if (string.IsNullOrWhiteSpace(fields))
                return;
            if (fields.Contains(cboExternal.Field.Name) == false)
                fields += $",{cboExternal.Field.Name}";
            _model = new JoinModel()
            {
                Table = _featureClass as ITable,
                Name = _featureClass.AliasName,
                FromField = cboCurrent.Field.Name,
                ToField = cboExternal.Field.Name,
                Fields = fields
            };
        }
    }
}
