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
                return new List<Guid>();
                return new List<Guid> { new Guid("7B9DF651-4B8B-4AA8-A4A9-C1463A35DAC7"), // Symbology
                                        new Guid("F24E7086-1762-4A7C-8403-D1169309CBC6"), // Repository
                                        new Guid("65beb2fd-eec2-461c-965e-f20a0cef2aa2"), // Identifier
                                        new Guid("894E958F-69DD-48DF-B35B-16871EC5D309"), // Table editor
                                        new Guid("70120ff9-1c6b-49a1-8949-dded8bcef499"), // Shape editor
                                        new Guid("F0CDF80F-5F74-48F6-8C8D-75F9B505EEE0"), // Debug window
                                        new Guid("F383FE43-0754-4EE2-951C-0066E87B28AB"), // GIS Toolbox
                                        new Guid("16479551-5754-46A1-9243-A3DF788E7406")  // Print lay-out
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
        private void SetDefaults()
        {
           Logger.Current.Trace("开始设置配置默认值");
            MapViewStyle= MapViewStyle.View2D;
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
         /*  GridFavorGreyscale = true;
           GridDefaultColorScheme = PredefinedColors.SummerMountains;
           GridRandomColorScheme = true;
           GridUseHistogram = true;
           IdentifierMode = IdentifierMode.AllLayersStopOnFirst;
           InnertiaOnPanning = AutoToggle.Auto;
           LastProjectPath = "";
           LegendExpandLayersOnAdding = true;
           LoadLastProject = true;
           LoadSymbology = true;
           LocalDocumentation = false;*/
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
