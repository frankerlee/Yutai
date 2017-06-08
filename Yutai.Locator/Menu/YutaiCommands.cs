using System.Collections.Generic;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Locator.Commands;

namespace Yutai.Plugins.Locator.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        private List<YutaiCommand> _commands;
        private LocatorPlugin _plugin;
        public YutaiCommands(IAppContext context, PluginIdentity identity)
            : base(context, identity)
        {

        }

        public LocatorPlugin Plugin
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
                    
                     new CmdStartLocator(_context) as YutaiCommand
                     
                };
            //}
            return _commands;
        }
    }
}
