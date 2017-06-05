// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdSwitchSelectedFeature.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/05  15:20
// 更新时间 :  2017/06/05  15:20

using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.MapLegend
{
    public class CmdSwitchSelectedFeature : YutaiCommand
    {

        private IMapLegendView _view;


        public CmdSwitchSelectedFeature(IAppContext context, IMapLegendView view)
        {
            _context = context;
            _view = view;
            OnCreate();
        }

        private void OnCreate()
        {
            base.m_caption = "切换选择";
            base.m_category = "TOC";
            base.m_bitmap = null;
            base.m_name = "dropSelection.mnuSwitchSelectedFeature";
            base._key = "dropSelection.mnuSwitchSelectedFeature";
            base.m_toolTip = "切换选择";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            OnCreate();
        }

        public void OnClick()
        {
            if (_view.SelectedItemType == esriTOCControlItem.esriTOCControlItemLayer && _view.SelectedLayer != null)
            {
                IActiveView pActiveView = _view.SelectedMap as IActiveView;
                if (pActiveView != null)
                {
                    if (_view.SelectedLayer is IFeatureLayer)
                    {
                        IFeatureSelection featureSelection = _view.SelectedLayer as IFeatureSelection;
                        if (featureSelection != null)
                        {
                            int count = featureSelection.SelectionSet.Count;
                            ISelectionEnvironment pSelectionEnvironment = new SelectionEnvironmentClass();
                            if ((_view.SelectedLayer as IFeatureLayer).FeatureClass.FeatureCount(null) - count <= (pSelectionEnvironment as ISelectionEnvironmentThreshold).WarningThreshold || MessageBox.Show("所选择记录较多，执行将花较长时间，是否继续？", "选择", MessageBoxButtons.YesNo) != DialogResult.No)
                            {
                                featureSelection.SelectFeatures(null, esriSelectionResultEnum.esriSelectionResultXOR, false);
                                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, featureSelection, null);
                            }
                        }
                    }
                }
            }
        }
    }
}