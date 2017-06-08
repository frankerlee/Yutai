namespace Yutai.ArcGIS.Framework
{
    public interface IBarEditItem
    {
        object BarEditItem { set; }

        BarEditStyle Style { get; }

        int Width { get; }
    }
}

