using System;
using System.Collections.Generic;
using Yutai.Pipeline.Editor.Commands;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        private List<YutaiCommand> _commands;
        private PipelineEditorPlugin _plugin;
        private List<string> _commandKeys;

        public YutaiCommands(IAppContext context, PluginIdentity identity)
            : base(context, identity)
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
                _commands = new List<YutaiCommand>()
                {
                    new CmdPointLineLinkage(_context, _plugin),
                    new CmdMoveTurningPoint(_context, _plugin),
                    new CmdDeletePipePoint(_context, _plugin),
                    new CmdCalculateDepth(_context, _plugin),
                    new CmdAngleConvert(_context, _plugin),
                    new CmdAngleForFPCQQX(_context, _plugin),
                    new CmdAngleForYSBZ(_context, _plugin),
                    new CmdAnnotationSorting(_context, _plugin),
                    new CmdAutoGenerateNumbers(_context, _plugin),
                    new CmdCenterPoint(_context, _plugin),
                    new CmdCopyAttributes(_context, _plugin),
                    new CmdIdentifyRoadName(_context, _plugin),
                    new CmdLayerByAttribute(_context, _plugin),
                    new CmdLineLineToRightAngle(_context, _plugin),
                    new CmdPointLineToRightAngle(_context, _plugin),
                    new CmdModifyFlow(_context, _plugin),
                    new CmdPartialDistance(_context, _plugin),
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