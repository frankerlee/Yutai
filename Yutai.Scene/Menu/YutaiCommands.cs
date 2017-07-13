using System;
using System.Collections.Generic;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Scene.Commands;
using Yutai.Plugins.Scene.Commands.Common;
using Yutai.Plugins.Scene.Commands.View;

namespace Yutai.Plugins.Scene.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        private List<YutaiCommand> _commands;
        private IScenePlugin _plugin;
        private List<string> _commandKeys;

        public YutaiCommands(IAppContext context, PluginIdentity identity)
            : base(context, identity)
        {
        }

        public IScenePlugin Plugin
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
                    new CmdOpenSceneView(_context, _plugin),
                    new CmdOpenSXD(_context, _plugin),
                    new CmdOpenProjectSXD(_context, _plugin),
                     new CmdSceneAddData(_context, _plugin),
                    new CmdLoadDGXFeatureClass(_context, _plugin),
                    new CmdSceneStyleManagerItem(_context, _plugin),


                    new ToolSceneZoomIn(_context, _plugin),
                    new ToolSceneZoomOut(_context, _plugin),
                    new CmdSceneFullExtent(_context, _plugin),
                    new ToolScenePan(_context, _plugin),
                    new ToolSceneFly(_context, _plugin),
                    new ToolSceneNavigate(_context, _plugin),
                    new ToolSceneSetObserver(_context, _plugin),
                    new ToolSceneTargetCenter(_context, _plugin),
                    new ToolSceneTargetZoom(_context, _plugin),
                    new ToolSceneZoomInOut(_context, _plugin),
                    new CmdSceneExpandFOV(_context, _plugin),
                    new CmdSceneNarrowFOV(_context, _plugin),
                    new CmdManageBookmark3D(_context, _plugin),
                    new CmdCreateBookmark3D(_context, _plugin),
                    new ToolSceneDrawFlyByPath(_context, _plugin),
                    new ToolSceneLineOfSight(_context, _plugin),
                    new CmdSceneViewerSetting(_context, _plugin),
                    new ToolSceneDrawStaticLoc(_context, _plugin),
                    new CmdLinkMapAndScene(_context,_plugin),


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