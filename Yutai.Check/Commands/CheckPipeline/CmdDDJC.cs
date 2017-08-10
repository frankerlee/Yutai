using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Check.Classes;
using Yutai.Check.Forms;
using Yutai.Check.Services;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Check.Commands.CheckPipeline
{
    class CmdDDJC : YutaiTool
    {
        private CheckPlugin _plugin;
        private IPipelineConfig _config;
        private FrmDDJC _frmDdjc;
        private CheckResultDockPanelService _dockPanelService;
        private IDictionary<int, IField> _selectedFields;
        private List<FeatureItem> _featureItems;

        private IFeatureLayer _featureLayer;

        public CmdDDJC(IAppContext context, CheckPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            if (_frmDdjc == null)
                _frmDdjc = new FrmDDJC(_context);
            if (_frmDdjc.ShowDialog() != DialogResult.OK)
                return;

            _featureLayer = _frmDdjc.FeatureLayer;
            _selectedFields = _frmDdjc.SelectedFields;
            if (!DDJC())
                return;

            if (_dockPanelService == null)
                _dockPanelService = _context.Container.GetInstance<CheckResultDockPanelService>();
            _dockPanelService.View.FeatureItems = _featureItems;
            _dockPanelService.View.FeatureLayer = _featureLayer;
            _dockPanelService.View.ReloadData();

            if (_dockPanelService.Visible == false)
                _dockPanelService.Show();
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "单点检查";
            base.m_category = "Check_Pipeline";
            base.m_name = "Check_Pipeline_DDJC";
            base._key = "Check_Pipeline_DDJC";
            base.m_toolTip = "将所选属性完全相同的要素检查出来。";
            base.m_checked = false;
            base.m_message = "单点检查";
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

        private bool DDJC()
        {
            try
            {
                if (_featureLayer == null)
                    return false;

                _featureItems = new List<FeatureItem>();
                // 遍历所有点

                IFeatureCursor pFeatureCursor = _featureLayer.Search(null, false);
                IFeature pFeature;
                while ((pFeature = pFeatureCursor.NextFeature()) != null)
                {
                    _featureItems.Add(new FeatureItem(pFeature));
                }
                Marshal.ReleaseComObject(pFeatureCursor);

                // 过滤重复点
                for (int i = 0; i <= _featureItems.Count - 1; i++)
                {
                    FeatureItem featureItem = _featureItems[i];
                    ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                    pSpatialFilter.Geometry = featureItem.MainFeature.Shape;
                    pSpatialFilter.GeometryField = _featureLayer.FeatureClass.ShapeFieldName;
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    pSpatialFilter.OutputSpatialReference[_featureLayer.FeatureClass.ShapeFieldName] =
                        _context.FocusMap.SpatialReference;
                    pFeatureCursor = _featureLayer.Search(pSpatialFilter, false);
                    while ((pFeature = pFeatureCursor.NextFeature()) != null)
                    {
                        if (featureItem.OID == pFeature.OID)
                        {
                            continue;
                        }
                        bool isSame = true;
                        foreach (KeyValuePair<int, IField> selectedField in _selectedFields)
                        {
                            object objFeature1 = featureItem.MainFeature.Value[selectedField.Key];
                            object objFeature2 = pFeature.Value[selectedField.Key];

                            if (objFeature1 is DBNull && objFeature2 is DBNull)
                            {
                                continue;
                            }
                            if (objFeature1 is DBNull || objFeature2 is DBNull)
                            {
                                isSame = false;
                                break;
                            }
                            if (objFeature1.ToString().Equals(objFeature2.ToString()))
                                continue;
                            isSame = false;
                            break;
                        }
                        if (isSame) // 相同 重复点
                        {
                            _featureItems.Remove(_featureItems.FirstOrDefault(c => c.OID == pFeature.OID));
                            featureItem.SubFeatureItems.Add(new FeatureItem(pFeature));
                            featureItem.Remarks = $"重复个数 - {featureItem.SubFeatureItems.Count}";
                        }
                    }
                }

                for (int i = _featureItems.Count - 1; i >= 0; i--)
                {
                    FeatureItem feature = _featureItems[i];
                    if (feature.SubFeatureItems.Count == 0)
                        _featureItems.Remove(feature);
                }
                return _featureItems.Any();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return false;
            }
        }
    }
}
