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
        private List<string> _commandKeys;
        public YutaiCommands(IAppContext context, PluginIdentity identity)
            : base(context, identity)
        {
        }

        public IdentifierPlugin Plugin
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
            //if (_commands == null)
            //{
            //    _commands = new List<YutaiCommand>()
            //{
            //    new YutaiMenuCommand(RibbonItemType.PageGroup, "View", "View.Info", "View.Info", "查看要素", "", ""),
            //    new CmdViewIdentifier(_context, _plugin) as YutaiCommand,
            //    new YutaiMenuCommand(RibbonItemType.Page, "Query", "Query", "Query", "查询", "", ""){Position = 3},
            //    new YutaiMenuCommand(RibbonItemType.PageGroup, "Query", "Query.Common", "Query.Common", "查询", "", ""),
            //    new CmdStartQueryBuilder(_context) as YutaiCommand,
            //    new CmdStartQueryLocation(_context) as YutaiCommand,
            //    new CmdStartQueryAttributeAndLocation(_context) as YutaiCommand,
            //    new YutaiMenuCommand(RibbonItemType.PageGroup, "Query", "Query.Setting", "Query.Setting", "设置", "", ""),
            //    new CmdSetSelectableLayer(_context) as YutaiCommand,
            //     new YutaiMenuCommand(RibbonItemType.ButtonGroup, "Query", "Query.Setting.Panel", "Query.Setting.Panel", "查询设置", "", "") {PanelRowCount = 2},
            //    new CmdSetCurrentLayer(_context,_plugin) as YutaiCommand,
            //    new CmdSetSelectRelation(_context,_plugin) as YutaiCommand,
            //    new YutaiMenuCommand(RibbonItemType.PageGroup, "Query", "Query.SelectionTools", "Query.SelectionTools",
            //        "图形查询", "", ""),

            //    new CmdSelectByMouse(_context) {SubType = -1,ItemType= RibbonItemType.DropDown}  as YutaiCommand,
            //     new CmdSelectByBuffer(_context) {SubType = -1,ItemType= RibbonItemType.DropDown}  as YutaiCommand,
            //     new CmdSelectAll(_context,_plugin) as YutaiCommand,
            //     new CmdSelectByScreen(_context,_plugin) as YutaiCommand,
            //    new CmdSwitchSelection(_context, _plugin) as YutaiCommand,
            //    new CmdZoomToSelection(_context) as YutaiCommand,
            //    new CmdSelectClear(_context) as YutaiCommand

            //};

            _commands = new List<YutaiCommand>()
            {
             
                new CmdViewIdentifier(_context, _plugin) as YutaiCommand,
                new CmdStartQueryBuilder(_context) as YutaiCommand,
                new CmdStartQueryLocation(_context) as YutaiCommand,
                new CmdStartQueryAttributeAndLocation(_context) as YutaiCommand,
                new CmdSetSelectableLayer(_context) as YutaiCommand,
                new CmdSetCurrentLayer(_context,_plugin) as YutaiCommand,
                new CmdSetSelectRelation(_context,_plugin) as YutaiCommand,
                new CmdSelectByMouse(_context) {SubType = -1,ItemType= RibbonItemType.DropDown}  as YutaiCommand,
                 new CmdSelectByBuffer(_context) {SubType = -1,ItemType= RibbonItemType.DropDown}  as YutaiCommand,
                 new CmdSelectAll(_context,_plugin) as YutaiCommand,
                 new CmdSelectByScreen(_context,_plugin) as YutaiCommand,
                new CmdSwitchSelection(_context, _plugin) as YutaiCommand,
                new CmdZoomToSelection(_context) as YutaiCommand,
                new CmdSelectClear(_context) as YutaiCommand

            };
            _commandKeys = new List<string>();
            foreach (var command in _commands)
            {
                if (command.ItemType == RibbonItemType.Page || command.ItemType == RibbonItemType.ButtonGroup ||
                    command.ItemType == RibbonItemType.PageGroup)
                    continue;
                _commandKeys.Add(command.Name);
            }
           //}
            return _commands;
        }
    }
}