using System;
using System.Collections.Generic;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Commands;

namespace Yutai.Plugins.Printing.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        private List<YutaiCommand> _commands;
        private PrintingPlugin _plugin;
        private List<string> _commandKeys;

        public YutaiCommands(IAppContext context, PluginIdentity identity)
            : base(context, identity)
        {
        }

        public PrintingPlugin Plugin
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
                   new CmdStartLayout(_context,_plugin),


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