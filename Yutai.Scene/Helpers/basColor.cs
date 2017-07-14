using System.Drawing;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.Plugins.Scene.Helpers
{
    internal sealed class basColor
    {
        public basColor()
        {
        }

        public static IRgbColor SelColor(int int_0, int int_1, tagRECT tagRECT_0)
        {
            IRgbColor rgbColorClass = new RgbColor()
            {
                RGB = int_0
            };
            rgbColorClass.RGB = ColorTranslator.ToOle(Color.Black);
            return rgbColorClass;
        }
    }
}