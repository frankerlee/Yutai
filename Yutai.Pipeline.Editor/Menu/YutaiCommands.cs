using System;
using System.Collections.Generic;
using Yutai.Pipeline.Editor.Commands;
using Yutai.Pipeline.Editor.Commands.Common;
using Yutai.Pipeline.Editor.Commands.Exchange;
using Yutai.Pipeline.Editor.Commands.Mark;
using Yutai.Pipeline.Editor.Commands.Profession;
using Yutai.Pipeline.Editor.Commands.Setting;
using Yutai.Pipeline.Editor.Commands.Verify;
using Yutai.Pipeline.Editor.Commands.Version;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        private List<YutaiCommand> _commands;
        private PipelineEditorPlugin _plugin;
        private List<string> _commandKeys;

        public YutaiCommands(IAppContext context, PipelineEditorPlugin plugin)
            : base(context, plugin.Identity)
        {
        }

        public PipelineEditorPlugin Plugin
        {
            get { return _plugin; }
            set { _plugin = value; }
        }

        public List<string> GetKeys()
        {
            return _commandKeys;
        }

        public override IEnumerable<YutaiCommand> GetCommands()
        {
            //第一次被初始化的时候Plugin为空，出现错误，所以在创建菜单的时候重新进行了初始化。

            try
            {
                if (_plugin == null)
                    return new List<YutaiCommand>();
                _commands = new List<YutaiCommand>()
                {
                    new CmdSetLayer(_context, _plugin),

                    new CmdCreateLine(_context, _plugin),
                    new CmdCreateLineWithPoint(_context, _plugin),
                    new CmdCreateLineWithFromPoint(_context, _plugin),
                    new CmdCreateLineWithToPoint(_context, _plugin),
                    new CmdDeletePoint(_context, _plugin),
                    new CmdSplitLine(_context, _plugin),
                    new CmdSplitLineByPoint(_context, _plugin),
                    new CmdPointLineLinkage(_context, _plugin),
                    new CmdPipelineStretch(_context, _plugin),
                    new CmdPointLineToRightAngle(_context, _plugin),
                    new CmdLineLineToRightAngle(_context, _plugin),

                    new CmdAngleConvert(_context, _plugin),
                    new CmdAutoGenerateNumbers(_context, _plugin),
                    new CmdIdentifyRoadName(_context, _plugin),

                    new CmdConversion(_context, _plugin),
                    new CmdClip(_context, _plugin),
                    new CmdDataExport(_context, _plugin),
                    new CmdDataStorage(_context, _plugin),
                    new CmdExportExcel(_context, _plugin),
                    new CmdExportMdb(_context, _plugin),
                    new CmdImportAxfData(_context, _plugin),
                    new CmdImportExcelData(_context, _plugin),
                    new CmdImportSurveyData(_context, _plugin),

                    new CmdAnnotationSorting(_context, _plugin),
                    new CmdBiaoZhu(_context, _plugin),
                    new CmdCheQi(_context, _plugin),
                    new CmdCheQiLianDong(_context, _plugin),
                    new CmdCheQiSheZhi(_context, _plugin),
                    new CmdDuoYaoSuCheQi(_context, _plugin),
                    new CmdDuoYaoSuCheQiSheZhi(_context, _plugin),
                    new CmdDeleteAll(_context, _plugin),

                    new CmdVerifySetting(_context, _plugin),

                    new CmdVersionManagement(_context, _plugin),
                    new CmdVersionQuery(_context, _plugin),
                };
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }


            return _commands;
        }
    }
}