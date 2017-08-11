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
    class CmdSXFKJC : YutaiTool
    {
        private CheckPlugin _plugin;
        private IPipelineConfig _config;
        private FrmFKJC _frmFkjc;
        private CheckResultDockPanelService _dockPanelService;
        private IDictionary<int, IField> _selectedFields;
        private List<FeatureItem> _featureItems;

        private IFeatureLayer _featureLayer;

        public CmdSXFKJC(IAppContext context, CheckPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            if (_frmFkjc == null)
                _frmFkjc = new FrmFKJC(_context);
            _frmFkjc.RefreshLayers();
            if (_frmFkjc.ShowDialog() != DialogResult.OK)
                return;

            _featureLayer = _frmFkjc.FeatureLayer;
            _selectedFields = _frmFkjc.SelectedFields;
            if (!FKJC())
                return;
            if (_dockPanelService == null)
                _dockPanelService = _context.Container.GetInstance<CheckResultDockPanelService>();
            _dockPanelService.View.DisplayName = true;
            _dockPanelService.View.DisplayRemarks = false;
            _dockPanelService.View.FeatureItems = _featureItems;
            _dockPanelService.View.FeatureLayer = _featureLayer;
            _dockPanelService.View.ReloadData();

            if (_dockPanelService.Visible == false)
                _dockPanelService.Show();
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "属性非空检查";
            base.m_category = "Check_Pipeline";
            base.m_name = "Check_Pipeline_SXFKJC";
            base._key = "Check_Pipeline_SXFKJC";
            base.m_toolTip = "将所选属性为空的要素检查出来。";
            base.m_checked = false;
            base.m_message = "属性非空检查";
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
        private bool FKJC()
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
                    string msg = "";
                    foreach (KeyValuePair<int, IField> selectedField in _selectedFields)
                    {
                        object objFeature = pFeature.Value[selectedField.Key];

                        if (objFeature is DBNull || string.IsNullOrWhiteSpace(objFeature.ToString()))
                        {
                            if (string.IsNullOrWhiteSpace(msg))
                                msg += $"为空字段：{selectedField.Value.Name};";
                            else
                            {
                                msg += $"{selectedField.Value.Name};";
                            }
                        }
                    }
                    if (string.IsNullOrWhiteSpace(msg))
                        continue;
                    _featureItems.Add(new FeatureItem(pFeature)
                    {
                        Name = msg
                    });
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
