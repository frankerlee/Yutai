using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Identifer.Commands;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Identifer.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        private List<YutaiCommand> _commands;
        private IdentifierPlugin _plugin;

        public YutaiCommands(IAppContext context, PluginIdentity identity)
            : base(context, identity)
        {
        }

        public IdentifierPlugin Plugin
        {
            get { return _plugin; }
            set { _plugin = value; }
        }

        public override IEnumerable<YutaiCommand> GetCommands()
        {
            //if (_commands == null)
            //{
            _commands = new List<YutaiCommand>()
            {
                new YutaiMenuCommand(RibbonItemType.ToolStrip, "View", "View.Info", "View.Info", "查看要素", "", ""),
                new CmdViewIdentifier(_context, _plugin) as YutaiCommand,
                new YutaiMenuCommand(RibbonItemType.TabItem, "Query", "Query", "Query", "查询", "", ""),
                new YutaiMenuCommand(RibbonItemType.ToolStrip, "Query", "Query.Common", "Query.Common", "查询", "", ""),
                new CmdStartQueryBuilder(_context) as YutaiCommand,
                new CmdStartQueryLocation(_context) as YutaiCommand,
                new CmdStartQueryAttributeAndLocation(_context) as YutaiCommand,
                new CmdSetSelectableLayer(_context) as YutaiCommand,
                new CmdSetCurrentLayer(_context,_plugin) as YutaiCommand,
                new YutaiMenuCommand(RibbonItemType.ToolStrip, "Query", "Query.SelectionTools", "Query.SelectionTools",
                    "图形查询", "", ""),
                new YutaiMenuCommand(RibbonItemType.Panel, "Query", "Query.SelectionTools.Mouse",
                    "Query.SelectionTools.Mouse", "图形查询", "", "") {PanelRowCount = 2},
                new CmdSelectByMouse(_context) {SubType = 0} as YutaiCommand,
                new CmdSelectByMouse(_context) {SubType = 1} as YutaiCommand,
                new CmdZoomToSelection(_context) as YutaiCommand,
                new CmdSwitchSelection(_context, _plugin) as YutaiCommand,
                new YutaiMenuCommand(RibbonItemType.ToolStrip, "Query", "Query.Buffer", "Query.Buffer", "缓冲区查询", "", ""),
                new CmdSelectByBuffer(_context) {SubType = 0} as YutaiCommand,
                new CmdSelectByBuffer(_context) {SubType = 1} as YutaiCommand
            };
            //}
            return _commands;
        }
    }
}