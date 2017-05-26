using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Services;

namespace Yutai.Controls
{
    public class MapLegendPresenter : CommandDispatcher<MapLegendDockPanel, MapLegendCommand>
    {
        private readonly IAppContext _context;
        private readonly IBroadcasterService _broadcaster;
        private readonly MapLegendDockPanel _legendDockPanel;

        public MapLegendPresenter(IAppContext context, IBroadcasterService broadcaster,
                               MapLegendDockPanel legendDockPanel)
            : base(legendDockPanel)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (broadcaster == null) throw new ArgumentNullException("broadcaster");
            if (legendDockPanel == null) throw new ArgumentNullException("legendDockPanel");

            _context = context;
            _broadcaster = broadcaster;
            _legendDockPanel = legendDockPanel;

            //View.LegendKeyDown += OnLegendKeyDown;
        }

        private void OnLegendKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete )//&& _legendDockPanel.Legend.SelectedLayer != null)
            {
                RunCommand(MapLegendCommand.RemoveLayer);
            }
        }

        public MapLegendDockPanel LegendControl
        {
            get { return _legendDockPanel; }
        }

        public override void RunCommand(MapLegendCommand command)
        {
            /*switch (command)
            {
                case MapLegendCommand.AddGroup:
                    Legend.Groups.Add();
                    break;
                case MapLegendCommand.AddLayer:
                    {
                        int groupHandle = _legendDockPanel.SelectedGroupHandle;
                        if (_layerService.AddLayer(DataSourceType.All) && groupHandle != -1)
                        {
                            int layerHandle = _layerService.LastLayerHandle;
                            Legend.Layers.MoveLayer(layerHandle, groupHandle);
                        }
                        break;
                    }
                case MapLegendCommand.GroupProperties:
                    {
                        int groupHandle = _legendDockPanel.SelectedGroupHandle;
                        var group = Legend.Groups.ItemByHandle(groupHandle);
                        if (group != null)
                        {
                            if (_context.Container.Run<LegendGroupPresenter, ILegendGroup>(group))
                            {
                                Legend.Redraw();
                            }
                        }
                        break;
                    }
                case MapLegendCommand.ZoomToGroup:
                    {
                        var group = Legend.Groups.ItemByHandle(_legendDockPanel.SelectedGroupHandle);
                        if (group != null)
                        {
                            var box = group.Envelope;
                            _context.Map.ZoomToExtents(box);
                        }
                        break;
                    }
                case MapLegendCommand.RemoveGroup:
                    {
                        var group = Legend.Groups.ItemByHandle(_legendDockPanel.SelectedGroupHandle);
                        if (group != null)
                        {
                            if (MessageService.Current.Ask("Do you want to remove group: " + group.Text + "?"))
                            {
                                Legend.Groups.Remove(group.Handle);
                            }
                        }
                        break;
                    }
                case MapLegendCommand.Labels:
                    {
                        var layer = _context.MapLegend.Layers.Current;
                        if (layer != null && layer.IsVector)
                        {
                            _broadcaster.BroadcastEvent(p => p.LayerLabelsClicked_, _context.Legend, new LayerEventArgs(layer.Handle));
                        }
                    }
                    break;
                case MapLegendCommand.TableEditor:
                    {
                        var layer = _context.Legend.Layers.Current;
                        if (layer != null && layer.IsVector)
                        {
                            var args = new PluginMessageEventArgs(PluginMessages.ShowAttributeTable);
                            _broadcaster.BroadcastEvent(t => t.MessageBroadcasted_, _context.Legend, args);
                        }
                    }
                    break;
                case MapLegendCommand.ZoomToLayer:
                    _context.Map.ZoomToLayer(_context.Legend.SelectedLayerHandle);
                    break;
                case MapLegendCommand.RemoveLayer:
                    _layerService.RemoveSelectedLayer();
                    break;
                case MapLegendCommand.Properties:
                    _broadcaster.BroadcastEvent(p => p.LayerDoubleClicked_, Legend,
                        new LayerEventArgs(Legend.SelectedLayerHandle));
                    break;
                case MapLegendCommand.SaveStyle:
                    {
                        var layer = Legend.Layers.Current;

                        if (layer != null)
                        {
                            if (!LayerSerializationHelper.CheckFilename(layer.Filename))
                            {
                                MessageService.Current.Info("Can not save settings for a non-disk based layer.");
                                return;
                            }

                            LayerSerializationHelper.SaveSettings(layer);
                        }
                        break;
                    }
                case MapLegendCommand.LoadStyle:
                    {
                        var layer = Legend.Layers.Current;

                        if (layer != null)
                        {
                            if (!LayerSerializationHelper.CheckFilename(layer.Filename))
                            {
                                MessageService.Current.Info("Can not load settings for a non-disk based layer.");
                                return;
                            }

                            LayerSerializationHelper.LoadSettings(layer, _broadcaster, false);
                        }

                        _context.Legend.Redraw(LegendRedraw.LegendAndMap);
                    }
                    break;
                case MapLegendCommand.OpenFileLocation:
                    {
                        var layer = Legend.Layers.Current;
                        if (layer != null && File.Exists(layer.Filename))
                        {
                            Shared.PathHelper.OpenFolderWithExplorer(layer.Filename);
                        }
                        else
                        {
                            MessageService.Current.Warn("Failed to find file for the layer.");
                        }
                        break;
                    }
            }*/
        }
    }
}
