using System;
using System.Windows.Forms;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.ConfigCommands
{
    class CmdSavePipeConfig : YutaiCommand
    {
        private PipelineAnalysisPlugin _plugin;
        private frmMapLayerOrganize frmOrganize = null;
        public CmdSavePipeConfig(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnClick(object sender, EventArgs args)
        {

            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "保存配置";
            base.m_category = "PipelineQuery";
            base.m_bitmap = Properties.Resources.icon_save_config;
            base.m_name = "PipeQuery_SaveConfig";
            base._key = "PipeQuery_SaveConfig";
            base.m_toolTip = "保存配置";
            base.m_checked = false;
            base.m_message = "保存配置";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;

            CommonUtils.AppContext = _context;
        }

        public void OnClick()
        {
            SaveFileDialog dialog=new SaveFileDialog();
            dialog.Filter = "XML配置文件(*.xml)|*.xml";
            dialog.OverwritePrompt = true;
            dialog.Title = "保存管线配置文件";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _plugin.PipeConfig.SaveToXml(dialog.FileName);
            }
            dialog = null;

        }
    }
}