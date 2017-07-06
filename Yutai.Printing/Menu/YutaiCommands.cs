using System;
using System.Collections.Generic;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Commands;
using Yutai.Plugins.Printing.Commands.Carto;
using Yutai.Plugins.Printing.Commands.Fence;

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
                    new CmdStartLayout(_context, _plugin),
                    new CmdCloseLayout(_context, _plugin),
                    new CmdPageSetup(_context,_plugin),

                    new ToolInsertLineElement(_context),
                    new ToolInsertCircleElement(_context),
                    new CmdDeleteElement(_context),
                    new CmdElementProperty(_context),
                    new CmdNewLegend(_context),
                    new CmdNewMapFrame(_context),
                    new CmdNewNeatline(_context),
                    new CmdNewNorthArrow(_context),
                    new CmdNewObject(_context),
                    new CmdNewPicture(_context),
                    new CmdNewScaleBar(_context),
                    new CmdNewScaleText(_context),
                    new CmdNewText(_context),
                    new CmdNewTitle(_context),
                    new CmdUndoOperation(_context),
                    new CmdRedoOperation(_context),
                    new ToolInsertEllipseElement(_context),
                    new ToolNewCurveElement(_context),
                    new CmdNewJtbElement(_context),
                    new CmdInsertDataGraphic(_context),
                    new CmdInsertFeactionText(_context),
                    new ToolNewLabelElement(_context),
                    new ToolNewMarkerElement(_context),
                    new ToolNewPolygonElement(_context),
                    new ToolNewRectangleElement(_context),
                    new ToolPagePan(_context),
                    new ToolPageZoomIn(_context),
                    new ToolPageZoomOut(_context),
                    new ToolSelectElement(_context),
                    new CmdPageFixZoomIn(_context),
                    new CmdPageFixZoomOut(_context),
                    new CmdPageZoomToPercent100(_context),
                    new CmdPageZoomToWhole(_context),
                    new CmdPageZoomToWidth(_context),
                    new CmdAlignGraphicElement(_context) {SubType = 0},
                    new CmdAlignGraphicElement(_context) {SubType = 1},
                    new CmdAlignGraphicElement(_context) {SubType = 2},
                    new CmdAlignGraphicElement(_context) {SubType = 3},
                    new CmdGroupElement(_context),
                    new CmdUnGroupElement(_context),
                    new ToolNewClipBounds(_context),
                    new ToolNewPolygonClipGeometry(_context),
                    new ToolNewCircleClipGeometry(_context),
                    new CmdKeyClipCircleGeometry(_context),
                    new CmdKeyPolygonClipGeometry(_context),
                    new CmdClipGeometryByImportExcel(_context),
                    new CmdGroupElementEx(_context),
                    new CmdClearClipBounds(_context),
                    new CmdClearClipGeometry(_context),
                    new CmdCartoSetting(_context),
                    new CmdApplyTfMapTemplate(_context,_plugin),
                    new CmdApplyTFMapTemplateByDataRange(_context,_plugin),
                    new ToolApplyTemplateByMouseClick(_context),
                    new CmdClearCartoTemplate(_context),
                    new CmdPrintingSetting(_context),
                    new ToolRealClipOut(_context) {SubType = 0},
                    new CmdClipResultOut(_context) {SubType = 0},
                    new ToolClipResultDataOut(_context),
                    new CmdExportClipRegionFeatureClass(_context),
                    new ToolClipOutToOtherFormat(_context),

                    new CmdShowMapTemplateView(_context),
                    new CmdShowAutoLayoutView(_context),
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