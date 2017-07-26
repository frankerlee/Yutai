using System;
using System.Collections.Generic;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Template.Commands;

namespace Yutai.Plugins.Template.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        private List<YutaiCommand> _commands;
        private TemplatePlugin _plugin;
        private List<string> _commandKeys;

        public YutaiCommands(IAppContext context, PluginIdentity identity)
            : base(context, identity)
        {
        }

        public TemplatePlugin Plugin
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
                   new CmdCreateFCByTemplate(_context,_plugin),
                   new CmdCreateFeatureDatasetByTemplate(_context,_plugin),
                   new CmdShowTemplateView(_context),
                };
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            //_commandKeys = new List<string>();
            //foreach (var command in _commands)
            //{
            //    if (command.ItemType == RibbonItemType.Page || command.ItemType == RibbonItemType.ButtonGroup ||
            //        command.ItemType == RibbonItemType.PageGroup)
            //        continue;
            //    _commandKeys.Add(command.Name);
            //}
            //}
            return _commands;
        }
    }
}