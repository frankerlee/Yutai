using System;
using System.Collections.Generic;
using Yutai.Pipeline.Analysis;
using Yutai.Pipeline.Analysis.Commands;
using Yutai.Pipeline.Analysis.QueryForms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        private List<YutaiCommand> _commands;
        private PipelineAnalysisPlugin _plugin;
        private List<string> _commandKeys;

        public YutaiCommands(IAppContext context, PluginIdentity identity)
            : base(context, identity)
        {
        }

        public PipelineAnalysisPlugin Plugin
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
                    new CmdStartOrganizeMap(_context,_plugin),


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