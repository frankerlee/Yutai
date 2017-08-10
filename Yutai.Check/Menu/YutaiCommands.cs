using System;
using System.Collections.Generic;
using Yutai.Check.Commands.CheckPipeline;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;

namespace Yutai.Check.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        private List<YutaiCommand> _commands;
        private CheckPlugin _plugin;
        private List<string> _commandKeys;

        public YutaiCommands(IAppContext context, CheckPlugin plugin)
            : base(context, plugin.Identity)
        {
        }

        public CheckPlugin Plugin
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
                    new CmdDDJC(_context, _plugin),
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
