using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal partial class ExportToExcelSelectData : UserControl
    {
        private IMap m_pMap = null;

        public ExportToExcelSelectData()
        {
            this.InitializeComponent();
        }

        private void cboLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboLayers.SelectedItem != null)
            {
                if (((this.cboLayers.SelectedItem as LayerObjectWrap).Layer as IFeatureSelection).SelectionSet.Count > 0)
                {
                    this.radioGroupExportType.Enabled = true;
                }
                else
                {
                    this.radioGroupExportType.SelectedIndex = 0;
                    this.radioGroupExportType.Enabled = false;
                }
            }
        }

        public bool Do()
        {
            if (this.cboLayers.SelectedItem == null)
            {
                MessageBox.Show("请选择要导出的图层!");
                return false;
            }
            ICursor cursor = null;
            ILayer layer = (this.cboLayers.SelectedItem as LayerObjectWrap).Layer;
            if (this.radioGroupExportType.SelectedIndex == 1)
            {
                (layer as IFeatureSelection).SelectionSet.Search(null, false, out cursor);
            }
            else
            {
                cursor = (layer as IFeatureLayer).FeatureClass.Search(null, false) as ICursor;
            }
            ExportToExcelHelper.ExcelHelper.Cursor = cursor;
            return true;
        }

        private void ExportToExcelSelectData_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.m_pMap.LayerCount; i++)
            {
                ILayer pLayer = this.m_pMap.get_Layer(i);
                if ((pLayer is IFeatureLayer) && ((pLayer as IFeatureLayer).FeatureClass != null))
                {
                    this.cboLayers.Properties.Items.Add(new LayerObjectWrap(pLayer));
                }
            }
            if (this.cboLayers.Properties.Items.Count > 0)
            {
                this.cboLayers.SelectedIndex = 0;
            }
        }

        public IMap Map
        {
            get { return this.m_pMap; }
            set { this.m_pMap = value; }
        }

        protected partial class LayerObjectWrap
        {
            private ILayer m_pLayer = null;

            public LayerObjectWrap(ILayer pLayer)
            {
                this.m_pLayer = pLayer;
            }

            public override string ToString()
            {
                if (this.m_pLayer != null)
                {
                    return this.m_pLayer.Name;
                }
                return "";
            }

            public ILayer Layer
            {
                get { return this.m_pLayer; }
            }
        }
    }
}