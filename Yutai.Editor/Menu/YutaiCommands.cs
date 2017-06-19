using System;
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
           
            try
            {
                _commands = new List<YutaiCommand>()
                {
                    new CmdEditorStart(_context, _plugin),
                    new CmdEditorStop(_context, _plugin),
                    new CmdEditorSave(_context, _plugin),
                    new CmdSnapOff(_context),
                    new CmdSnapSetting(_context),
                    new CmdSnapPoint(_context),
                    new CmdSnapMidPoint(_context),
                    new CmdSnapEndPointt(_context),
                    new CmdSnapVertexPoint(_context),
                    new CmdSnapIntersectPoint(_context),
                    new CmdSnapBoundary(_context),
                    new CmdSnapTangent(_context),
                    new CmdSnapSketch(_context),
                    new CmdShowEditTemplate(_context),
                    new CmdSketchPoint(_context),
                    new CmdSketchLinePoint(_context),
                    new CmdSketchAlongLinePoint(_context),
                    new CmdSketchLine(_context),
                    new CmdSketchArcLine(_context),
                    new CmdSketchRectangle(_context),
                    new CmdSketchCircle(_context),
                    new CmdSketchEllipse(_context),
                    new CmdSketchRectangle2(_context),
                    new CmdShowGeometryVertex(_context),
                    new CmdStartSketch(_context),
                    new CmdSnapToEndPoint(_context),
                    new CmdSnapToMidPoint(_context),
                    new CmdSnapToSegment(_context),
                    new CmdSnapToVertex(_context),
                    new StreamingFlag(_context),
                    new CmdDirectionTool(_context),
                    new CmdLengthTool(_context),
                    new CmdDirectionLengthTool(_context),
                    new CmdAbsoluteXYTool(_context),
                    new CmdDeltaXYTool(_context),
                    new CmdSquareFinishTool(_context),
                    new CmdCompletePartTool(_context),
                    new CmdDeleteSketchTool(_context),
                    new CmdCompleteSketchTool(_context),
                    new CmdFeatureReShapeTool(_context),
                    new CmdEditTarget(_context),
                    new CmdViewGeometryInfo(_context),
                    new CmdMoveVertToPoint(_context),
                    new CmdMoveVert(_context),
                    new CmdMoveFeatureToPoint(_context),
                    new CmdMoveFeature(_context),
                    new CmdAddPointInGeometry(_context),
                    new CmdEditCopy(_context),
                    new CmdEditPaste(_context),
                    new CmdCopyFeaturesTool(_context),
                    new CmdZoom2SelectedFeature(_context),
                    new CmdDeleteSelectedFeature(_context),
                    new CmdClearSelectedFeature(_context),
                    new CmdEditAttribute(_context),
                    new CmdFollowAlong(_context),
                    new CmdBufferTool(_context),
                    new CmdRotateFeature(_context),
                    new CmdDeComposeGeometry(_context),
                    new CmdTrimLineEx(_context),
                    new CmdStretchLine(_context),
                    new CmdExtendLine(_context),
                    new CmdComposeGeometry(_context),
                    new CmdGeometryGeneralize(_context),
                    new CmdGeometrySmooth(_context),
                    new CmdProportionSplit(_context),
                    new CmdSplitTool(_context),
                    new CmdMirrorToolEx(_context),
                    new CmdPointSplitLine(_context),
                    new CmdPolygonCutByPolygon(_context),
                    new CmdPolygonCutHollow(_context),
                    new CmdConstructParallel(_context),



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