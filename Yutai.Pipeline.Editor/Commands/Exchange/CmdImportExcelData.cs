using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Forms.Exchange;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Exchange
{
    class CmdImportExcelData : YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;

        public CmdImportExcelData(IAppContext context, PipelineEditorPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            FrmImportExcelData frm = new FrmImportExcelData(_context);
            frm.ShowDialog();
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "导入Excel数据";
            base.m_category = "PipelineEditor";
            base.m_bitmap = Properties.Resources.icon_pipe_excel;
            base.m_name = "PipelineEditor_ImportExcelData";
            base._key = "PipelineEditor_ImportExcelData";
            base.m_toolTip = "";
            base.m_checked = false;
            base.m_message = "";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
        
        public override void OnDblClick()
        {

        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
        }
    }
}
