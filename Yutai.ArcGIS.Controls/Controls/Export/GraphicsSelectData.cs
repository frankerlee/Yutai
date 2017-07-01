using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal partial class GraphicsSelectData : UserControl
    {
        private IMap m_pMap = null;

        public GraphicsSelectData()
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
                MessageBox.Show("请图层!");
                return false;
            }
            ILayer layer = (this.cboLayers.SelectedItem as LayerObjectWrap).Layer;
            if (this.radioGroupExportType.SelectedIndex == 1)
            {
                GraphicHelper.pGraphicHelper.DataSource = (layer as IFeatureSelection).SelectionSet;
            }
            else
            {
                GraphicHelper.pGraphicHelper.DataSource = (layer as IFeatureLayer).FeatureClass;
            }
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