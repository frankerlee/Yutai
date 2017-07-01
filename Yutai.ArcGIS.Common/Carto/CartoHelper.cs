using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using stdole;

namespace Yutai.ArcGIS.Common.Carto
{
    public class CartoHelper
    {
        public CartoHelper()
        {
        }

        private static IColor BuildRGB(int int_0, int int_1, int int_2)
        {
            IRgbColor rgbColorClass = new RgbColor()
            {
                Red = int_0,
                Green = int_1,
                Blue = int_2,
                UseWindowsDithering = true
            };
            return rgbColorClass;
        }

        private static ICalibratedMapGridBorder CreateCalibratedMapGridBorder()
        {
            ICalibratedMapGridBorder border = new CalibratedMapGridBorder() as ICalibratedMapGridBorder;
            border.BackgroundColor = CartoHelper.BuildRGB(255, 255, 255);
            border.ForegroundColor = CartoHelper.BuildRGB(0, 0, 0);
            border.BorderWidth = 3;
            border.Interval = 72;
            border.Alternating = false;
            ICalibratedMapGridBorder calibratedMapGridBorderClass = border as ICalibratedMapGridBorder;
            return calibratedMapGridBorderClass;
        }

        private static IGridLabel CreateFormattedGridLabel()
        {
            IFormattedGridLabel formattedGridLabelClass = new FormattedGridLabel() as IFormattedGridLabel;
            IGridLabel gridLabel = formattedGridLabelClass as IGridLabel;
            IFontDisp stdFontClass = new StdFont() as IFontDisp;
            stdFontClass.Name = "Arial";
            stdFontClass.Size = new decimal(16);
            gridLabel.Font = stdFontClass;
            gridLabel.Color = CartoHelper.BuildRGB(0, 0, 0);
            gridLabel.LabelOffset = 2;
            gridLabel.LabelAlignment[esriGridAxisEnum.esriGridAxisLeft] = false;
            gridLabel.LabelAlignment[esriGridAxisEnum.esriGridAxisRight] = false;
            INumericFormat format = new NumericFormat() as INumericFormat;
            format.AlignmentOption = esriNumericAlignmentEnum.esriAlignRight;
            format.RoundingOption = esriRoundingOptionEnum.esriRoundNumberOfDecimals;
            format.RoundingValue = 2;
            format.ShowPlusSign = false;
            format.UseSeparator = true;
            format.ZeroPad = true;
            INumericFormat numericFormatClass = format as INumericFormat;
            formattedGridLabelClass.Format = numericFormatClass as INumberFormat;
            return gridLabel;
        }

        public static IMapGrid CreateMapGrid(IMapFrame imapFrame_0)
        {
            IMapGrid measuredGridClass = new MeasuredGrid();
            measuredGridClass.SetDefaults(imapFrame_0);
            measuredGridClass.LineSymbol = null;
            IMarkerSymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol();
            (simpleMarkerSymbolClass as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
            simpleMarkerSymbolClass.Size = 28.3464;
            measuredGridClass.TickMarkSymbol = simpleMarkerSymbolClass;
            measuredGridClass.TickLineSymbol = new SimpleLineSymbol();
            measuredGridClass.TickLength = 14.1732;
            measuredGridClass.SetTickVisibility(true, true, true, true);
            measuredGridClass.SetSubTickVisibility(false, false, false, false);
            (measuredGridClass as IMeasuredGrid).XIntervalSize = 0.1*imapFrame_0.Map.MapScale;
            (measuredGridClass as IMeasuredGrid).YIntervalSize = 0.1*imapFrame_0.Map.MapScale;
            measuredGridClass.LabelFormat = CartoHelper.CreateFormattedGridLabel();
            measuredGridClass.Border = CartoHelper.CreateSimpleMapGridBorder();
            if (measuredGridClass is IMeasuredGrid)
            {
                IEnvelope mapBounds = imapFrame_0.MapBounds;
                (measuredGridClass as IMeasuredGrid).FixedOrigin = true;
                (measuredGridClass as IMeasuredGrid).XOrigin = mapBounds.XMin;
                (measuredGridClass as IMeasuredGrid).YOrigin = mapBounds.YMin;
            }
            return measuredGridClass;
        }

        private static IGridLabel CreateMixedFontGridLabel()
        {
            IMixedFontGridLabel mixedFontGridLabelClass = new MixedFontGridLabel() as IMixedFontGridLabel;
            IGridLabel gridLabel = mixedFontGridLabelClass as IGridLabel;
            IFontDisp stdFontClass = new StdFont() as IFontDisp;
            stdFontClass.Name = "Arial";
            stdFontClass.Size = new decimal(16);
            gridLabel.Font = stdFontClass;
            gridLabel.Color = CartoHelper.BuildRGB(0, 0, 0);
            gridLabel.LabelOffset = 2;
            gridLabel.LabelAlignment[esriGridAxisEnum.esriGridAxisLeft] = false;
            gridLabel.LabelAlignment[esriGridAxisEnum.esriGridAxisRight] = false;
            stdFontClass = new StdFont() as IFontDisp;
            stdFontClass.Name = "Arial";
            stdFontClass.Size = new decimal(12);
            mixedFontGridLabelClass.SecondaryFont = stdFontClass;
            mixedFontGridLabelClass.SecondaryColor = CartoHelper.BuildRGB(0, 0, 0);
            mixedFontGridLabelClass.NumGroupedDigits = 6;
            IFormattedGridLabel formattedGridLabel = mixedFontGridLabelClass as IFormattedGridLabel;
            INumericFormat format = new NumericFormat() as INumericFormat;
            format.AlignmentOption = esriNumericAlignmentEnum.esriAlignRight;
            format.RoundingOption = esriRoundingOptionEnum.esriRoundNumberOfDecimals;
            format.RoundingValue = 2;
            format.ShowPlusSign = true;
            format.UseSeparator = false;
            format.ZeroPad = true;
            INumericFormat numericFormatClass = format as INumericFormat;
            formattedGridLabel.Format = numericFormatClass as INumberFormat;
            return gridLabel;
        }

        private static IMapGridBorder CreateSimpleMapGridBorder()
        {
            ISimpleMapGridBorder simpleMapGridBorderClass = new SimpleMapGridBorder() as ISimpleMapGridBorder;
            ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol()
            {
                Style = esriSimpleLineStyle.esriSLSSolid,
                Width = 2
            };
            simpleMapGridBorderClass.LineSymbol = simpleLineSymbolClass;
            return simpleMapGridBorderClass as IMapGridBorder;
        }
    }
}