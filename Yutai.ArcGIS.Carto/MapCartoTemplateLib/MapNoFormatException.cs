using System;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public sealed class MapNoFormatException : Exception
    {
        public override string Message
        {
            get { return "图号格式错误!"; }
        }
    }
}