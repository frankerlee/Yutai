using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class AnnoReferenceScaleSetCtrl : UserControl
    {
        private Container container_0 = null;

        public AnnoReferenceScaleSetCtrl()
        {
            this.InitializeComponent();
        }

        private void AnnoReferenceScaleSetCtrl_Load(object sender, EventArgs e)
        {
        }

        public bool Do()
        {
            try
            {
                double num = double.Parse(this.txtScale.Text);
                if (num <= 0.0)
                {
                    MessageBox.Show("请输入大于0的数字!");
                    return false;
                }
                NewObjectClassHelper.m_pObjectClassHelper.m_RefScale = num;
                NewObjectClassHelper.m_pObjectClassHelper.m_Units = (esriUnits) this.cboMapUnit.SelectedIndex;
                return true;
            }
            catch
            {
                MessageBox.Show("请输入数字!");
                return false;
            }
        }
    }
}