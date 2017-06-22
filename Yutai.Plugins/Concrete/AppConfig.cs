using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.Api.Enums;
using Yutai.Plugins.Enums;
using Yutai.Shared;

namespace Yutai.Plugins.Concrete
{
    [DataContract(Name = "Settings")]
    public class AppConfig
    {
        private List<Guid> _applicationPlugins;
        private CoordinatesDisplay _coordinatesDisplay;
        private PyramidPromptType _pyramidPromptType;
        private bool _useSnap;
        private bool _isSnapSketch;
        private IEngineSnapEnvironment _engineSnapEnvironment;
        private bool _isSnapPoint;
        private bool _isSnapIntersectionPoint;
        private bool _isSnapEndPoint;
        private bool _isSnapVertexPoint;
        private bool _isSnapMiddlePoint;
        private bool _isSnapBoundary;
        private bool _isSupportZd;
        private IEngineSnapEnvironment _snapEnvironment;
        private bool _isInEdit;
        private bool _canEdited;
        private bool _isSnapTangent;
        private string _customConfigPages;

        public AppConfig()
        {
            SetDefaults();
        }

        public static AppConfig Instance { get; internal set; }

        [DataMember]
        public AutoToggle AnimationOnZooming { get; set; }

        [DataMember]
        public List<Guid> ApplicationPlugins
        {
            get { return _applicationPlugins ?? (_applicationPlugins = DefaultApplicationPlugins); }
            set { _applicationPlugins = value; }
        }

        public List<Guid> DefaultApplicationPlugins
        {
            [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1500:CurlyBracketsForMultiLineStatementsMustNotShareLine", Justification = "Reviewed. Suppression is OK here.")]
            get
            {
                return new List<Guid>
                {
                   // new Guid("2b81c89a-ee45-4276-9dc1-72bbbf07f53f"), //定位器
                    new Guid("e7598649-d49b-45fa-b020-57e2dd777337"), //信息查看器
                    new Guid("01f8e32a-5837-431f-9c1b-5d0f195fb93e"), // 书签管理
                   // new Guid("4a3bcaab-9d3e-4ca7-a19d-7ee08fb0629e"), // 编辑
                   // new Guid("7da6c412-c345-4ccc-9f22-ef7eb3757898"), // 属性表
                   // new Guid("5e857d4e-1a77-46d8-b1bb-b36548be7333"),//Catalog功能
                };

            }
        }

        [DataMember]
        public AngleFormat CoordinateAngleFormat { get; set; }

        [DataMember]
        public int CoordinatePrecision { get; set; }

        [DataMember]
        public CoordinatesDisplay CoordinatesDisplay
        {
            get { return _coordinatesDisplay == CoordinatesDisplay.None ? CoordinatesDisplay.Auto : _coordinatesDisplay; }
            set { _coordinatesDisplay = value == CoordinatesDisplay.None ? CoordinatesDisplay.Auto : value; }
        }

        [DataMember]
        public bool FirstRun { get; set; }

        [DataMember]
        public bool UsingRibbon { get; set; }

        [DataMember]
        public bool LoadLastProject { get; set; }
        [DataMember]
        public bool LocalDocumentation { get; set; }
        [DataMember]
        public string LastConfigPage { get; set; }

        [DataMember]
        public int MeasurementsAreaPrecision { get; set; }

        [DataMember]
        public AreaUnits MeasurementsAreaUnits { get; set; }

        [DataMember]
        public int MeasurementsAreaWidth { get; set; }

        [DataMember]
        public int MeasurementsLengthPrecision { get; set; }

        [DataMember]
        public LengthUnits MeasurementsLengthUnits { get; set; }

        [DataMember]
        public int MeasurementsLengthWidth { get; set; }

        [DataMember]
        public AngleFormat MeasuringAngleFormat { get; set; }

        [DataMember]
        public int MeasuringAnglePrecision { get; set; }

        [DataMember]
        public int MeasuringAreaPrecision { get; set; }

        [DataMember]
        public AreaDisplay MeasuringAreaUnits { get; set; }

        [DataMember]
        public int MeasuringLengthPrecision { get; set; }

        [DataMember]
        public LengthDisplay MeasuringLengthUnits { get; set; }

        [DataMember]
        public Color MeasuringLineColor { get; set; }

        [DataMember]
        public DashStyle MeasuringLineStyle { get; set; }

        [DataMember]
        public int MeasuringLineWidth { get; set; }

        [DataMember]
        public bool MeasuringPointLabelsVisible { get; set; }

        [DataMember]
        public bool MeasuringPointsVisible { get; set; }

        [DataMember]
        public bool MeasuringShowBearing { get; set; }

        [DataMember]
        public bool MeasuringShowLength { get; set; }

        [DataMember]
        public bool MeasuringShowTotalLength { get; set; }

        [DataMember]
        public bool OverviewBackgroundVisible { get; set; }

        [DataMember]
        public ScalebarUnits ScalebarUnits { get; set; }

        [DataMember]
        public bool ShowCoordinates { get; set; }

        [DataMember]
        public bool ShowMenuToolTips { get; set; }

        [DataMember]
        public bool ShowPluginInToolTip { get; set; }
        [DataMember]
        public bool ShowScalebar { get; set; }

        [DataMember]
        public bool ShowWelcomeDialog { get; set; }

        [DataMember]
        public MapViewStyle MapViewStyle { get; set; }

        [DataMember]
        public MenuStyle MenuStyle { get; set; }

        [DataMember]
        public Color MapBackgroundColor { get; set; }

        [DataMember]
        public IdentifierMode IdentifierMode { get; set; }

        [DataMember]
        public int IdentifierToPixel { get; set; }

        [DataMember]
        public ISelectionEnvironment SelectionEnvironment { get; set; }

        [DataMember]
        public IFeatureLayer SelectionCurrentLayerName { get; set; }
        

        [DataMember]
        public int LocatorMaxCount { get; set; }
        [DataMember]
        public PyramidPromptType PyramidPromptType
        {
            get { return _pyramidPromptType; }
            set { _pyramidPromptType = value; }
        }
        [DataMember]
        public bool UseSnap
        {
            get { return _useSnap; }
            set { _useSnap = value; }
        }
        [DataMember]
        public bool IsSnapSketch
        {
            get { return _isSnapSketch; }
            set { _isSnapSketch = value; }
        }
        [DataMember]
        public IEngineSnapEnvironment EngineSnapEnvironment
        {
            get { return _engineSnapEnvironment; }
            set { _engineSnapEnvironment = value; }
        }
        [DataMember]
        public bool IsSnapPoint
        {
            get { return _isSnapPoint; }
            set { _isSnapPoint = value; }
        }
        [DataMember]
        public bool IsSnapIntersectionPoint
        {
            get { return _isSnapIntersectionPoint; }
            set { _isSnapIntersectionPoint = value; }
        }
        [DataMember]
        public bool IsSnapEndPoint
        {
            get { return _isSnapEndPoint; }
            set { _isSnapEndPoint = value; }
        }
        [DataMember]
        public bool IsSnapVertexPoint
        {
            get { return _isSnapVertexPoint; }
            set { _isSnapVertexPoint = value; }
        }
        [DataMember]
        public bool IsSnapMiddlePoint
        {
            get { return _isSnapMiddlePoint; }
            set { _isSnapMiddlePoint = value; }
        }
        [DataMember]
        public bool IsSnapBoundary
        {
            get { return _isSnapBoundary; }
            set { _isSnapBoundary = value; }
        }
        [DataMember]
        public bool IsSupportZD
        {
            get { return _isSupportZd; }
            set { _isSupportZd = value; }
        }

        public IEngineSnapEnvironment SnapEnvironment
        {
            get { return _snapEnvironment; }
            set { _snapEnvironment = value; }
        }

        public bool IsInEdit
        {
            get { return _isInEdit; }
            set { _isInEdit = value; }
        }

        public bool CanEdited
        {
            get { return _canEdited; }
            set { _canEdited = value; }
        }

        public bool IsSnapTangent
        {
            get { return _isSnapTangent; }
            set { _isSnapTangent = value; }
        }

        public string CustomConfigPages
        {
            get { return _customConfigPages; }
            set { _customConfigPages = value; }
        }

        public bool LoadAllConfigPages { get; set; }
        public double Tolerance { get; set; }
        public double SnapTolerance { get; set; }
        public bool IsAddStartPoint { get; set; }
        public bool IsAddEndPoint { get; set; }
        public bool IsAddVertexPoint { get; set; }


        internal void SetDefaults()
        {
            Logger.Current.Trace("开始设置配置默认值");
            #region 编辑部分设置

            _canEdited = true;
            _isInEdit = false;
          
            IsSnapTangent = false;
            UseSnap = false;
            _engineSnapEnvironment=new EngineEditorClass();
            ((IEngineEditProperties2) _engineSnapEnvironment).SnapTips = true;
            _engineSnapEnvironment.SnapTolerance = 10;

            
            #endregion
            LocatorMaxCount = 100;

            MapViewStyle = MapViewStyle.View2D;
            AnimationOnZooming = AutoToggle.Auto;
            UsingRibbon = true;
            /*   CacheRenderingData = false;
               CacheDbfRecords = false;*/
            CoordinateAngleFormat = AngleFormat.Seconds;
            CoordinatesDisplay = CoordinatesDisplay.Auto;
            CoordinatePrecision = 3;
            /*    CreatePyramidsOnOpening = true;
                CreateSpatialIndexOnOpening = true;
                DisplayDynamicVisibilityWarnings = true;*/
            FirstRun = true;
            IdentifierMode = IdentifierMode.AllLayer;
            IdentifierToPixel = 5;
            SelectionEnvironment=new SelectionEnvironmentClass()
            {
                
            };

            LoadLastProject = true;
            LocalDocumentation = false;

            /*  GridFavorGreyscale = true;
              GridDefaultColorScheme = PredefinedColors.SummerMountains;
              GridRandomColorScheme = true;
              GridUseHistogram = true;

              InnertiaOnPanning = AutoToggle.Auto;
              LastProjectPath = "";
              LegendExpandLayersOnAdding = true;
              
              LoadSymbology = true;
              */
            MapBackgroundColor = Color.White;
            //MeasurementsAreaFieldName = "Area";
            MeasurementsAreaPrecision = 3;
            MeasurementsAreaUnits = AreaUnits.SquareMeters;
            MeasurementsAreaWidth = 14;
            // MeasurementsLengthFieldName = "Length";
            MeasurementsLengthPrecision = 3;
            MeasurementsLengthUnits = LengthUnits.Meters;
            MeasurementsLengthWidth = 14;
            //MeasurementsPerimeterFieldName = "Perimeter";
            MeasuringAngleFormat = AngleFormat.Degrees;
            MeasuringAnglePrecision = 1;
            MeasuringAreaPrecision = 1;
            MeasuringAreaUnits = AreaDisplay.Metric;
            /* MeasuringBearingType = BearingType.Absolute;
             MeasuringFillColor = Color.Orange;
             MeasuringFillTransparency = 100;*/
            MeasuringLengthPrecision = 1;
            MeasuringLengthUnits = LengthDisplay.Metric;
            MeasuringLineColor = Color.Orange;
            MeasuringLineStyle = DashStyle.Solid;
            MeasuringLineWidth = 2;
            MeasuringPointLabelsVisible = true;
            MeasuringPointsVisible = true;
            MeasuringShowBearing = true;
            MeasuringShowLength = true;
            MeasuringShowTotalLength = true;
            /*         MouseTolerance = 10;
                     MouseWheelDirection = MouseWheelDirection.Forward;
                     OgrMaxFeatureCount = 50000;
                     OgrShareConnection = false;*/
            OverviewBackgroundVisible = true;
            /*           PrintingUnits = 0;
                       PrintingMargins = new Margins(25, 25, 50, 50);
                       PrintingOrientation = Orientation.Vertical;
                       PrintingPaperFormat = "A4";
                       PrintingTemplate = "";
                       ProjectionAbsence = ProjectionAbsence.IgnoreAbsence;
                       ProjectionMismatch = ProjectionMismatch.Reproject;
                       ProjectionShowLoadingReport = true;
                       PyramidCompression = TiffCompression.Auto;
                       PyramidSampling = RasterOverviewSampling.Nearest;
                       QueryBuilderShowValue = true;
                       QueryBuilderSelectionOperation = SelectionOperation.New;
                       RasterDownsamplingMode = InterpolationType.Bilinear;
                       RasterUpsamplingMode = InterpolationType.None;
                       ResizeBehavior = ResizeBehavior.KeepScale;
                       ReuseTileBuffer = true;*/
            ScalebarUnits = ScalebarUnits.GoogleStyle;
            /*   ShapeEditorShowLabels = true;
               ShapeEditorShowAttributeDialog = true;
               ShapeEditorShowBearing = false;
               ShapeEditorBearingType = BearingType.Absolute;
               ShapeEditorBearingPrecision = 1;
               ShapeEditorAngleFormat = AngleFormat.Degrees;
               ShapeEditorShowLength = true;
               ShapeEditorShowArea = true;
               ShapeEditorUnits = LengthDisplay.Metric;
               ShapeEditorUnitPrecision = 1;*/
            ShowCoordinates = true;
            ShowMenuToolTips = false;
            ShowPluginInToolTip = false; // perhaps some kind of debug mode will be enough
                                         /*         ShowProjectionAbsenceDialog = true;
                                                  ShowProjectionMismatchDialog = true;
                                                  ShowPyramidDialog = true;
                                                  ShowRedrawTime = false;*/
            ShowScalebar = true;
            /*         ShowSpatialIndexDialog = false;
                     ShowValuesOnMouseMove = true;*/
            ShowWelcomeDialog = true;
            /*      ShowZoombar = true;
                  SpatialIndexFeatureCount = 10000;
                  SymbolobyStorage = SymbologyStorage.Project;
                  TableEditorFormatValues = true;
                  TableEditorLayout = TableEditorLayout.Tabbed;
                  TableEditorShowAliases = true;
                  TaskRunInBackground = false;
                  TilesAutoDetectProxy = true;
                  TilesUseRamCache = true;
                  TilesMaxRamSize = 100.0;
                  TilesUseDiskCache = true;
                  TilesDatabase = string.Empty;
                  TilesMaxDiskSize = 300.0;
                  TilesMaxDiskAge = TilesMaxAge.Month3;
                  TilesProxyAddress = string.Empty;
                  TilesProxyPassword = string.Empty;
                  TilesProxyUserName = string.Empty;
                  TilesUseProxy = true;
                  ToolOutputAddToMap = true;
                  ToolOutputInMemory = true;
                  ToolOutputOverwrite = false;
                  ToolShowGdalOptionsDialog = true;
                  UpdaterCheckNewVersion = true;
                  UpdaterHasNewInstaller = false;
                  UpdaterIsDownloading = false;
                  UpdaterLastChecked = new DateTime(2015, 1, 1);
                  WmsDiskCaching = true;
                  WmsLastServer = string.Empty;
                  ZoomBarVerbosity = ZoomBarVerbosity.Full;
                  ZoomBehavior = ZoomBehavior.UseTileLevels;
                  ZoomBoxStyle = ZoomBoxStyle.Blue;*/
            Logger.Current.Trace("End AppConfig.SetDefaults()");

        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            SetDefaults();
        }


    }
}
