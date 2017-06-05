using System.Collections.Generic;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Commands;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        private List<YutaiCommand> _commands;
        private EditorPlugin _plugin;
        private List<string> _commandKeys;
        public YutaiCommands(IAppContext context, PluginIdentity identity)
            : base(context, identity)
        {
        }

        public EditorPlugin Plugin
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
           _commands = new List<YutaiCommand>()
            {
                new YutaiMenuCommand(RibbonItemType.TabItem, "Edit", "Edit", "Edit", "编辑", "", "") {Position = 4},
                new YutaiMenuCommand(RibbonItemType.ToolStrip, "Edit", "Edit.Common", "Edit.Common", "编辑控制", "", "") ,
                new CmdEditorStart(_context,_plugin),
                new CmdEditorStop(_context,_plugin),
                new CmdEditorSave(_context,_plugin),
                new YutaiMenuCommand(RibbonItemType.ToolStrip, "Edit", "Edit.Snap", "Edit.Snap", "捕捉设置", "", "") ,
                new CmdSnapOff(_context),
                new YutaiMenuCommand(RibbonItemType.Panel, "Edit", "Edit.Snap.Config", "Edit.Snap.Config", "", "", "") {PanelRowCount = 3} ,
                new CmdSnapPoint(_context),
                new CmdSnapMidPoint(_context),
                new CmdSnapEndPointt(_context),
                new CmdSnapVertexPoint(_context),
                new CmdSnapIntersectPoint(_context),
                new CmdSnapBoundary(_context),
                new CmdSnapTangent(_context),
                new CmdSnapSketch(_context)
            };
            _commandKeys = new List<string>();
            foreach (var command in _commands)
            {
                if (command.ItemType == RibbonItemType.TabItem || command.ItemType == RibbonItemType.Panel ||
                    command.ItemType == RibbonItemType.ToolStrip)
                    continue;
                _commandKeys.Add(command.Name);
            }
           //}
            return _commands;
        }
    }
}