using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Check.Classes;
using Yutai.Check.Forms;
using Yutai.Check.Services;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Check.Commands.CheckPipeline
{
    class CmdLDJC : YutaiTool
    {
        private CheckPlugin _plugin;
        private IPipelineConfig _config;
        private FrmLDJC _frmLdjc;
        private CheckResultDockPanelService _dockPanelService;
        private List<FeatureItem> _featureItems;

        private IFeatureLayer _pointFeatureLayer;
        private IFeatureLayer _lineFeatureLayer;

        public CmdLDJC(IAppContext context, CheckPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            if (_frmLdjc == null)
                _frmLdjc = new FrmLDJC(_context);
            if (_frmLdjc.ShowDialog() != DialogResult.OK)
                return;

            _pointFeatureLayer = _frmLdjc.PointFeatureLayer;
            _lineFeatureLayer = _frmLdjc.LineFeatureLayer;
            if (!LDJC())
                return;
            if (_dockPanelService == null)
                _dockPanelService = _context.Container.GetInstance<CheckResultDockPanelService>();
            _dockPanelService.View.DisplayName = true;
            _dockPanelService.View.DisplayRemarks = false;
            _dockPanelService.View.FeatureItems = _featureItems;
            _dockPanelService.View.FeatureLayer = _lineFeatureLayer;
            _dockPanelService.View.ReloadData();

            if (_dockPanelService.Visible == false)
                _dockPanelService.Show();
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "漏点检查";
            base.m_category = "Check_Pipeline";
            base.m_name = "Check_Pipeline_LDJC";
            base._key = "Check_Pipeline_LDJC";
            base.m_toolTip = "将所选属性完全相同的要素检查出来。";
            base.m_checked = false;
            base.m_message = "漏点检查";
            base._itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                if (_context.FocusMap == null)
                    return false;
                if (_context.FocusMap.LayerCount <= 0)
                    return false;
                return true;
            }
        }
        private bool LDJC()
        {
            try
            {
                if (_pointFeatureLayer == null || _lineFeatureLayer == null)
                    return false;

                _featureItems = new List<FeatureItem>();
                IFeatureCursor pFeatureCursor = _lineFeatureLayer.Search(null, false);
                IFeature pFeature;
                ISpatialFilter spatialFilter;
                IFeatureCursor featureCursor;
                while ((pFeature = pFeatureCursor.NextFeature()) != null)
                {
                    string msg = "";
                    IPolyline polyline = pFeature.Shape as IPolyline;
                    if (polyline == null || polyline.IsEmpty)
                        continue;
                    spatialFilter = new SpatialFilterClass();
                    spatialFilter.Geometry = polyline.FromPoint;
                    spatialFilter.GeometryField = _pointFeatureLayer.FeatureClass.ShapeFieldName;
                    spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    spatialFilter.OutputSpatialReference[_pointFeatureLayer.FeatureClass.ShapeFieldName] =
                        _context.FocusMap.SpatialReference;
                    featureCursor = _pointFeatureLayer.Search(spatialFilter, false);
                    if (featureCursor.NextFeature() == null)
                    {
                        // 缺少起点
                        msg += "管线漏点，漏点为起点";
                    }
                    featureCursor = null;
                    spatialFilter.Geometry = polyline.ToPoint;
                    featureCursor = _pointFeatureLayer.Search(spatialFilter, false);
                    if (featureCursor.NextFeature() == null)
                    {
                        // 缺少终点
                        if (msg.Length <= 0)
                        {
                            msg += "管线漏点，漏点为终点";
                        }
                        else
                        {
                            msg += "及终点";
                        }
                    }
                    featureCursor = null;
                    if (msg.Length > 0)
                    {
                        _featureItems.Add(new FeatureItem(pFeature)
                        {
                            Name = msg
                        });
                    }
                }
                Marshal.ReleaseComObject(pFeatureCursor);
                
                if (_featureItems.Any())
                {
                    MessageBox.Show(@"检查完毕", @"提示");
                    return true;
                }
                else
                {
                    MessageBox.Show(@"检查完毕，未发现任何问题", @"提示");
                    return false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return false;
            }
        }


    }
}
