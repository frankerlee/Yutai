using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Commands.Document;
using Yutai.Commands.Views;
using Yutai.Commands.Windows;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        private List<YutaiCommand> _commands;
        private List<string> _commandKeys;
        

        public YutaiCommands(IAppContext context, PluginIdentity identity)
            : base(context, identity)
        {
           
        }

        public List<string> GetKeys()
        {
            return _commandKeys;
        }
        public override IEnumerable<YutaiCommand> GetCommands()
        {
            if (_commands == null)
            {
                _commands = new List<YutaiCommand>()
                {
                    new YutaiMenuCommand(RibbonItemType.TabItem, "File", "File", "File", "文件", "", ""),
                    new YutaiMenuCommand(RibbonItemType.ToolStrip, "File", "File.Document", "File.Document", "项目", "",
                        "") ,
                    new CmdNewYutaiDoc(_context),
                    new CmdOpenYutaiDoc(_context),
                    new YutaiMenuCommand(RibbonItemType.Panel, "File", "File.Document.Sub", "File.Document.Sub", "", "",
                        "") {PanelRowCount = 3,ToolStripLayoutStyleYT =  ToolStripLayoutStyleYT.Flow},
                    new CmdSaveYutaiDoc(_context),
                    new CmdCloseYutaiDoc(_context),
                    new YutaiMenuCommand(RibbonItemType.ToolStrip, "File", "File.Mxd", "File.Mxd", "二维文档", "",
                        ""),
                    new CmdOpenMapDocument(_context),
                    new CmdSaveMapDocument(_context),
                    new CmdCloseMapDocument(_context),
                     new YutaiMenuCommand(RibbonItemType.ToolStrip, "File", "File.Sxd", "File.Sxd", "三维文档", "",
                        ""),
                    new CmdOpenSceneDocument(_context),
                    new CmdSaveSceneDocument(_context),
                    new CmdCloseSceneDocument(_context),
                    new YutaiMenuCommand(RibbonItemType.TabItem, "View", "View", "View", "视图", "", ""),
                    new YutaiMenuCommand(RibbonItemType.ToolStrip, "View", "View.Common", "View.Common", "视图控制", "", ""),
                    new CmdViewZoomIn(_context) as YutaiCommand,
                    new CmdViewZoomOut(_context) as YutaiCommand,
                    new CmdViewPan(_context) as YutaiCommand,
                    new CmdViewFixedZoomIn(_context) as YutaiCommand,
                    new CmdViewFixedZoomOut(_context) as YutaiCommand,
                    new CmdViewFullExtent(_context) as YutaiCommand,
                    new CmdViewZoomPrev(_context) as YutaiCommand,
                    new CmdViewZoomNext(_context) as YutaiCommand,
                    new YutaiMenuCommand(RibbonItemType.ToolStrip, "View", "View.Measure", "View.Measure", "量测工具", "", ""),
                     new CmdViewMeasureLength(_context) as YutaiCommand,
                      new CmdViewMeasureArea(_context) as YutaiCommand,
                      new YutaiMenuCommand(RibbonItemType.TabItem, "Window", "Window", "Window", "窗口", "", ""),
                    new YutaiMenuCommand(RibbonItemType.ToolStrip, "Window", "Window.Common", "Window.Common", "窗口控制", "", ""),
                    new CmdLegendDock(_context) as YutaiCommand,
                    new CmdOverviewDock(_context) as YutaiCommand
                };
                _commandKeys=new List<string>();
                foreach (var command in _commands)
                {
                    if (command.ItemType == RibbonItemType.TabItem || command.ItemType == RibbonItemType.Panel ||
                        command.ItemType == RibbonItemType.ToolStrip)
                        continue;
                    _commandKeys.Add(command.Name);
                }
            }
            return _commands;
        }
    }
}