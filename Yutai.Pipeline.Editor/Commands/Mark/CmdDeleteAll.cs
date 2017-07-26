// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdDeleteAll.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/07/25  15:11
// 更新时间 :  2017/07/25  15:11

using System;
using System.Windows.Forms;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Mark
{
    public class CmdDeleteAll : YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;
        private ICheQiConfig _cheQiConfig;
        private IMultiCheQiConfig _multiCheQiConfig;

        public CmdDeleteAll(IAppContext context, PipelineEditorPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }


        public override void OnClick(object sender, EventArgs args)
        {
            if (
                MessageBox.Show(@"该功能将会清空扯旗设置/多要素扯旗设置中的注记图层和辅助线图层，是否继续？", @"提示", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                ArcGIS.Common.Editor.Editor.StartEditOperation();
                if (_cheQiConfig != null)
                {
                    CommonHelper.ClearFeaturesInFeatureLayer(_cheQiConfig.FlagAnnoLayer);
                    CommonHelper.ClearFeaturesInFeatureLayer(_cheQiConfig.FlagLineLayer);
                }
                if (_multiCheQiConfig != null)
                {
                    CommonHelper.ClearFeaturesInFeatureLayer(_multiCheQiConfig.FlagAnnoLayer);
                    CommonHelper.ClearFeaturesInFeatureLayer(_multiCheQiConfig.FlagLineLayer);
                }
                _context.ActiveView.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                ArcGIS.Common.Editor.Editor.StopEditOperation();
            }
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "清空注记";
            base.m_category = "PipelineEditor";
            //base.m_bitmap = Properties.Resources.icon_valve;
            base.m_name = "PipelineEditor_DeleteAll";
            base._key = "PipelineEditor_DeleteAll";
            base.m_toolTip = "";
            base.m_checked = false;
            base.m_message = "";
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
                if (ArcGIS.Common.Editor.Editor.EditMap == null)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                    return false;
                if (_plugin.CheQiConfig != null)
                {
                    _cheQiConfig = _plugin.CheQiConfig;
                    return true;
                }
                if (_plugin.MultiCheQiConfig != null)
                {
                    _multiCheQiConfig = _plugin.MultiCheQiConfig;
                    return true;
                }
                return true;
            }
        }
    }
}