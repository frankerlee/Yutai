using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
    public partial class PoPointAlarmForm : Form
    {
        private partial class LayerItem
        {
            public IFeatureLayer featureLayer;

            public override string ToString()
            {
                return this.featureLayer.Name;
            }
        }

        private IContainer icontainer_0 = null;

        public IAppContext m_iApp;

        public IMapControl3 MapControl;

        public IPipelineConfig pPipeCfg;


        public PoPointAlarmForm(IAppContext context, IPipelineConfig config)
        {
            this.InitializeComponent();
            m_iApp = context;
            MapControl = m_iApp.MapControl as IMapControl3;
            pPipeCfg = config;
        }

        private void PoPointAlarmForm_Load(object obj, EventArgs eventArgs)
        {
            this.FillLayers();
        }

        private void FillLayers()
        {
            this.LayerBox.Items.Clear();
            CommonUtils.ThrougAllLayer(m_iApp.FocusMap, new CommonUtils.DealLayer(this.AddName));
            if (this.LayerBox.Items.Count > 0)
            {
                this.LayerBox.SelectedIndex = 0;
            }
        }

        public void AddName(ILayer pLayer)
        {
            if (pLayer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = pLayer as IFeatureLayer;
                if (this.pPipeCfg.IsPipelineLayer(featureLayer.FeatureClass.AliasName, enumPipelineDataType.Line))
                {
                    PoPointAlarmForm.LayerItem pclass = new PoPointAlarmForm.LayerItem();
                    pclass.featureLayer = featureLayer;
                    this.LayerBox.Items.Add(pclass);
                }
            }
        }

        public void DeleteAllElements(IMap pMap)
        {
            IGraphicsContainer graphicsContainer = (IGraphicsContainer) pMap;
            graphicsContainer.DeleteAllElements();
            IActiveView activeView = pMap as IActiveView;
            activeView.PartialRefresh((esriViewDrawPhase) 8, null, null);
            activeView.Refresh();
        }

        private void btnAnalyse_Click(object obj, EventArgs eventArgs)
        {
            PoPointAlarmForm.LayerItem pclass = this.LayerBox.SelectedItem as PoPointAlarmForm.LayerItem;
            this.DeleteAllElements(m_iApp.FocusMap);
            if (this.ppalarmResult_0 == null)
            {
                this.ppalarmResult_0 = new PPAlarmResult(m_iApp, pPipeCfg);
                this.ppalarmResult_0.App = this.m_iApp;
                this.ppalarmResult_0.m_pCurLayer = pclass.featureLayer;
                this.ppalarmResult_0.m_strLayerName = this.LayerBox.Text;
                this.ppalarmResult_0.m_nExpireTime = (int) Convert.ToSingle(this.txBoxExpireTime.Text.Trim());
                this.ppalarmResult_0.Show();
            }
            else if (this.ppalarmResult_0.Visible)
            {
                this.ppalarmResult_0.m_strLayerName = this.LayerBox.Text;
                this.ppalarmResult_0.m_pCurLayer = pclass.featureLayer;
                this.ppalarmResult_0.m_nExpireTime = (int) Convert.ToSingle(this.txBoxExpireTime.Text.Trim());
                this.ppalarmResult_0.ThrougAllLayer();
            }
            else
            {
                this.ppalarmResult_0.m_strLayerName = this.LayerBox.Text;
                this.ppalarmResult_0.m_pCurLayer = pclass.featureLayer;
                this.ppalarmResult_0.m_nExpireTime = (int) Convert.ToSingle(this.txBoxExpireTime.Text.Trim());
                this.ppalarmResult_0.ThrougAllLayer();
                this.ppalarmResult_0.Visible = true;
            }
        }
    }
}