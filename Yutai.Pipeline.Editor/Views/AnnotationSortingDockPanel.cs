using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010.Customization;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Commands.Common;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Controls;

namespace Yutai.Pipeline.Editor.Views
{
    public partial class AnnotationSortingDockPanel : DockPanelControlBase, IAnnotationSortingView
    {
        private readonly IAppContext _context;
        private PipelineEditorPlugin _plugin;
        private readonly ICheQiConfig _cheQiConfig;
        private IPoint _originPoint;
        private List<IFeature> _annotationFeatures;

        public AnnotationSortingDockPanel(IAppContext context, PipelineEditorPlugin plugin)
        {
            InitializeComponent();
            _context = context;
            _plugin = plugin;
            _cheQiConfig = _plugin.CheQiConfig;
        }

        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield break; }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield break; }
        }

        public override string Caption
        {
            get { return "注记排序"; }
            set { Caption = value; }
        }

        public override DockPanelState DefaultDock => DockPanelState.Bottom;


        public override string DockName => DefaultDockName;

        public override string DefaultNestDockName => "";

        public const string DefaultDockName = "PipelineEditor_AnnotationSorting";
        
        public void Initialize(IAppContext context)
        {
        }

        private void btnSelectFeature_Click(object sender, EventArgs e)
        {
            _originPoint = new PointClass()
            {
                X = (_context.ActiveView.Extent.XMin + _context.ActiveView.Extent.XMax)/2,
                Y = _context.ActiveView.Extent.YMin
            };
            List<IFeature> features = CommonHelper.GetSelectedFeatures(_cheQiConfig.FlagAnnoLayer);
            List<DistanceFeature> pDistanceFeatures = ToDistanceFeatures(features, _originPoint);
            _annotationFeatures = pDistanceFeatures.Select(c => c.Feature).ToList();
            List<string> pAnnotationList = CommonHelper.ToAnnotations(_annotationFeatures);
            listBoxAnnotation.DataSource = pAnnotationList;
        }
        private List<DistanceFeature> ToDistanceFeatures(List<IFeature> features, IPoint point)
        {
            List<DistanceFeature> list = new List<DistanceFeature>();

            foreach (IFeature feature in features)
            {
                IPoint tempPoint = feature.Shape as IPoint;
                if (tempPoint == null)
                    continue;
                list.Add(new DistanceFeature
                {
                    Distance = GeometryHelper.GetDistance(point, tempPoint),
                    Feature = feature
                });
            }

            return new List<DistanceFeature>(list.OrderByDescending(c => c.Distance));
        }

        private void btnSelectDatumPoint_Click(object sender, EventArgs e)
        {
            if (cmbDirection.SelectedItem == null)
                return;
            enumAnnotationDirection direction = enumAnnotationDirection.RightUp;
            switch (cmbDirection.SelectedItem.ToString())
            {
                case "左上":
                    direction = enumAnnotationDirection.LeftUp;
                    break;
                case "右上":
                    direction = enumAnnotationDirection.RightUp;
                    break;
                case "右下":
                    direction = enumAnnotationDirection.RightDown;
                    break;
                case "左下":
                    direction = enumAnnotationDirection.LeftDown;
                    break;
            }
            CmdGetTargetPoint tool = new CmdGetTargetPoint(_context, _plugin);
            _context.CurrentTool = tool;
            tool.MouseDownEventHandler += delegate(IPoint point)
            {

                List<IPolygon> polygons = CommonHelper.GetMovedPoints(_annotationFeatures, point, direction);
                for (int i = 0; i < _annotationFeatures.Count; i++)
                {
                    IFeature pFeature = _annotationFeatures[i];
                    pFeature.Shape = polygons[i];
                    pFeature.Store();
                    IAnnotationFeature pAnnotationFeature = pFeature as IAnnotationFeature;
                    if (pAnnotationFeature == null)
                        continue;
                    IElement pElement = pAnnotationFeature.Annotation as IElement;
                    pElement.Geometry = new PointClass
                    {
                        X = (polygons[i].Envelope.XMin + polygons[i].Envelope.XMax) / 2,
                        Y = (polygons[i].Envelope.YMin + polygons[i].Envelope.YMax) / 2
                    };
                    pAnnotationFeature.Annotation = pElement;
                    pFeature.Store();
                }
                _context.FocusMap.ClearSelection();
                _context.ActiveView.Refresh();
            };
        }
    }

    public class DistanceFeature
    {
        public double Distance { get; set; }
        public IFeature Feature { get; set; }
    }

    public enum enumAnnotationDirection
    {
        LeftUp,
        RightUp,
        RightDown,
        LeftDown
    }
}
