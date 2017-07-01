using System;
using System.Collections.Generic;
using ESRI.ArcGIS.ADF.BaseClasses;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Concrete
{
    public abstract class CommandProviderBase
    {
        protected readonly Dictionary<string, YutaiCommand> Commands = new Dictionary<string, YutaiCommand>();
        protected IAppContext _context;

        protected CommandProviderBase(IAppContext context, PluginIdentity identity)
        {
            _context = context;
            if (identity == null) throw new ArgumentNullException("identity");

            var list = GetCommands();

            foreach (var cmd in list)
            {
                cmd.PluginIdentity = identity;
                Commands.Add(cmd.Name, cmd);
            }

            AssignShortcutKeys();
        }

        protected virtual void AssignShortcutKeys()
        {
            // override in derived class
        }

        /// <summary>
        /// Defines the list of menu commands, populate a List with commands that your plugin is using.
        /// </summary>
        public abstract IEnumerable<YutaiCommand> GetCommands();

        /// <summary>
        /// Gets the <see cref="MenuCommand"/> with the specified key.
        /// </summary>
        public YutaiCommand this[string key]
        {
            get { return Commands[key]; // don't catch it, if there is a mistake we want to know at once
            }
        }
    }
}