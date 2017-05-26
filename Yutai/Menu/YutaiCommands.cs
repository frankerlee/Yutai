using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Commands.Document;
using Yutai.Commands.Views;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        private List<YutaiCommand> _commands;

        public YutaiCommands(IAppContext context, PluginIdentity identity)
            : base(context, identity)
        {
        }


        public override IEnumerable<YutaiCommand> GetCommands()
        {
            if (_commands == null)
            {
                _commands = new List<YutaiCommand>()
                {
                    new YutaiMenuCommand(RibbonItemType.TabItem, "File", "File", "File", "文件", "", ""),
                    new YutaiMenuCommand(RibbonItemType.ToolStrip, "File", "File.Document", "File.Document", "常用", "",
                        ""),
                    new CmdOpenDocument(_context),
                    new YutaiMenuCommand(RibbonItemType.TabItem, "View", "View", "View", "视图", "", ""),
                    new YutaiMenuCommand(RibbonItemType.ToolStrip, "View", "View.Common", "View.Common", "视图控制", "", ""),
                    new CmdViewZoomIn(_context) as YutaiCommand,
                    new CmdViewZoomOut(_context) as YutaiCommand,
                    new CmdViewPan(_context) as YutaiCommand,
                    new CmdViewFixedZoomIn(_context) as YutaiCommand,
                    new CmdViewFixedZoomOut(_context) as YutaiCommand,
                    new CmdViewFullExtent(_context) as YutaiCommand,
                    new CmdViewZoomPrev(_context) as YutaiCommand,
                    new CmdViewZoomNext(_context) as YutaiCommand,
                    new YutaiMenuCommand(RibbonItemType.ToolStrip, "View", "View.Measure", "View.Measure", "量测工具", "", ""),
                     new CmdViewMeasureLength(_context) as YutaiCommand,
                      new CmdViewMeasureArea(_context) as YutaiCommand
                };
            }
            return _commands;
        }
    }
}