using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Helpers;
using WorkspaceHelper = Yutai.Pipeline.Editor.Helper.WorkspaceHelper;

namespace Yutai.Pipeline.Editor.Forms.Profession
{
    public partial class FrmAngleConvert : Form
    {
        private IFeatureClass _featureClass;
        private IAppContext _appContext;
        public FrmAngleConvert(IAppContext appContext)
        {
            InitializeComponent();
            _appContext = appContext;
            this.ucSelectFeatureClass1.Map = _appContext.FocusMap;
        }
        
        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (cmbField.SelectedItem == null)
                return;
            Yutai.ArcGIS.Common.Editor.Editor.StartEditOperation();
            try
            {
                int idx = _featureClass.FindField(cmbField.SelectedItem.ToString());
                IFeatureCursor pFeatureCursor = _featureClass.Search(null, false);
                IFeature pFeature;
                while ((pFeature = pFeatureCursor.NextFeature()) != null)
                {
                    double value = 0, toValue = 0;
                    object objValue = pFeature.Value[idx];
                    if (objValue is DBNull)
                        continue;
                    if (objValue == null)
                        continue;
                    if (double.TryParse(objValue.ToString(), out value))
                    {
                        if (radioButtonDegreeToRadian.Checked)
                        {
                            toValue = ConvertToRadian(value);
                        }
                        else
                        {
                            toValue = ConvertToDegree(value);
                        }
                    }
                    pFeature.Value[idx] = toValue;
                    pFeature.Store();
                }
                Marshal.ReleaseComObject(pFeatureCursor);
                MessageBox.Show(@"转换完成！");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                Yutai.ArcGIS.Common.Editor.Editor.StartEditOperation();
            }
        }

        private void txtDegree_TextChanged(object sender, EventArgs e)
        {
            if (txtDegree.Focused)
            {
                double degree;
                if (double.TryParse(txtDegree.Text.Trim(), out degree))
                {
                    txtRadian.Text = string.Format("{0}", ConvertToRadian(degree));
                }
            }
        }

        private void txtRadian_TextChanged(object sender, EventArgs e)
        {
            if (txtRadian.Focused)
            {
                double radian;
                if (double.TryParse(txtRadian.Text.Trim(), out radian))
                {
                    txtDegree.Text = string.Format("{0}", ConvertToDegree(radian));
                }
            }
        }

        private double ConvertToRadian(double degree)
        {
            return degree * Math.PI / 180;
        }

        private double ConvertToDegree(double radian)
        {
            return radian * 180 / Math.PI;
        }

        private void ucSelectFeatureClass1_SelectComplateEvent()
        {
            IFeatureClass featureClass = ucSelectFeatureClass1.SelectFeatureClass;
            if (featureClass == null)
                return;
            ComboBoxHelper.AddItemsFromFields(featureClass.Fields, cmbField);
        }
    }
}
