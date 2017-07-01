using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class LegendItemWrap
    {
        private ILegendItem ilegendItem_0 = null;

        public LegendItemWrap(ILegendItem ilegendItem_1)
        {
            this.ilegendItem_0 = ilegendItem_1;
        }

        public override string ToString()
        {
            return this.ilegendItem_0.Layer.Name;
        }

        public ILegendItem LegendItem
        {
            get { return this.ilegendItem_0; }
        }
    }
}