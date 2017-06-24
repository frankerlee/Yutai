using System;
using ESRI.ArcGIS.Carto;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdPreAlarmAnalysis : YutaiTool
    {

        private PreAlarmDlg preAlarmDlg_0;
        private PipelineAnalysisPlugin _plugin;


        public CmdPreAlarmAnalysis(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick()
        {

            _context.SetCurrentTool(this);
            if (this.preAlarmDlg_0 == null)
            {
                this.preAlarmDlg_0 = new PreAlarmDlg(_context,_plugin.PipeConfig);
                this.preAlarmDlg_0.App = _context;
                this.preAlarmDlg_0.pPipeCfg = _plugin.PipeConfig;
                this.preAlarmDlg_0.Show();
            }
            else if (!this.preAlarmDlg_0.Visible)
            {
                this.preAlarmDlg_0.Visible = true;
            }


        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "预警分析";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_analysis_collision;
            base.m_name = "PipeAnalysis_PreAlarmAnalysis";
            base._key = "PipeAnalysis_PreAlarmAnalysis";
            base.m_toolTip = "预警分析";
            base.m_checked = false;
            base.m_message = "预警分析";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            CommonUtils.AppContext = _context;
        }

        private void method_0(IMap map)
        {
            int layerCount = map.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer layer = map.get_Layer(i);
                if (layer is ICompositeLayer)
                {
                    this.VerifyLayer(layer);
                }
                else
                {
                    this.method_1((IFeatureLayer)layer);
                }
            }
        }

        public ILayer VerifyLayer(ILayer pLayVal)
        {
            ICompositeLayer compositeLayer = pLayVal as ICompositeLayer;
            int count = compositeLayer.Count;
            for (int i = 0; i < count; i++)
            {
                ILayer layer = compositeLayer.get_Layer(i);
                if (layer is ICompositeLayer)
                {
                    this.VerifyLayer(layer);
                }
                else
                {
                    this.method_1((IFeatureLayer)layer);
                }
            }
            return null;
        }

        private void method_1(IFeatureLayer featureLayer)
        {
        }
      
      
    }
}